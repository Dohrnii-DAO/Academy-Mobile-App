using Rg.Plugins.Popup.Pages;
using Xamarin.Forms.Xaml;

namespace DohrniiFoundation.Views.Lessons
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LessonOnboardingPopUpPage : PopupPage
    {
        public LessonOnboardingPopUpPage()
        {
            InitializeComponent();
        }
        protected override bool OnBackButtonPressed()
        {
            return true;
        }
    }
}