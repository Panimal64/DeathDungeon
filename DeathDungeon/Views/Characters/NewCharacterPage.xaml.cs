using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using DeathDungeon.Models;

namespace DeathDungeon.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NewCharacterPage : ContentPage
    {
        public Character Data { get; set; }

        public NewCharacterPage()
        {
            InitializeComponent();

            Data = new Character
            {
                Name = "Character Name",
                classType = 0,
                Level = 1,
                CurrentExperience = 0,
                MaximumHealth = 10,
                CurrentHealth = 10,
                Attack = 10,
                Defense = 10,
                Speed = 1,
                Description = "This is a Character description.",
                Id = Guid.NewGuid().ToString()
            };

            BindingContext = this;
        }

        public async void Save_Clicked(object sender, EventArgs e)
        {
            MessagingCenter.Send(this, "AddData", Data);
            await Navigation.PopAsync();
        }

        private async void Cancel_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
    }
}