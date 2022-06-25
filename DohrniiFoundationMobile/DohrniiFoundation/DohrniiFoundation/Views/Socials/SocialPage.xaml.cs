using DohrniiFoundation.Models.Socials;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DohrniiFoundation.Views.Socials
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SocialPage : ContentPage
    {
        public SocialPage()
        {
            InitializeComponent();
        }

        protected override bool OnBackButtonPressed()
        {
            return true;
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            await socialVM.InitData();
        }

        private async void User_Tapped(object sender, EventArgs e)
        {
            var user = (AppUser)(sender as Frame).BindingContext;
            await socialVM.SendFriendRequest(user);
        }
    }
}