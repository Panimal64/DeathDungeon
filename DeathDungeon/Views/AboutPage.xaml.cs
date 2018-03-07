
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

            SettingDataSource.IsToggled = false;
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
            }
            else
            {

                ItemsViewModel.Instance.SetDataStore(BaseViewModel.DataStoreEnum.Mock);
                MonstersViewModel.Instance.SetDataStore(BaseViewModel.DataStoreEnum.Mock);
                CharactersViewModel.Instance.SetDataStore(BaseViewModel.DataStoreEnum.Mock);
                ScoresViewModel.Instance.SetDataStore(BaseViewModel.DataStoreEnum.Mock);
             }

            // Have data refresh...
            ItemsViewModel.Instance.SetNeedsRefresh(true);
            MonstersViewModel.Instance.SetNeedsRefresh(true);
            CharactersViewModel.Instance.SetNeedsRefresh(true);
            ScoresViewModel.Instance.SetNeedsRefresh(true);
        }

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

        private async void GetItemsPost_Command(object sender, EventArgs e)
        {
            var number = 10; // Want to get 10 items from the server
            var level = 20;  // Wanted to get a range of items up to maximum level
            var attribute = AttributeEnum.Speed;   //Want to get only items that increase speed
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

    }
}