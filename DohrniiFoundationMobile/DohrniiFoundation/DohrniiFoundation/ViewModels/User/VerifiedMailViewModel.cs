using DohrniiFoundation.Helpers;
using Rg.Plugins.Popup.Extensions;
using Xamarin.Forms;

namespace DohrniiFoundation.ViewModels
{
    /// <summary>
    /// View model to binding and handle functionality of the verified email
    /// </summary> 
    public class VerifiedMailViewModel : ObservableObject
    {
        #region Properties
        public string Verified { get; set; } = StringConstant.Verified;
        #endregion

        #region Constructor
        public VerifiedMailViewModel()
        {
        }
        #endregion

        #region Commands
        public Command OKButtonCommand
        {
            get
            {
                return new Command(async () =>
                {
                    await Application.Current.MainPage.Navigation.PopPopupAsync();
                });
            }
        }
        #endregion
    }
}
