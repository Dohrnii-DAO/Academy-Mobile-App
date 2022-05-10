using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DohrniiFoundation.Views.Lessons
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ClassesQuestionPage : ContentPage
    {
        public ClassesQuestionPage()
        {
            InitializeComponent();
        }
        protected override bool OnBackButtonPressed()
        {
            return true;
        }
    }
}