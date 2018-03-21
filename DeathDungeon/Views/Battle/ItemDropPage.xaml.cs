using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using DeathDungeon.ViewModels;
using DeathDungeon.Models;


namespace DeathDungeon.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ItemDropPage : ContentPage
	{
        //___________VARIABLES_____________________________________
        private ItemDropViewModel _viewModel;

        public List<Item> PoolSet { get; set; }

        public List<Item> LocationSet { get; set; } 
        public List<Character> DatasetCharacter { get; set; }
        List<Item> list;
        string player;
        string mode;
        Character temp = new Character();
        //__________INIT_____________________________________
        public ItemDropPage()
        {
            
            InitializeComponent();
            BindingContext = _viewModel = ItemDropViewModel.Instance;
            
            //Dataset = _viewModel.Dataset.ToList();
            DatasetCharacter = _viewModel.DatasetCharacter.ToList();
            PoolSet = new List<Item>( _viewModel.Dataset.ToList());
            LocationSet = new List<Item>();
        }

        public ItemDropPage(List<Item> Pool)
        {

            InitializeComponent();
            BindingContext = _viewModel = ItemDropViewModel.Instance;

            ItemDropViewModel.Instance.LoadData();
            DatasetCharacter = _viewModel.DatasetCharacter.ToList();
            PoolSet = new List<Item>(Pool);
            LocationSet = new List<Item>();
        }
        //__________UI ____________________________________
        //character is slected 
        void OnCharacterChosen(object sender, EventArgs args)
        {
            _viewModel.NeedsRefresh();
            Character temp = (Character)CharacterPicker.SelectedItem;
            if (temp == null)
            {
                return;
            }
            player = temp.Id.ToString();
            LocationPicker.IsEnabled = true;

        }
        //location chosen
        void OnLocChosen(object sender, EventArgs args)
        {
            Picker modePicker = (Picker)sender;

            mode = modePicker.SelectedItem.ToString();
            rolePicker.IsEnabled = false;
            rolePicker.ItemsSource = null;
            rolePicker.Items.Clear();

           Character temp = _viewModel.DatasetCharacter.FirstOrDefault(a => a.Id == player);
            List<Item> templist;
            
            CurrentItem.Text = temp.GetItemAtLocation((ItemLocationEnum)Enum.Parse(typeof(ItemLocationEnum), mode));

            LocationSet.Clear();
            PoolSet = PoolSet.Where(a => a != null).ToList();
            if (mode == ItemLocationEnum.LeftFinger.ToString() || mode == ItemLocationEnum.RightFinger.ToString())
            {
                templist = PoolSet.FindAll(a => a.Location.ToString() == mode || a.Location.ToString() == "Finger");
            }
            else
            {
                templist = PoolSet.FindAll(a => a.Location.ToString() == mode);
            }
            foreach (var data in  templist)
            {
                LocationSet.Add(data);
            }
            rolePicker.ItemsSource = LocationSet;
            rolePicker.IsEnabled = true;
            
        }
        //items select
        void OnItemChosen(object sender, EventArgs args)
        {
            Picker picker = (Picker)sender;
            Item temp = (Item)picker.SelectedItem;
            if (temp == null)
            {
                return;
            }
            var movitem = _viewModel.DatasetCharacter.FirstOrDefault(a => a.Id == player).RemoveItem((ItemLocationEnum)Enum.Parse(typeof(ItemLocationEnum), mode));

            PoolSet.Add(movitem);
            _viewModel.Dataset.Add(movitem);
            _viewModel.DatasetCharacter.FirstOrDefault(a => a.Id == player).AddItem((ItemLocationEnum)Enum.Parse(typeof(ItemLocationEnum), mode), temp.Id );
            PoolSet.Remove(temp);
            _viewModel.Dataset.Remove(temp);
            _viewModel.clean();
            PoolSet =PoolSet.Where(x => x != null).ToList();
            CurrentItem.Text = _viewModel.DatasetCharacter.FirstOrDefault(a => a.Id == player).GetItemAtLocation((ItemLocationEnum)Enum.Parse(typeof(ItemLocationEnum), mode));
            RemoveItem.IsEnabled = true;
        }
        //remove item 
        private async void RemoveItems_Command(object sender, EventArgs e)
        {
            var movitem = _viewModel.DatasetCharacter.FirstOrDefault(a => a.Id == player).RemoveItem((ItemLocationEnum)Enum.Parse(typeof(ItemLocationEnum), mode));
            PoolSet.Add(movitem);
            _viewModel.Dataset.Add(movitem);
            _viewModel.clean();
            CurrentItem.Text = _viewModel.DatasetCharacter.FirstOrDefault(a => a.Id == player).GetItemAtLocation((ItemLocationEnum)Enum.Parse(typeof(ItemLocationEnum), mode));

            RemoveItem.IsEnabled = true;
        }
        //go back to game
        private async void Back_Clicked(object sender, EventArgs e)
        {
            _viewModel.Back();
            await Navigation.PopModalAsync();
            Navigation.RemovePage(this);
        }
        //___________________________________________________________
        protected override void OnAppearing()
        {
            base.OnAppearing();

            BindingContext = null;

            if (ToolbarItems.Count > 0)
            {
                ToolbarItems.RemoveAt(0);
            }

            InitializeComponent();

            if (_viewModel.Dataset.Count == 0||_viewModel.DatasetCharacter.Count ==0)
            {
                _viewModel.LoadDataCommand.Execute(null);
            }
            else if (_viewModel.NeedsRefresh())
            {
                _viewModel.LoadDataCommand.Execute(null);
            }

            BindingContext = _viewModel;
        }







    }
}