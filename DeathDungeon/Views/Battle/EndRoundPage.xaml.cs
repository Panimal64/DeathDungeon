using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using DeathDungeon.Models;
using DeathDungeon.ViewModels;

namespace DeathDungeon.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class EndRoundPage : ContentPage
	{
        private ScoreDetailViewModel _viewModel;
        //___________________________________________________________
        public EndRoundPage(ScoreDetailViewModel viewModel)
        {
            
                InitializeComponent();

                BindingContext = _viewModel = viewModel;
            
        }
        //___________________________________________________________
        public EndRoundPage () //end roung page
		{
            InitializeComponent();

            var data = new Score
            {
                Name = "Score name",
                ScoreTotal = 0
            };

            _viewModel = new ScoreDetailViewModel(data);
            BindingContext = _viewModel;
        }
        //go coninue to game
        private async void Continue_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
    }
}

