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
    public class BattleViewModel: BaseViewModel
    {
        private static BattleViewModel _instance;

        public static BattleViewModel Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new BattleViewModel();
                }
                return _instance;
            }
        }

        public BattleClass battleInstance { get; set; }
        public ObservableCollection<Monster> DatasetMonster { get; set; }
        public ObservableCollection<Character> DatasetCharacter { get; set; }
        //public ObservableCollection<Character> DatasetTurnCharacter { get; set; }
        //public ObservableCollection<Monster> DatasetTurnMonster { get; set; }
        public Character DatasetTurnCharacter { get; set; } 
        public Monster DatasetTurnMonster { get; set; }
        public Command LoadDataCommand { get; set; }

        private bool _needsRefresh;

        public BattleViewModel()
        {
            Title = "Battle";
            DatasetMonster = new ObservableCollection<Monster>();
            DatasetCharacter= new ObservableCollection<Character>();
            //DatasetTurnMonster = new ObservableCollection<Monster>();
            //DatasetTurnCharacter = new ObservableCollection<Character>();
            battleInstance = new BattleClass();
            LoadDataCommand = new Command(async () => await ExecuteLoadDataCommand());


            //crudi monster
            MessagingCenter.Subscribe<BattlePage, Monster>(this, "DeleteDataMonster", async (obj, data) =>
            {
                DatasetMonster.Remove(data);
                await DataStore.DeleteAsync_MonsterParty(data);
            });
            MessagingCenter.Subscribe<BattlePage, Character>(this, "DeleteDataCharacter", async (obj, data) =>
            {
                DatasetCharacter.Remove(data);
                await DataStore.DeleteAsync_CharacterParty(data);
            });

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

        private async Task ExecuteTurnClearCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                DatasetTurnMonster = null;
                DatasetTurnCharacter = null; 

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

    
        private async Task ExecuteLoadDataCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                DatasetMonster.Clear();
                DatasetCharacter.Clear();

                var dataset = await DataStore.GetPartyAsync_Monster(true);
                var datasett = await DataStore.GetPartyAsync_Character(true);

                foreach (var data in dataset)
                {
                    DatasetMonster.Add(data);
                }
                foreach (var data in datasett)
                {
                    DatasetCharacter.Add(data);
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

