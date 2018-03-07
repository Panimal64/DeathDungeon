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
    public class MonsterPartyViewModel : BaseViewModel
    {
        // Make this a singleton so it only exist one time because holds all the data records in memory
        private static MonsterPartyViewModel _instance;

        public static MonsterPartyViewModel Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new MonsterPartyViewModel();
                }
                return _instance;
            }
        }

        public ObservableCollection<Monster> Dataset { get; set; }
        public Command LoadDataCommand { get; set; }

        private bool _needsRefresh;

        public MonsterPartyViewModel()
        {

            Title = "Monster Party List";
            Dataset = new ObservableCollection<Monster>();
            LoadDataCommand = new Command(async () => await ExecuteLoadDataCommand());

            //MessagingCenter.Subscribe<DeleteMonsterPage, Monster>(this, "DeleteData", async (obj, data) =>
            //{
            //    Dataset.Remove(data);
            //    await DataStore.DeleteAsync_Monster(data);
            //});
            MessagingCenter.Subscribe<RecruitMonsterPage, Monster>(this, "RecruitData", async (obj, data) =>
            {

                _needsRefresh = true;
            });
            MessagingCenter.Subscribe<RecruitMonsterPage, Monster>(this, "DischargeData", async (obj, data) =>
            {

                
                _needsRefresh = true;
            });
            MessagingCenter.Subscribe<BattlePage, Monster>(this, "DeleteDataMonster", async (obj, data) =>
            {

                _needsRefresh = true;
            });
            //MessagingCenter.Subscribe<NewMonsterPage, Monster>(this, "AddData", async (obj, data) =>
            //{
            //    Dataset.Add(data);
            //    await DataStore.AddAsync_Monster(data);
            //});

            //MessagingCenter.Subscribe<EditMonsterPage, Monster>(this, "EditData", async (obj, data) =>
            //{

            //    // Find the Item, then update it
            //    var myData = Dataset.FirstOrDefault(arg => arg.Id == data.Id);
            //    if (myData == null)
            //    {
            //        return;
            //    }

            //    myData.Update(data);
            //    await DataStore.UpdateAsync_Monster(data);

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
                var dataset = await DataStore.GetPartyAsync_Monster(true);
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