using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using DeathDungeon.ViewModels;
using System.Threading.Tasks;
using DeathDungeon.Models;
namespace DeathDungeon.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class OpeningPage : ContentPage
    {
       

        private BattleViewModel _viewModel;


        public OpeningPage()
        {
            InitializeComponent();
            ItemsViewModel.Instance.LoadDataCommand.Execute(null);
            MonstersViewModel.Instance.LoadDataCommand.Execute(null);
            CharactersViewModel.Instance.LoadDataCommand.Execute(null);
            ScoresViewModel.Instance.LoadDataCommand.Execute(null);
            // BattleViewModel.Instance.LoadDataCommand.Execute(null); //was active when manual battle happened
        }
        //------------MANUAL BATTLE--------------------------------
        //If player clicks manual battle
        private async void UserBattle_Clicked(object sender, EventArgs e)
        {
            //BattleViewModel.Instance.LoadDataCommand.Execute(null);
            if (GlobalVariables.auto_battle_reset == true)
            {
                _viewModel.resetBattleView();
            }

            else if (_viewModel == BattleViewModel.AutoInstance)
            {
                _viewModel.resetBattleView();
            }
            _viewModel = BattleViewModel.Instance;
            _viewModel.LoadDataCommand.Execute(null);
            await Navigation.PushModalAsync(new CharacterPartyPage());
        }

        //-------------AUTOBATTLE---------------------------------
        //If player clicks auto battle 
        private async void AutoBattle_Clicked(object sender, EventArgs e)
        {
            if (GlobalVariables.auto_battle_reset == true)
            {
                _viewModel.resetAutoBattleView();
            }

            else if (_viewModel == BattleViewModel.Instance)
            {
                _viewModel.resetAutoBattleView();
            }
            _viewModel = BattleViewModel.AutoInstance;

            _viewModel.LoadDataCommand.Execute(null);

            await Task.Delay(5000);
           
            string result = await _viewModel.FightClickedAutoAsync();

            if (result == "endgame")
            {
                var data = new ScoreDetailViewModel(_viewModel.battleInstance.score);
                await Navigation.PushModalAsync(new EndGamePage(data));
            }
            if (GlobalVariables.auto_battle_reset == false)
            {
                GlobalVariables.auto_battle_reset = true;
            }
            
        }
        //-------------------------------------------------------
        protected override void OnAppearing()
        {
            base.OnAppearing();

            BindingContext = null;
            
            InitializeComponent();

            _viewModel = BattleViewModel.AutoInstance;

            _viewModel.LoadDataCommand.Execute(null);

            BindingContext = _viewModel;
           

        }

    }
}