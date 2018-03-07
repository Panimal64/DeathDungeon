 using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DeathDungeon.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class OpeningPage : ContentPage
    {
        public OpeningPage()
        {
            InitializeComponent();
        }

        private async void UserBattle_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new CharacterPartyPage());
        }
        private async void AutoBattle_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new NewScorePage());
        }

    }
}