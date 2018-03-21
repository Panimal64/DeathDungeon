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
    public class ItemDropViewModel : BaseViewModel
    {
        private static ItemDropViewModel _instance;

        private bool _needsRefresh;

        public static ItemDropViewModel Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new ItemDropViewModel();
                }
                return _instance;
            }
        }



        public ObservableCollection<Item> Dataset { get; set; }

        public ObservableCollection<Item> ItemSet { get; set; }

        public ObservableCollection<Character> DatasetCharacter { get; set; }

        public Command LoadDataCommand { get; set; }

        public ItemDropViewModel()
        {
            Title = "Item Pool";
            Dataset = new ObservableCollection<Item>();
            DatasetCharacter = new ObservableCollection<Character>();
            ItemSet = new ObservableCollection<Item>();
            LoadDataCommand = new Command(async () => await ExecuteLoadDataCommand());

        }

        Item itemSelected;


        public Item ItemSelected
        {
            get { return itemSelected; }
            set
            {
                if (itemSelected != value)
                {
                    itemSelected = value;
                    OnPropertyChanged();

                }
            }
        }

        Item poolSelected;


        public Item PoolSelected
        {
            get { return poolSelected; }
            set
            {
                if (poolSelected != value)
                {
                    poolSelected = value;
                    OnPropertyChanged();

                }
            }
        }
        Character characterSelected;

        public Character CharacterSelected
        {
            get { return characterSelected; }
            set
            {
                if (characterSelected != value)
                {
                    characterSelected = value;
                    OnPropertyChanged();

                }
            }
        }

        ItemLocationEnum locationSelected;

        public ItemLocationEnum LocationSelected
        {
            get { return locationSelected; }
            set
            {
                if(locationSelected != value)
                {
                    locationSelected = value;
                    
                  
                    OnPropertyChanged();
                }
            }
        }
        
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
        public void LoadData()
        {
            ExecuteLoadDataCommand();
        }

        private async Task ExecuteLoadDataCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                Dataset.Clear();
                DatasetCharacter.Clear();
                var dataset = BattleViewModel.Instance.Pool;
                var datasett = BattleViewModel.Instance.DatasetCharacter;

                foreach(var data in datasett)
                {
                    DatasetCharacter.Add(data);
                }

                foreach (var data in dataset)
                {
                    Dataset.Add(data);
                }

                Dataset = new ObservableCollection<Item>(Dataset.OrderBy(a => a.Name)
                    .ThenBy(a => a.Value)
                    .ThenBy(a => a.Attribute)
                    .ThenByDescending(a => a.Range)
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
        public void Back()
        {
            BattleViewModel.Instance.DatasetCharacter.Clear();
            BattleViewModel.Instance.Pool.Clear();
            foreach (var data in DatasetCharacter)
            {
                BattleViewModel.Instance.DatasetCharacter.Add(data);
                BattleViewModel.Instance.battleInstance.GetCharacters();

            }
            BattleViewModel.Instance.SpeedCheck();
            BattleViewModel.Instance.battleInstance.GetCharacters();
        }

        public void clean()
        {
            List<Item> clean = Dataset.ToList();
            Dataset.Clear();
            foreach (var data in clean.Where(x => x != null))
            {
               Dataset.Add(data);
            }
        }
    }
}
