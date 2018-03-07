using DeathDungeon.Models;
using DeathDungeon.ViewModels;
using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DeathDungeon.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RecruitMonsterPage : ContentPage
    {
        // ReSharper disable once NotAccessedField.Local
        private MonstersDetailViewModel _viewModel;

        public Monster Data { get; set; }

        public RecruitMonsterPage(MonstersDetailViewModel viewModel)
        {
            // Save off the item
            Data = viewModel.Data;
            viewModel.Title = "Recruit " + viewModel.Title;

            InitializeComponent();

            // Set the data binding for the page
            BindingContext = _viewModel = viewModel;
        }

        private async void Recruit_Clicked(object sender, EventArgs e)
        {
            MessagingCenter.Send(this, "RecruitData", Data);

            // Remove Item Details Page manualy
            Navigation.RemovePage(Navigation.NavigationStack[Navigation.NavigationStack.Count - 2]);

            await Navigation.PopAsync();
        }
        private async void Discharge_Clicked(object sender, EventArgs e)
        {
            MessagingCenter.Send(this, "DischargeData", Data);

            // Remove Item Details Page manualy
            Navigation.RemovePage(Navigation.NavigationStack[Navigation.NavigationStack.Count - 2]);

            await Navigation.PopAsync();
        }


        private async void Cancel_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
    }
}