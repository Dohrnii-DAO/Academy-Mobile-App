using DohrniiFoundation.Models.More;
using Microsoft.AppCenter.Crashes;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DohrniiFoundation.Views.More
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProfileMorePage : ContentPage
    {
        public ProfileMorePage()
        {
            InitializeComponent();
        }
        protected override bool OnBackButtonPressed()
        {
            return true;
        }
        private void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            try
            {
                ProfileMoreModel selectedItem = e.SelectedItem as ProfileMoreModel;
                ((ListView)sender).SelectedItem = null;
                VM.GetSelectedItem(selectedItem);
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
            }
        }
    }
}