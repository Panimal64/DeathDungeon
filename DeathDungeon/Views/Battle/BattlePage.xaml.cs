using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        //private BattleClass battleInstance;

        public BattlePage()
        {
            InitializeComponent();

            BindingContext = _viewModel = BattleViewModel.Instance;

        }

        public async void Refresh_Clicked(object sender, SelectedItemChangedEventArgs args)
        {

            _viewModel.SetNeedsRefresh(true);
        }

        //On SELECTED ITEMS_____________________________________________________
        public async void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            var data = args.SelectedItem as Monster;
            if (data == null)
            {
                return;
            }
            var answer = await DisplayAlert("Fight", "Battle Ready?", "Yes", "No");
            if (answer)
            {
                // Call to the Item Service and have it Get the Items
                //ItemsController.Instance.GetItemsFromServer();
                //BattleClass.Instance.
                _viewModel.DatasetTurnMonster = data;

            }
            //await Navigation.PushAsync(new MonsterDetailPage(new MonstersDetailViewModel(data)));
            await DisplayAlert("Fight", _viewModel.DatasetTurnMonster.Name, "Yes", "No");
            // Manually deselect item.
            MonsterListView.SelectedItem = null;
        }

        public async void ItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            var data = args.SelectedItem as Character;
            if (data == null)
            {
                return;
            }

            //await Navigation.PushAsync(new CharacterDetailPage(new CharacterDetailViewModel(data)));
            var answer = await DisplayAlert("Fight", "Battle Ready?", "Yes", "No");
            if (answer)
            {
                // Call to the Item Service and have it Get the Items
                //ItemsController.Instance.GetItemsFromServer();
                //BattleClass.Instance.
                _viewModel.DatasetTurnCharacter = data;

            }

            await DisplayAlert("Fight", _viewModel.DatasetTurnCharacter.Name, "Yes", "No");

            // Manually deselect item.
            CharacterListView.SelectedItem = null;
        }
        //Buttons---------------------------------------------------------
        //Start Fight BUTTON func
        public async void CharacterFight_Clicked(object sender, SelectedItemChangedEventArgs args)
        {
            
            if (_viewModel.DatasetCharacter.Count <= 0 || _viewModel.DatasetMonster.Count <= 0)
            {
                if (_viewModel.DatasetCharacter.Count <= 0)
                {
                    _viewModel.battleInstance.EndGame();

                    await Navigation.PushAsync(new EndGamePage());
                }
                else 
                {
                    _viewModel.battleInstance.EndRound();
                    await Navigation.PushAsync(new EndRoundPage());


                }

            }
            else if (_viewModel.DatasetTurnCharacter == null || _viewModel.DatasetTurnMonster == null)
            {
                if (_viewModel.DatasetTurnCharacter == null)
                {
                    
                    _viewModel.DatasetTurnCharacter = _viewModel.DatasetCharacter[0];
                    
                }
                if (_viewModel.DatasetTurnMonster == null)
                {
                    
                    _viewModel.DatasetTurnMonster = _viewModel.DatasetMonster[0];
                    
                }
                _viewModel.battleInstance.CharacterAttacks(_viewModel.DatasetTurnCharacter, _viewModel.DatasetTurnMonster);


            }
            else
            {
                _viewModel.battleInstance.CharacterAttacks(_viewModel.DatasetTurnCharacter, _viewModel.DatasetTurnMonster);
            }


            //await DisplayAlert("Character Attacks", _viewModel.DatasetTurnMonster.Name, "Ok");
            //Output.Text = ("Character Attacks " + _viewModel.DatasetTurnMonster.Name);
            //Output.IsVisible = true;

            if (_viewModel.DatasetTurnCharacter != null && _viewModel.DatasetTurnMonster != null)
            {
                if (_viewModel.DatasetTurnMonster.IsLiving())
                {
                    Output.Text = (_viewModel.DatasetTurnCharacter.Name + " attacks " + _viewModel.DatasetTurnMonster.Name + " HP Remaining: " + _viewModel.DatasetTurnMonster.CurrentHealth);
                    Output.IsVisible = true;

                    //await DisplayAlert("Monster:", _viewModel.DatasetTurnMonster.Name
                    //+ " has " + _viewModel.DatasetTurnMonster.CurrentHealth + "HP"
                    //                   , "Ok");
                }
                else
                {
                    Output.Text = (_viewModel.DatasetTurnCharacter.Name + " attacks " + _viewModel.DatasetTurnMonster.Name + " Monster Died");
                    Output.IsVisible = true;

                    //await DisplayAlert("Monster:", _viewModel.DatasetTurnMonster.Name
                    //+ " has died", "Ok");

                    //make this a quick function
                    MessagingCenter.Send(this, "DeleteDataMonster", _viewModel.DatasetTurnMonster);
                    _viewModel.DatasetTurnMonster = null;

                }
            }

        }

        public async void MonsterFight_Clicked(object sender, SelectedItemChangedEventArgs args)
        {
            if (_viewModel.DatasetCharacter.Count <= 0 || _viewModel.DatasetMonster.Count <= 0)
            {
                if (_viewModel.DatasetCharacter.Count <= 0)
                {
                    _viewModel.battleInstance.EndGame();

                    await Navigation.PushAsync(new EndGamePage());
                }
                else
                {
                    _viewModel.battleInstance.EndRound();
                    await Navigation.PushAsync(new EndRoundPage());


                }

            }
            else if (_viewModel.DatasetTurnCharacter == null || _viewModel.DatasetTurnMonster == null)
            {
                if (_viewModel.DatasetTurnCharacter == null)
                {

                    _viewModel.DatasetTurnCharacter = _viewModel.DatasetCharacter[0];

                }
                if (_viewModel.DatasetTurnMonster == null)
                {

                    _viewModel.DatasetTurnMonster = _viewModel.DatasetMonster[0];

                }
                _viewModel.battleInstance.MonsterAttacks(_viewModel.DatasetTurnCharacter, _viewModel.DatasetTurnMonster);

            }
            else
            {
                _viewModel.battleInstance.MonsterAttacks(_viewModel.DatasetTurnCharacter, _viewModel.DatasetTurnMonster);
            }


            //await DisplayAlert("Fight", _viewModel.DatasetTurnMonster.Name + " " + _viewModel.DatasetTurnCharacter.Name, "Yes", "No");

            // _viewModel.battleInstance.MonsterAttacks(_viewModel.DatasetTurnCharacter, _viewModel.DatasetTurnMonster);
            //await DisplayAlert("Monster Attacks", _viewModel.DatasetTurnCharacter.Name, "Ok");
            if (_viewModel.DatasetTurnCharacter != null && _viewModel.DatasetTurnMonster != null)
            {
                if (_viewModel.DatasetTurnCharacter.IsLiving())
                {
                    Output.Text = (_viewModel.DatasetTurnMonster.Name + " attacks " + _viewModel.DatasetTurnCharacter.Name + " HP Remaining: " + _viewModel.DatasetTurnCharacter.CurrentHealth);
                    Output.IsVisible = true;

                    //await DisplayAlert("Character:", _viewModel.DatasetTurnCharacter.Name
                    //+ " has " + _viewModel.DatasetTurnCharacter.CurrentHealth + "HP"
                    //                   , "Ok");
                }
                else
                {
                    Output.Text = (_viewModel.DatasetTurnMonster.Name + " attacks " + _viewModel.DatasetTurnCharacter.Name + " Character Died");
                    Output.IsVisible = true;

                    //await DisplayAlert("Character:", _viewModel.DatasetTurnCharacter.Name
                    //+ " has died", "Ok");

                    //make this a quick function
                    MessagingCenter.Send(this, "DeleteDataCharacter", _viewModel.DatasetTurnCharacter);
                    _viewModel.DatasetTurnCharacter = null;
                }
            }
        }

        //Page Setting---------------------------------------------------------
        protected override void OnAppearing()
        {
            base.OnAppearing();

            BindingContext = null;

            if (ToolbarItems.Count > 0)
            {
                ToolbarItems.RemoveAt(0);
            }

            InitializeComponent();

            if (_viewModel.DatasetMonster.Count == 0 || _viewModel.DatasetCharacter.Count == 0)
            {
                _viewModel.LoadDataCommand.Execute(null);
            }
            else if (_viewModel.NeedsRefresh())
            {
                _viewModel.LoadDataCommand.Execute(null);
            }

            BindingContext = _viewModel;
            //ADJUSTING OR POPULATING GRID
            populateGrid();
            insertGuy();//test cyclops entry
            insertGrid();//test grid
        }

        //
        public void populateGrid()
        {
        for (int i = 0; i< 2; i++) ///should edit/WHY IS THIS STAYING
            {
                for (int j = 0; j< 6; j++)
                {
                    Image Box = new Image { Source = "wizard.png" }; //will replace with image of box
                    Grid.SetRow(Box, j);
                    Grid.SetColumn(Box, i);
                    this.ContentPanel.Children.Add(Box);
                }
            }   
        }
        //test for button_______________________________________________________

        public void insertGrid()
        {
            
            for (int i = 0; i < 2; i++) ///should edit/WHY IS THIS STAYING
            {
                for (int j = 0; j < 6; j++)
                {
                    Button button = new Button();
                    //button.BackgroundColor = Color.Transparent;
                    Grid.SetRow(button, j);
                    Grid.SetColumn(button, i);
                    this.ContentPanel.Children.Add(button);
                }
            }
        }
        //TEST Insert Character_________________________________________________

        public void insertGuy()
        {
            for (int i = 0; i < 2; i++) ///should edit/WHY IS THIS STAYING
            {
                for (int j = 0; j < 6; j++)
                {
                    if (i == 1)
                    {
                        Image Box = new Image { Source = "cyclops.png" }; //will replace with image of box
                                                                          //var child = this.ContentPanel.Children.Remove
                                                                          // this.ContentPanel.Children.Remove(child);
                        //REMOVING CHILD
                        for (int k = 0; k < this.ContentPanel.Children.Count(); k++)
                        {
                            var child = this.ContentPanel.Children[k];
                            if (Grid.GetRow(child) == j
                            &&
                            Grid.GetColumn(child) == i)
                            {
                                this.ContentPanel.Children.RemoveAt(k);
                            }
                        }

                        Grid.SetRow(Box, j);
                        Grid.SetColumn(Box, i);
                        this.ContentPanel.Children.Add(Box);
                    }
                }
            }
        }
    }


}
