using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using DeathDungeon.Models;
using DeathDungeon.ViewModels;

namespace DeathDungeon.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MonsterPartyPage : ContentPage
    {
        private MonstersViewModel _viewModel;
        public MonsterPartyPage()
        {
            InitializeComponent();
            BindingContext = _viewModel = MonstersViewModel.Instance;

        }


        public async void MonsterItemSelected(object sender, SelectedItemChangedEventArgs args){

            var data = args.SelectedItem as Monster;
            if (data == null)
            {
                return;
            }

            await Navigation.PushAsync(new MonsterDetailPage(new MonstersDetailViewModel(data)));

            // Manually deselect item.
            MonsterListView.SelectedItem = null;
        }
        public async void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            var data = args.SelectedItem as Monster;
            if (data == null)
            {
                return;
            }

            await Navigation.PushAsync(new MonsterDetailPage(new MonstersDetailViewModel(data)));

            // Manually deselect item.
            MonsterListView.SelectedItem = null;
        }
        public async void PartyItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            var data = args.SelectedItem as Monster;
            if (data == null)
            {
                return;
            }

            await Navigation.PushAsync(new MonsterDetailPage(new MonstersDetailViewModel(data)));

            // Manually deselect item.
            PartyListView.SelectedItem = null;
        }
        private async void Continue_Clicked(object sender, EventArgs e)
        {
            if (_viewModel.DatasetParty.Count == 6)
            {
                await Navigation.PushAsync(new BattlePage());
            }
        }

       // private async void Cancel_Clicked(object sender, EventArgs e)
        //{
          //  await Navigation.PopAsync();
        //}
        protected override void OnAppearing()
        {
            base.OnAppearing();

            BindingContext = null;

            if (ToolbarItems.Count > 0)
            {
                ToolbarItems.RemoveAt(0);
            }

            InitializeComponent();

            if (_viewModel.Dataset.Count == 0||_viewModel.DatasetParty.Count==0)
            {
                _viewModel.LoadDataCommand.Execute(null);
            }
            else if (_viewModel.NeedsRefresh())
            {
                _viewModel.LoadDataCommand.Execute(null);
            }

            BindingContext = _viewModel;
        }


    }
}