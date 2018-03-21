using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;
using DeathDungeon.Models;
using DeathDungeon.Views;
using System.Linq;

namespace DeathDungeon.ViewModels
{
    public class CharactersViewModel : BaseViewModel
    {
        // Make this a singleton so it only exist one time because holds all the data records in memory
        private static CharactersViewModel _instance;
        public Random randomCharacter = new Random();
        //__________________________INSTANCE_____________________________________________
        public static CharactersViewModel Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new CharactersViewModel();
                }
                return _instance;
            }
        }

        public ObservableCollection<Character> Dataset { get; set; }

        public List<string> PartyList { get; set; }

        public ObservableCollection<Character> DatasetParty { get; set; }

        public Command LoadDataCommand { get; set; }

        private bool _needsRefresh;

        public CharactersViewModel()
        {

            Title = "Character List";
            Dataset = new ObservableCollection<Character>();
            PartyList = new List<string>();
            DatasetParty = new ObservableCollection<Character>();
            
            LoadDataCommand = new Command(async () => await ExecuteLoadDataCommand());
            //___________________________CRUD SUB_______________________________________________
            MessagingCenter.Subscribe<DeleteCharacterPage, Character>(this, "DeleteData", async (obj, data) =>
            {
                Dataset.Remove(data);
                await DataStore.DeleteAsync_Character(data);
            });

            MessagingCenter.Subscribe<NewCharacterPage, Character>(this, "AddData", async (obj, data) =>
            {
                Dataset.Add(data);
                await DataStore.AddAsync_Character(data);
            });

            MessagingCenter.Subscribe<EditCharacterPage, Character>(this, "EditData", async (obj, data) =>
            {

                // Find the Item, then update it
                var myData = Dataset.FirstOrDefault(arg => arg.Id == data.Id);
                if (myData == null)
                {
                    return;
                }

                myData.Update(data);
                await DataStore.UpdateAsync_Character(data);

                _needsRefresh = true;

            });
        }
        //____________________________________________________________________________________
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
                DatasetParty.Clear();
                var dataset = await DataStore.GetAllAsync_Character(true);
                dataset = dataset
                    .OrderBy(a => a.Level)
                    .ThenBy(a => a.Name)
                    .ThenBy(a => a.Speed)
                    .ThenByDescending(a => a.MaximumHealth)
                    .ToList();

               // var datasett = await DataStore.GetPartyAsync_Character(true);
                foreach (var data in dataset)
                {
                    
                    Dataset.Add(data);
                    
                }
                Dataset= new ObservableCollection<Character>(Dataset.OrderBy(a => a.Level)
                    .ThenBy(a => a.Name)
                    .ThenBy(a => a.Speed)
                    .ThenByDescending(a => a.MaximumHealth)
                    .ToList());
                foreach (var data in dataset)
                {
                   if (PartyList.Contains(data.Id))
                   {
                        DatasetParty.Add(data);
                   }
                    //DatasetParty = new ObservableCollection<Character>(DatasetParty.OrderBy(a => a.Level)
                    //.ThenBy(a => a.Name)
                    //.ThenBy(a => a.Speed)
                    //.ThenByDescending(a => a.MaximumHealth)
                    //.ToList());

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

        

        #region DataOperations

        public async Task<bool> AddAsync(Character data)
        {
            Dataset.Add(data);
            var myReturn = await DataStore.AddAsync_Character(data);
            return myReturn;
        }

        public async Task<bool> DeleteAsync(Character data)
        {
            Dataset.Remove(data);
            var myReturn = await DataStore.DeleteAsync_Character(data);
            return myReturn;
        }

        public async Task<bool> UpdateAsync(Character data)
        {
            // Find the Character, then update it
            var myData = Dataset.FirstOrDefault(arg => arg.Id == data.Id);
            if (myData == null)
            {
                return false;
            }

            myData.Update(data);
            await DataStore.UpdateAsync_Character(myData);

            _needsRefresh = true;

            return true;
        }

        // Call to database to ensure most recent
        public async Task<Character> GetAsync(string id)
        {
            var myData = await DataStore.GetAsync_Character(id);
            return myData;
        }

        #endregion DataOperations
        //__________________PARTY LOGIC_______________________________________________________
        //add to party
        public async Task<bool>AddToParty(Character character)
        {
            var myData = Dataset.FirstOrDefault(arg => arg.Id == character.Id);
            if (myData == null)
            {
                return false;
            }
            if (PartyList.Count < 6)
            {
                PartyList.Add(character.Id);
                DatasetParty.Add(character);
                _needsRefresh = true;
                return true;
            }
            return false;
        }
        //remove from party
        public async Task<bool> RemoveFromParty(Character character)
        {
            var myData = DatasetParty.FirstOrDefault(arg => arg.Id == character.Id);

            if (myData == null)
            {
                return false;
            }
            PartyList.Remove(character.Id);
            DatasetParty.Remove(character);
            _needsRefresh = true;
            return true;
        }
        //_________________AUTO BATTLE FILL CHARS__________________________________________________
        public async void FillParty()
        {
            if (Dataset.Count != 0)
            {
                Dataset.Clear();
                Debug.WriteLine("DatasetParty-Character was not empty when created");
            }
            for (int i = 0; i < 6; i++)
            {
                //await Task.Delay(1000);
                int charType = randomCharacter.Next(1, 7);//possible test
                Character _character = new Character(charType);
                Dataset.Add(_character);
                
                
            }
            _needsRefresh = true;
        }
        //____________________________________________________________________________________
    }
}
   
