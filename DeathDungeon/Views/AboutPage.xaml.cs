
using System;
using DeathDungeon.Services;
using DeathDungeon.Controllers;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using DeathDungeon.ViewModels;
using DeathDungeon.Models;

namespace DeathDungeon.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AboutPage : ContentPage
    {
        public AboutPage()
        {
            InitializeComponent();

            SQLText.IsVisible = false;
            DisableRandomText.IsVisible = false;
            MissText.IsVisible = false;
            CritText.IsVisible = false;
            SetHitText.IsVisible = false;
            VolcanoText.IsVisible = false;
           
            SettingDataSource.IsToggled = true;
            
        }

        private void Switch_OnToggled(object sender, ToggledEventArgs e)
        {
            // This will change out the DataStore to be the Mock Store if toggled off, or the SQL if on.

            if (e.Value == true)
            {
                ItemsViewModel.Instance.SetDataStore(BaseViewModel.DataStoreEnum.Sql);
                MonstersViewModel.Instance.SetDataStore(BaseViewModel.DataStoreEnum.Sql);
                CharactersViewModel.Instance.SetDataStore(BaseViewModel.DataStoreEnum.Sql);
                ScoresViewModel.Instance.SetDataStore(BaseViewModel.DataStoreEnum.Sql);
                BattleViewModel.Instance.SetDataStore(BaseViewModel.DataStoreEnum.Sql);
                ItemDropViewModel.Instance.SetDataStore(BaseViewModel.DataStoreEnum.Sql);
                if (GlobalVariables.auto_battle_reset == false)//sets global switch for auto to work
                {
                    GlobalVariables.auto_battle_reset = true;
                }
            }
            else
            {

                ItemsViewModel.Instance.SetDataStore(BaseViewModel.DataStoreEnum.Mock);
                MonstersViewModel.Instance.SetDataStore(BaseViewModel.DataStoreEnum.Mock);
                CharactersViewModel.Instance.SetDataStore(BaseViewModel.DataStoreEnum.Mock);
                ScoresViewModel.Instance.SetDataStore(BaseViewModel.DataStoreEnum.Mock);
                BattleViewModel.Instance.SetDataStore(BaseViewModel.DataStoreEnum.Mock);
                ItemDropViewModel.Instance.SetDataStore(BaseViewModel.DataStoreEnum.Mock);
                if (GlobalVariables.auto_battle_reset == false) //sets global switch for auto to work
                {
                    GlobalVariables.auto_battle_reset = true;
                }
            }
            //LOAD MONSTER FROM START
            ItemsViewModel.Instance.LoadDataCommand.Execute(null);
            MonstersViewModel.Instance.LoadDataCommand.Execute(null);
            CharactersViewModel.Instance.LoadDataCommand.Execute(null);
            ScoresViewModel.Instance.LoadDataCommand.Execute(null);
            BattleViewModel.Instance.LoadDataCommand.Execute(null);
            ItemDropViewModel.Instance.LoadDataCommand.Execute(null);

            // Have data refresh...
            ItemsViewModel.Instance.SetNeedsRefresh(true);
            MonstersViewModel.Instance.SetNeedsRefresh(true);
            CharactersViewModel.Instance.SetNeedsRefresh(true);
            ScoresViewModel.Instance.SetNeedsRefresh(true);
            BattleViewModel.Instance.SetNeedsRefresh(true);
            ItemDropViewModel.Instance.SetNeedsRefresh(true);
        }

        //clear current database
        private async void ClearDatabase_Command(object sender, EventArgs e)
        {
            var answer = await DisplayAlert("Delete", "Sure you want to Delete All Data, and start over?", "Yes", "No");
            if (answer)
            {
                // Call to the SQL DataStore and have it clear the tables.
                SQLDataStore.Instance.InitializeDatabaseNewTables();
            }
        }


        // Add code for GetItems_Command
        private async void GetItems_Command(object sender, EventArgs e)
        {
            var answer = await DisplayAlert("Get Items", "Would like like to import items from server?", "Yes", "No");
            if (answer)
            {
                // Call to the SQL DataStore and have it clear the tables.
                ItemsController.Instance.GetItemsFromServer();
            }
        }
        //gett post command for items
        private async void GetItemsPost_Command(object sender, EventArgs e)
        {
            var number = 100; // Want to get 10 items from the server
            var level = 20;  // Wanted to get a range of items up to maximum level
            var attribute = AttributeEnum.undefined ;   //Want to get only items that increase speed
            var location = ItemLocationEnum.undefined; //Wanted to get all possible locations, not just a single location such as feet
            var random = true;  //Wanted random items between level 1 and 20
            var updateDataBase = true;  //Update the database when new items are retrieved and not have duplicates

            var myDataList = await ItemsController.Instance.GetItemsFromServerPost(number, level, attribute, location, random, updateDataBase);

            var myOutput = string.Empty;
            foreach (var item in myDataList)
            {
                myOutput += item.FormatOutput() + "\n";
            }

            var answer = await DisplayAlert("Returned List", myOutput, "Yes", "No");
        }
        //enable debuger
        private void StartDebug(object sender, ToggledEventArgs e)
        {
            if (e.Value == true)
            {
                SettingDataSource.IsVisible = true;
                Get_Items.IsVisible = true;
                Post_Items.IsVisible = true;
                Clear_Database.IsVisible = true;
                SQLText.IsVisible = true;
                //DisableRandomText.IsVisible = true;
                //Disable_Random_Switch.IsVisible = true;
                //MissText.IsVisible = true;
                //Enable_Miss_Switch.IsVisible = true;
                //CritText.IsVisible = true;
                //Enable_Crit_Switch.IsVisible = true;
                //SetHitText.IsVisible = true;
                
            }
            else
            {
                SettingDataSource.IsVisible = false;
                Get_Items.IsVisible = false;
                Post_Items.IsVisible = false;
                Clear_Database.IsVisible = false;
                SQLText.IsVisible = false;
                //DisableRandomText.IsVisible = false;
                //Disable_Random_Switch.IsVisible = false;
                //MissText.IsVisible = false;
                //Enable_Miss_Switch.IsVisible = false;
                //CritText.IsVisible = false;
                //Enable_Crit_Switch.IsVisible = false;
                //SetHitText.IsVisible = false;
                
            }
        }

        //disables randoms
        private void NoRandom_OnToggled(object sender, ToggledEventArgs e)
        {
            if (e.Value == true)
            {
                GlobalVariables.ForceRollsToNotRandom = true;
                GlobalVariables.ForcedRandomValue = 1;
            }
            else
            {
                GlobalVariables.ForceRollsToNotRandom = false;
                GlobalVariables.ForcedRandomValue = 0;
            }
        }
        //miss toggler
        private void Miss_OnToggled(object sender, ToggledEventArgs e)
        {
            if (e.Value == true)
            {
                GlobalVariables.critMiss = true;
            }
            else
            {
                GlobalVariables.critMiss = false;
            }
        }
        //crit toggler
        private void Crit_OnToggled(object sender, ToggledEventArgs e)
        {
            if (e.Value == true)
            {
                GlobalVariables.critHit = true;
            }
            else
            {
                GlobalVariables.critHit = false;
            }
        }

       
        //hit toggle
        void Hit_Entry(object sender, EventArgs e)
        {
            GlobalVariables.ForceToHitValue = Convert.ToInt32(((Entry)sender).Text); //Send int to Global Variables Force to Hit
        }

    }
}