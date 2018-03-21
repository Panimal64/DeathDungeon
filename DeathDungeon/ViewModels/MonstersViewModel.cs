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
    public class MonstersViewModel : BaseViewModel
    {
        // Make this a singleton so it only exist one time because holds all the data records in memory
        private static MonstersViewModel _instance;

        public Random randomMonster = new Random();

        public static MonstersViewModel Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new MonstersViewModel();
                }
                return _instance;
            }
        }

        public ObservableCollection<Monster> Dataset { get; set; }
        public ObservableCollection<Monster> DatasetParty{ get; set; }
        public Command LoadDataCommand { get; set; }

        private bool _needsRefresh;

        public MonstersViewModel()
        {
            Title = "Monster List";
            Dataset = new ObservableCollection<Monster>();
            //________________________CRUD SUB________________________________________________
            LoadDataCommand = new Command(async () => await ExecuteLoadDataCommand());

            MessagingCenter.Subscribe<DeleteMonsterPage, Monster>(this, "DeleteData", async (obj, data) =>
            {
                Dataset.Remove(data);
                await DataStore.DeleteAsync_Monster(data);
            });
           
           
            MessagingCenter.Subscribe<NewMonsterPage, Monster>(this, "AddData", async (obj, data) =>
            {
                Dataset.Add(data);
                await DataStore.AddAsync_Monster(data);
            });
            MessagingCenter.Subscribe<BattlePage, Monster>(this, "DeleteDataMonster", async (obj, data) =>
            {

                _needsRefresh = true;
            });
            MessagingCenter.Subscribe<EditMonsterPage, Monster>(this, "EditData", async (obj, data) =>
            {
                // Find the Monster, then update it
                var myData = Dataset.FirstOrDefault(arg => arg.Id == data.Id);
                if (myData == null)
                {
                    return;
                }

                myData.Update(data);
                await DataStore.UpdateAsync_Monster(myData);

                _needsRefresh = true;

            });
        }

        //_________________MONSTER GENERATOR_________________________________________________
        //POPULATE RANDOM MONSTERS___________________
        //possible gloal round int round
        public void newRoundMonsters()
        {
            if (Dataset.Count != 0) {
                Dataset.Clear();
                Debug.WriteLine("Dataset was not empty when new round Monster appear");
            }
            for (int i = 0; i < 6; i++)
            {
                
                int monsterType = randomMonster.Next(1, 16);//possible test
                Monster _monster = new Monster(monsterType);
                Dataset.Add(_monster);

            }
        }
        //____________________________________________________________________________________
        //END POPULTATE MONSTER_____________________________
        // Return True if a refresh is needed
        // It sets the refresh flag to false
        public bool NeedsRefresh()
        {
            if (!_needsRefresh)
            {
                return false;
            }

            _needsRefresh = false;
            return true;
        }

        // Sets the need to refresh
        public void SetNeedsRefresh(bool value)
        {
            _needsRefresh = value;
        }

        private async Task ExecuteLoadDataCommand()
        {
            if (IsBusy)
            {
                return;
            }

            IsBusy = true;

            try
            {
                Dataset.Clear();
                

                var dataset = await DataStore.GetAllAsync_Monster(true);
                // Example of how to sort the database output using a linq query.
                //Sort the list
                dataset = dataset
                    .OrderBy(a => a.Name)
                    .ThenBy(a => a.Level)
                    .ThenBy(a => a.Speed)
                    .ThenByDescending(a => a.MaximumHealth)
                    .ToList();

                
                foreach (var data in dataset)
                {
                    Dataset.Add(data);
                }
                Dataset = new ObservableCollection<Monster>(Dataset.OrderBy(a => a.Level)
                    .ThenBy(a => a.Name)
                    .ThenBy(a => a.Speed)
                    .ThenByDescending(a => a.MaximumHealth)
                    .ToList());
                
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

        #region DataOperations

        public async Task<bool> AddAsync(Monster data)
        {
            Dataset.Add(data);
            var myReturn = await DataStore.AddAsync_Monster(data);
            return myReturn;
        }

        public async Task<bool> DeleteAsync(Monster data)
        {
            Dataset.Remove(data);
            var myReturn = await DataStore.DeleteAsync_Monster(data);
            return myReturn;
        }

        public async Task<bool> UpdateAsync(Monster data)
        {
            // Find the Monster, then update it
            var myData = Dataset.FirstOrDefault(arg => arg.Id == data.Id);
            if (myData == null)
            {
                return false;
            }

            myData.Update(data);
            await DataStore.UpdateAsync_Monster(myData);

            _needsRefresh = true;

            return true;
        }

        // Call to database to ensure most recent
        public async Task<Monster> GetAsync(string id)
        {
            var myData = await DataStore.GetAsync_Monster(id);
            return myData;
        }

        #endregion DataOperations

    }
}