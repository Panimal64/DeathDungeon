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
	public partial class CharacterPartyPage : ContentPage
	{
        //____________VARIABLES______________________________________
        private CharactersViewModel _viewModel;
        //__________INIT________________________________________
        public CharacterPartyPage()
        {
            InitializeComponent();
            BindingContext = _viewModel = CharactersViewModel.Instance;
        }
        //______________UI_________________________________________
        //select character to add
        public async void CharacterItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            var data = args.SelectedItem as Character;
            if (data == null)
            {
                return;
            }

            _viewModel.AddToParty(data);

            // Manually deselect item.
            CharacterListView.SelectedItem = null;
        }
        //party select to remove from party
        public async void PartyItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            var data = args.SelectedItem as Character;
            if (data == null)
            {
                return;
            }

            _viewModel.RemoveFromParty(data);
            //await Navigation.PushAsync(new CharacterDetailPage(new CharacterDetailViewModel(data)));

            // Manually deselect item.
            PartyListView.SelectedItem = null;
        }

        //add more character, disable for final product
        private async void Add_Clicked(object sender, EventArgs e)
        {

            await Navigation.PushAsync(new NewCharacterPage());
        }
        //continue to battle, under condition of 6 character
        private async void Continue_Clicked(object sender, EventArgs e)
        {
            if (_viewModel.DatasetParty.Count == 6)
            {
                await Navigation.PushModalAsync(new BattlePage(_viewModel.PartyList));
            }
        }
        //clear party
        private void clearParty()
        {
            _viewModel.DatasetParty.Clear();
        }
        //___________________________________________________________
        protected override void OnAppearing()
        {
            base.OnAppearing();

            BindingContext = null;

            if (ToolbarItems.Count > 0)
            {
                ToolbarItems.RemoveAt(0);
            }

            InitializeComponent();

            _viewModel.DatasetParty.Clear();

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