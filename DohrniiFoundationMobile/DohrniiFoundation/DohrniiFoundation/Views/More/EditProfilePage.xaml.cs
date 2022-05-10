using DohrniiFoundation.Helpers;
using System.Text.RegularExpressions;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DohrniiFoundation.Views.More
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EditProfilePage : ContentPage
    {
        #region Constructor
        public EditProfilePage()
        {
            InitializeComponent();
        }
        #endregion
        #region Methods
        protected override bool OnBackButtonPressed()
        {
            return true;
        }
        #endregion
    }
}