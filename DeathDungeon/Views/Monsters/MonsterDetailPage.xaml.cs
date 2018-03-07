using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using DeathDungeon.Models;
using DeathDungeon.ViewModels;

namespace DeathDungeon.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    // ReSharper disable once RedundantExtendsListEntry
    public partial class MonsterDetailPage : ContentPage
    {
        // ReSharper disable once FieldCanBeMadeReadOnly.Local
        private MonstersDetailViewModel _viewModel;

        public MonsterDetailPage(MonstersDetailViewModel viewModel)
        {
            InitializeComponent();

            BindingContext = _viewModel = viewModel;
        }

        public MonsterDetailPage()
        {
            InitializeComponent();

            var data = new Monster
            {
                Name = "Monster Name",
                Level = 1,
                CurrentExperience = 10,
                MaximumHealth = 10,
                CurrentHealth = 10,
                Attack = 1,
                Defense = 1,
                Speed = 1,
                Description = "This is an monster description."
            };

            _viewModel = new MonstersDetailViewModel(data);
            BindingContext = _viewModel;
        }

        private async void Edit_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new EditMonsterPage(_viewModel));
        }

        private async void Delete_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new DeleteMonsterPage(_viewModel));
        }

        private async void Cancel_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
        private async void Recruit_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new RecruitMonsterPage(_viewModel));
        }
    }
}