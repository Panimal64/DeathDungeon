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
	public partial class EndGamePage : ContentPage
	{
        private ScoreDetailViewModel _viewModel;
        
        public Score Data { get; set; }
        //---------------sets scores from battle-------------------------------
        public EndGamePage(ScoreDetailViewModel viewModel)
        {
            Data = new Score(); 
            Data = viewModel.Data;
            Data.Id = Guid.NewGuid().ToString();
            InitializeComponent();
           //Binders to the VM
            BindingContext = _viewModel = viewModel;
            BindingContext = _viewModel;
            Data.ScoreTotal = _viewModel.Data.ExperienceGainedTotal;
            Data.GameDate = _viewModel.Data.GameDate;    // Set to be now by default.
            Data.AutoBattle = _viewModel.Data.AutoBattle;         //assume user battle
            Data.ItemsDroppedList = _viewModel.Data.ItemsDroppedList;
            Data.MonstersKilledList = _viewModel.Data.MonstersKilledList;
            Data.ExperienceGainedTotal = _viewModel.Data.ExperienceGainedTotal;
            Data.Round = _viewModel.Data.Round;
            Data.Turn = _viewModel.Data.Turn;
            Data.CharacterAtDeathList = _viewModel.Data.CharacterAtDeathList;

        }
        //---------------INITS--------------------------------
        public EndGamePage()
        {
            InitializeComponent();

            Data = new Score //initializer for data
            {
                Name = "Adventurer",
                ScoreTotal = 0,
                Id = Guid.NewGuid().ToString()
            };

            BindingContext = this;
        }
        //---------------SAVE SCORE AND GO MENU---------------------------------
        private async void Menu_Clicked(object sender, EventArgs e)
        {
            int numModal = Application.Current.MainPage.Navigation.ModalStack.Count; //counts stack of modal pushes
            MessagingCenter.Send(this, "AddData", Data);//send data to database
            ScoresViewModel.Instance.SetNeedsRefresh(true); //set model to refresh
            for (int curr = 0; curr < numModal; curr++) //loop to send back to main menu
            {
                await Application.Current.MainPage.Navigation.PopModalAsync();
            }
            
        }
    }
}

