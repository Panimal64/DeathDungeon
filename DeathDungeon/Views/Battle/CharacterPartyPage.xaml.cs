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
        //      //private CharacterPartyViewModel _viewModel;
        //      private CharacterPartyViewModel _viewModel;

        //      public CharacterPartyPage ()
        //{
        //	InitializeComponent ();
        //          BindingContext = _viewModel = CharacterPartyViewModel.Instance;

        //}

        //      public async void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
        //      {
        //          var data = args.SelectedItem as Character;
        //          if (data == null)
        //          {
        //              return;
        //          }

        //          await Navigation.PushAsync(new CharacterDetailPage(new CharacterDetailViewModel(data)));

        //          // Manually deselect item.
        //          ItemsListView.SelectedItem = null;
        //      }
        //      private async void Continue_Clicked(object sender, EventArgs e)
        //      {
        //          await Navigation.PushAsync(new MonsterPartyPage());
        //      }
        //      private async void Add_Clicked(object sender, EventArgs e)
        //      {
        //          await Navigation.PushAsync(new NewCharacterPage());
        //      }

        //      // private async void Cancel_Clicked(object sender, EventArgs e)
        //      //{
        //      //  await Navigation.PopAsync();
        //      //}

        //      protected override void OnAppearing()
        //      {
        //          base.OnAppearing();

        //          BindingContext = null;

        //          if (ToolbarItems.Count > 0)
        //          {
        //              ToolbarItems.RemoveAt(0);
        //          }

        //          InitializeComponent();

        //          if (_viewModel.Dataset.Count == 0)
        //          {
        //              _viewModel.LoadDataCommand.Execute(null);
        //          }
        //          else if (_viewModel.NeedsRefresh())
        //          {
        //              _viewModel.LoadDataCommand.Execute(null);
        //          }

        //          BindingContext = _viewModel;
        //      }

        private CharactersViewModel _viewModel;

        public CharacterPartyPage()
        {
            InitializeComponent();
            BindingContext = _viewModel = CharactersViewModel.Instance;
        }

        public async void CharacterItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            var data = args.SelectedItem as Character;
            if (data == null)
            {
                return;
            }

            await Navigation.PushAsync(new CharacterDetailPage(new CharacterDetailViewModel(data)));

            // Manually deselect item.
            CharacterListView.SelectedItem = null;
        }
        public async void PartyItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            var data = args.SelectedItem as Character;
            if (data == null)
            {
                return;
            }

            await Navigation.PushAsync(new CharacterDetailPage(new CharacterDetailViewModel(data)));

            // Manually deselect item.
            PartyListView.SelectedItem = null;
        }

        private async void Add_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new NewCharacterPage());
        }
        private async void Continue_Clicked(object sender, EventArgs e)
             {
            if (_viewModel.DatasetParty.Count == 6)
            {
                await Navigation.PushAsync(new MonsterPartyPage());
            }
              }

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