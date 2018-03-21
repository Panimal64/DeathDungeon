using System;

using DeathDungeon.Models;
using DeathDungeon.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DeathDungeon.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class EditCharacterPage : ContentPage
	{
	    // ReSharper disable once NotAccessedField.Local
	    private CharacterDetailViewModel _viewModel;

        public Character Data { get; set; }

        public EditCharacterPage(CharacterDetailViewModel viewModel)
        {
            // Save off the item
            Data = viewModel.Data;
            viewModel.Title = "Edit " + viewModel.Title;

            InitializeComponent();
            

            // Set the data binding for the page
            BindingContext = _viewModel = viewModel;
            ClassPicker.SelectedItem = Data.classType.ToString();

        }

	    public async void Save_Clicked(object sender, EventArgs e)
        {
            MessagingCenter.Send(this, "EditData", Data);

            // removing the old ItemDetails page, 2 up counting this page
            Navigation.RemovePage(Navigation.NavigationStack[Navigation.NavigationStack.Count - 2]);

            // Add a new items details page, with the new Item data on it
            await Navigation.PushAsync(new CharacterDetailPage(new CharacterDetailViewModel(Data)));

            // Last, remove this page
            Navigation.RemovePage(this);
        }

	    private async void Cancel_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
        void Level_OnStepperValueChanged(object sender, ValueChangedEventArgs e)
        {
            LevelValue.Text = String.Format("{0}", e.NewValue);
        }

        // The stepper function for Value
        //void Value_OnStepperValueChanged(object sender, ValueChangedEventArgs e)
        //{
        //    ValueValue.Text = String.Format("{0}", e.NewValue);
        //}
    }
}