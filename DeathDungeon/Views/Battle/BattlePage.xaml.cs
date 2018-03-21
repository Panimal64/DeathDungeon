using DeathDungeon.Models;
using DeathDungeon.ViewModels;
using DeathDungeon.Views;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

using DeathDungeon.ViewModels;
using DeathDungeon.Models;
using DeathDungeon.Views;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DeathDungeon.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    //Initialize---------------------------------------------------------------
    public partial class BattlePage : ContentPage
    {

        private BattleViewModel _viewModel;

        private string message;
        //private BattleClass battleInstance;

        public BattlePage()
        {
            InitializeComponent();

            BindingContext = _viewModel = BattleViewModel.Instance;


        }


        public BattlePage(List<string> characterList)
        {

            InitializeComponent();

            BindingContext = _viewModel = BattleViewModel.Instance;

            _viewModel.PartyList = characterList;
            _viewModel.SetNeedsRefresh(true);
        }
        //____________________SELECTED ITEMS_______________________________________________________
        //Fight button click
        public async void OnFight_Clicked(object sender, SelectedItemChangedEventArgs args)
        {

            message = _viewModel.FightClicked();

            if (message == "EndGame")
            {
                _viewModel.battleInstance.EndGame();
                var data = new ScoreDetailViewModel(_viewModel.battleInstance.score);
                await Navigation.PushModalAsync(new EndGamePage(data));
            }
            else if (message == "EndRound") //finish round
            {
                var data = new ScoreDetailViewModel(_viewModel.battleInstance.score);
                await Navigation.PushModalAsync(new EndRoundPage(data));
                //or send to items page
                return;

            }
            else if (message == "reload") //reload
            {

                reloadEntities();
            }
            OutputCharacter.Text = _viewModel.checkCharacterMessage(); //check if monster or character message appeared
            OutputMonster.Text = _viewModel.checkMonsterMessage();

            string checker = _viewModel.entityCheck();
            if (checker == "EndGame") //endgame case
            {
                var data = new ScoreDetailViewModel(_viewModel.battleInstance.score);
                await Navigation.PushModalAsync(new EndGamePage(data));
            }
            else if (checker == "refresh") //reload round
            {
                
                await Navigation.PushModalAsync(new ItemDropPage(_viewModel.Pool));
                RoundRefreshPage();
            }

        }


        //_____________GRID___________________________________________________
        //Setup play grid
        public void GridSetup()
        {
            #region first populate grid commnads
            //ADJUSTING OR POPULATING GRID
            populateGrid();

            insertMonster();//test cyclops entry
            insertGrid();
            //insertGrid();//test grid
            _viewModel.battleInstance.ResetTurns();
            _viewModel.battleInstance.LoadTurnOrder();


            #endregion
        }

        #region commands



        #endregion
        #region population of grid
        //Populate grid
        public void populateGrid()
        {
            if (_viewModel.DatasetCharacter.Count == 0)
            {
                Debug.WriteLine("Dataset not populated");
                throw new System.ArgumentException("Please Check SQL", "original");
            }
            else
            {
                _viewModel.battleInstance.TurnCharacter = _viewModel.battleInstance.ListCharacters[0];
                for (int i = 0; i < 2; i++) ///should edit/WHY IS THIS STAYING
                {
                    for (int j = 0; j < _viewModel.battleInstance.ListCharacters.Count; j++)
                    {

                        if (i == 0)
                        {
                            string CharacterImg = _viewModel.battleInstance.ListCharacters.ElementAt(j).EntityImage;

                            _viewModel.battleInstance.ListCharacters.ElementAt(j).X = i;//set x
                            _viewModel.battleInstance.ListCharacters.ElementAt(j).Y = j;//set y

                            Image Box = new Image { Source = CharacterImg, BackgroundColor = Xamarin.Forms.Color.Transparent }; //will replace with image of box
                            Grid.SetRow(Box, j);
                            Grid.SetColumn(Box, i);
                            this.ContentPanel.Children.Add(Box);
                        }


                    }
                }
            }

        }

        //insert monster's into grid
        public void insertMonster()
        {
            if (_viewModel.DatasetCharacter.Count == 0)
            {
                Debug.WriteLine("Dataset not populated");
                throw new System.ArgumentException("Please Check SQL", "original");
            } //prevent load if no characters

            //todo switch to highest speed on queue
            _viewModel.battleInstance.TurnMonster = _viewModel.battleInstance.ListMonsters[0];

            for (int i = 0; i < 2; i++) ///should edit/WHY IS THIS STAYING
            {
                for (int j = 0; j < _viewModel.battleInstance.ListMonsters.Count; j++)
                {

                    if (i == 1)
                    {
                        string MonsterImg = _viewModel.battleInstance.ListMonsters.ElementAt(j).EntityImage;
                        _viewModel.battleInstance.ListMonsters.ElementAt(j).X = i;//set x
                        _viewModel.battleInstance.ListMonsters.ElementAt(j).Y = j;//set y


                        Image Box = new Image { Source = MonsterImg, BackgroundColor = Xamarin.Forms.Color.Transparent }; //will replace with image of box
                        Grid.SetRow(Box, j);
                        Grid.SetColumn(Box, i);
                        this.ContentPanel.Children.Add(Box);
                    }


                }
            }
        }


        //reload entire page character/ monstersr4
        public void reloadEntities()
        {
            ClearBattlePage();
            SetBackground();

            if (_viewModel.battleInstance.ListCharacters.Count != 0 && _viewModel.battleInstance.ListMonsters.Count != 0)
            {

                for (int i = 0; i < 2; i++) ///should edit/WHY IS THIS STAYING
                {
                    for (int j = 0; j < _viewModel.battleInstance.ListCharacters.Count; j++)
                    {
                        if (_viewModel.battleInstance.ListCharacters.ElementAt(j).IsLiving())
                        {
                            string characterImg = _viewModel.battleInstance.ListCharacters.ElementAt(j).EntityImage;
                            int xcoord = _viewModel.battleInstance.ListCharacters.ElementAt(j).X;
                            int ycoord = _viewModel.battleInstance.ListCharacters.ElementAt(j).Y;


                            Image Box = new Image { Source = characterImg }; //will replace with image of box
                            Grid.SetRow(Box, ycoord);
                            Grid.SetColumn(Box, xcoord);
                            this.ContentPanel.Children.Add(Box);

                            //L

                        }

                    }
                }
                for (int i = 0; i < 2; i++) ///should edit/WHY IS THIS STAYING
                {
                    for (int j = 0; j < _viewModel.battleInstance.ListMonsters.Count; j++)
                    {

                        if (_viewModel.battleInstance.ListMonsters.ElementAt(j).IsLiving())
                        {
                            string MonsterImg = _viewModel.battleInstance.ListMonsters.ElementAt(j).EntityImage;
                            int xcoord = _viewModel.battleInstance.ListMonsters.ElementAt(j).X;
                            int ycoord = _viewModel.battleInstance.ListMonsters.ElementAt(j).Y;


                            Image Box = new Image { Source = MonsterImg, }; //will replace with image of box
                            Grid.SetRow(Box, ycoord);
                            Grid.SetColumn(Box, xcoord);
                            this.ContentPanel.Children.Add(Box);
                        }
                    }
                }
                insertGrid();

            }


        }

        #endregion
        #region ui filler
       
        //clear battle gird
        public void ClearBattlePage()
        {
            this.ContentPanel.Children.Clear();
        }
        //set background
        public void SetBackground()
        {
            Image Box = new Image
            {
                Source = "DungeonCave.jpg",
                HorizontalOptions = Xamarin.Forms.LayoutOptions.Fill,
                VerticalOptions = Xamarin.Forms.LayoutOptions.Fill,
                Aspect = Xamarin.Forms.Aspect.Fill
            };
            Grid.SetColumn(Box, 0);
            Grid.SetColumnSpan(Box, 2);
            Grid.SetRow(Box, 0);
            Grid.SetRowSpan(Box, 6);

            this.ContentPanel.Children.Add(Box);
        }
        //refresh the entire page
        public async void RoundRefreshPage() //REFRESH After Round
        {
            if (_viewModel.battleInstance.ListCharacters.Count <= 0)
            {
                _viewModel.battleInstance.EndGame();
                var data = new ScoreDetailViewModel(_viewModel.battleInstance.score);
                Debug.WriteLine("here is experience sent to page" + data.Data.ExperienceGainedTotal);
                Debug.WriteLine("here is the turns sent to page" + data.Data.Turn);
                await Navigation.PushModalAsync(new EndGamePage(data));
            }
            
            //clear battlepage
            ClearBattlePage();
            // reload map
            SetBackground();
            //Updated Message
            OutputMonster.Text = _viewModel.checkMonsterMessage();
            OutputCharacter.Text = _viewModel.checkCharacterMessage();
            //new 6 monster rewspawn
            _viewModel.refreshMonster();

            //reload current players in DatasetCharacter
            populateGrid();
            //load new monsters
            insertMonster();
            insertGrid();
            //calls for ew loading of monster turns
            _viewModel.battleInstance.LoadTurnOrder();
            _viewModel.battleInstance.numberMonsterDead = 0;
            _viewModel.battleInstance.checkTurns();
            //Update Rest of UI
            RoundNum.Text = _viewModel.getRound();
            Debug.WriteLine("Round " + _viewModel.getRound());

        }
        //Button Selected_______________________________________________________
        //on select choose monster
        public async void SelectedBox(object sender, EventArgs args)
        {
            reloadEntities();
            //var answer = await DisplayAlert("Fight", "Battle Ready?", "Yes", "No");
            Button getter = (Button)sender;
            Monster getMonster = _viewModel.DatasetMonster.FirstOrDefault(a => a.Y.ToString() == getter.ClassId); //a.X == getter.X &&
            if (getMonster != null)
            {
                getter.BackgroundColor = Xamarin.Forms.Color.Red;
                _viewModel.battleInstance.TurnMonster = getMonster;
                Debug.WriteLineIf(getMonster == null, " empty Monster in Selected box");
                var answer = await DisplayAlert("Fight", getMonster.Name + " ", "Yes", "No");
            }
            else
            {
                var answer = DisplayAlert("Empty", "Select a Monster ", "ok");
            }

        }

        //Buttons for pressing_______________________________________________________
        //places grid 
        public void insertGrid()
        {

            for (int i = 0; i < 2; i++) ///should edit/WHY IS THIS STAYING
            {
                for (int j = 0; j < 6; j++)
                {
                    {
                        if (i == 1)
                        {
                            Button button = new Button();
                            button.HorizontalOptions = Xamarin.Forms.LayoutOptions.Fill;
                            button.VerticalOptions = LayoutOptions.Fill;
                            button.ClassId = j.ToString();
                            button.Clicked += new EventHandler(SelectedBox);
                            //button.BackgroundColor = Color.Transparent;
                            Grid.SetRow(button, j);
                            Grid.SetColumn(button, i);
                            this.ContentPanel.Children.Add(button);
                        }
                    }
                }
            }

            #endregion
        }
        //____________________________________________________
        protected override void OnAppearing()
        {
            base.OnAppearing();

            BindingContext = null;

            if (ToolbarItems.Count > 0)
            {
                ToolbarItems.RemoveAt(0);
            }

            InitializeComponent();

            //LOAD datasets
            if (_viewModel.getRound() == "1")
            {
                if (_viewModel.DatasetMonster.Count == 0 || _viewModel.DatasetCharacter.Count == 0)
                {
                    _viewModel.LoadDataCommand.Execute(null);

                }
                else if (_viewModel.NeedsRefresh())
                {
                    _viewModel.LoadDataCommand.Execute(null);
                }
            }
            BindingContext = _viewModel;
            RoundNum.Text = _viewModel.getRound(); //text for round
            GridSetup();

        }
    }
}