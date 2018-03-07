 using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;

using Xamarin.Forms;

using DeathDungeon.Models;
using DeathDungeon.Views;
using System.Linq;

namespace DeathDungeon.ViewModels
{
    public class CharacterPartyViewModel : BaseViewModel
    {
        // Make this a singleton so it only exist one time because holds all the data records in memory
        private static CharacterPartyViewModel _instance;

        public static CharacterPartyViewModel Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new CharacterPartyViewModel();
                }
                return _instance;
            }
        }

        public ObservableCollection<Character> Dataset { get; set; }
        public Command LoadDataCommand { get; set; }

        private bool _needsRefresh;

        public CharacterPartyViewModel()
        {

            Title = "Character Party List";
            Dataset = new ObservableCollection<Character>();
            LoadDataCommand = new Command(async () => await ExecuteLoadDataCommand());

            //MessagingCenter.Subscribe<DeleteCharacterPage, Character>(this, "DeleteData", async (obj, data) =>
            //{
            //    Dataset.Remove(data);
            //    await DataStore.DeleteAsync_Character(data);
            //});
            MessagingCenter.Subscribe<RecruitCharacterPage, Character>(this, "RecruitData", async (obj, data) =>
            {

                _needsRefresh = true;
            });
            MessagingCenter.Subscribe<RecruitCharacterPage, Character>(this, "DischargeData", async (obj, data) =>
            {

                _needsRefresh = true;
            });
            MessagingCenter.Subscribe<BattlePage, Character>(this, "DeleteDataCharacter", async (obj, data) =>
            {

                _needsRefresh = true;
            });
            //MessagingCenter.Subscribe<NewCharacterPage, Character>(this, "AddData", async (obj, data) =>
            //{
            //    Dataset.Add(data);
            //    await DataStore.AddAsync_Character(data);
            //});

            //MessagingCenter.Subscribe<EditCharacterPage, Character>(this, "EditData", async (obj, data) =>
            //{

            //    // Find the Item, then update it
            //    var myData = Dataset.FirstOrDefault(arg => arg.Id == data.Id);
            //    if (myData == null)
            //    {
            //        return;
            //    }

            //    myData.Update(data);
            //    await DataStore.UpdateAsync_Character(data);

            //    _needsRefresh = true;

            //});
        }

        // Return True if a refresh is needed
        // It sets the refresh flag to false
        public bool NeedsRefresh()
        {
            if (_needsRefresh)
            {
                _needsRefresh = false;
                return true;
            }
            return false;
        }

        // Sets the need to refresh
        public void SetNeedsRefresh(bool value)
        {
            _needsRefresh = value;
        }

        private async Task ExecuteLoadDataCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                Dataset.Clear();
                var dataset = await DataStore.GetPartyAsync_Character(true);
                foreach (var data in dataset)
                {
                    Dataset.Add(data);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}