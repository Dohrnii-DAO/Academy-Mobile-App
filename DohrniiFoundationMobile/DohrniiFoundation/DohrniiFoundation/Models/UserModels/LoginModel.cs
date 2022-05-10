using DohrniiFoundation.Helpers;
using DohrniiFoundation.Resources;

namespace DohrniiFoundation.Models.UserModels
{
    /// <summary>
    /// This model class is to bind the values from the login API
    /// </summary>
    public class LoginModel : ObservableObject
    {
        #region Private Properties
        private string rememeberImage = StringConstant.Unselected;
        private string email;
        private string password;
        private string emailAddress = DFResources.EmailAddressText;
        private string loginTitle = DFResources.PleaseLoginText;
        private bool isSignUpClicked = false;
        private bool isLoginVisible = true;
        private bool isLoginBtnVisible = true;
        private bool isSignUpBtnVisible = false;
        private string signText = DFResources.SignUpText;
        private string accountText = DFResources.AlreadyHaveAccountText;
        #endregion

        #region Public Properties
        public bool Isselected = false;
        public string Email
        {
            get
            {
                return email;
            }
            set
            {
                if (email != value)
                {
                    email = value;
                    this.OnPropertyChanged();
                }
            }
        }
        public string Password
        {
            get
            {
                return password;
            }
            set
            {
                if (password != value)
                {
                    password = value;
                    this.OnPropertyChanged();
                }
            }
        }
        public string RememeberImage
        {
            get
            {
                return rememeberImage;
            }
            set
            {
                if (rememeberImage != value)
                {
                    rememeberImage = value;
                    this.OnPropertyChanged();
                }
            }
        }
        public string EmailAddress
        {
            get
            {
                return emailAddress;
            }
            set
            {
                if (emailAddress != value)
                {
                    emailAddress = value;
                    this.OnPropertyChanged();
                }
            }
        }
        public bool IsSignUpClicked
        {
            get
            {
                return isSignUpClicked;
            }
            set
            {
                if (isSignUpClicked != value)
                {
                    isSignUpClicked = value;
                    this.OnPropertyChanged();
                }
            }
        }
        public string LoginTitle
        {
            get
            {
                return loginTitle;
            }
            set
            {
                if (loginTitle != value)
                {
                    loginTitle = value;
                    this.OnPropertyChanged();
                }
            }
        }
        public bool IsLoginVisible
        {
            get
            {
                return isLoginVisible;
            }
            set
            {
                if (isLoginVisible != value)
                {
                    isLoginVisible = value;
                    this.OnPropertyChanged();
                }
            }
        }
        public string SignText
        {
            get
            {
                return signText;
            }
            set
            {
                if (signText != value)
                {
                    signText = value;
                    this.OnPropertyChanged();
                }
            }
        }
        public bool IsLoginBtnVisible
        {
            get
            {
                return isLoginBtnVisible;
            }
            set
            {
                if (isLoginBtnVisible != value)
                {
                    isLoginBtnVisible = value;
                    this.OnPropertyChanged();
                }
            }
        }
        public bool IsSignUpBtnVisible
        {
            get
            {
                return isSignUpBtnVisible;
            }
            set
            {
                if (isSignUpBtnVisible != value)
                {
                    isSignUpBtnVisible = value;
                    this.OnPropertyChanged();
                }
            }
        }
        public string AccountText
        {
            get
            {
                return this.accountText;
            }
            set
            {
                if (this.accountText != value)
                {
                    this.accountText = value;
                    this.OnPropertyChanged();
                }
            }
        }
        #endregion
    }
}
