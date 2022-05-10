using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DohrniiFoundation.Views.Lessons
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class QuizCompletePage : ContentPage
    {
        public QuizCompletePage()
        {
            InitializeComponent();
        }
        protected override bool OnBackButtonPressed()
        {
            return true;
        }
    }
}