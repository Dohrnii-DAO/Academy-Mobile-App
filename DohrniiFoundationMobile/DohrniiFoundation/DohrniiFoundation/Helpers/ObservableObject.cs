using Microsoft.AppCenter.Crashes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace DohrniiFoundation.Helpers
{
    /// <summary>
    /// Class to change the value of property at runtime and reflect changes on UI
    /// </summary>
    public class ObservableObject : INotifyPropertyChanged
    {
        #region Private Properties
        private bool isLoading = false;
        private bool isInternet;
        private bool responseError;
        private string pageTitle;
        #endregion

        #region Public Properties
        /// <summary>
        /// get and set loading activity.
        /// </summary>
        public bool IsLoading
        {
            get { return this.isLoading; }
            set
            {
                this.isLoading = value;
                this.OnPropertyChanged(nameof(this.IsLoading));
            }
        }
        /// <summary>
        /// get and set Internet connection.
        /// </summary>
        public bool IsInternet
        {
            get { return this.isInternet; }
            set
            {
                this.isInternet = value;
                this.OnPropertyChanged(nameof(this.IsInternet));
            }
        }
        /// <summary>
        /// get and set Response Error.
        /// </summary>
        public bool ResponseError
        {
            get { return this.responseError; }
            set
            {
                this.responseError = value;
                this.OnPropertyChanged(nameof(this.ResponseError));
            }
        }
        /// <summary>
        /// get and set Page Title
        /// </summary>
        public string PageTitle
        {
            get { return this.pageTitle; }
            set
            {
                this.pageTitle = value;
                this.OnPropertyChanged(nameof(this.PageTitle));
            }
        }
        /// <summary>
        /// Occurs when property changed.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion

        #region Constructor
        /// <summary>
        /// constructor
        /// </summary>
        public ObservableObject()
        {
            Connectivity.ConnectivityChanged += Connectivity_ConnectivityChanged;
            this.IsInternet = Connectivity.NetworkAccess != NetworkAccess.Internet;
        }
        #endregion

        #region Methods
        /// <summary>
        /// When the internet connection changes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void Connectivity_ConnectivityChanged(object sender, ConnectivityChangedEventArgs e)
        {
            this.IsInternet = e.NetworkAccess != NetworkAccess.Internet;
        }
        /// <summary>
        /// Sets the property.
        /// </summary>
        /// <returns><c>true</c>, if property was set, <c>false</c> otherwise.</returns>
        /// <param name="backingStore">Backing store.</param>
        /// <param name="value">Value.</param>
        /// <param name="propertyName">Property name.</param>
        /// <param name="onChanged">On changed.</param>
        /// <typeparam name="T">The 1st type parameter.</typeparam>
        protected bool SetProperty<T>(ref T backingStore, T value, [CallerMemberName] string propertyName = "", Action onChanged = null)
        {
            if (EqualityComparer<T>.Default.Equals(backingStore, value))
                return false;
            backingStore = value;
            onChanged?.Invoke();
            this.OnPropertyChanged(propertyName);
            return true;
        }
        /// <summary>
        /// Raises the property changed event.
        /// </summary>
        /// <param name="propertyName">Property name.</param>
        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            try
            {
                var changed = this.PropertyChanged;
                if (changed == null)
                    return;

                changed.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
            }
        }
        #endregion

        #region Commands
        /// <summary>
        /// Gets command to Property bind to navigate to back screen
        /// </summary>
        public Command BackCommand
        {
            get
            {
                return new Command(() =>
                {
                    try
                    {
                        Application.Current.MainPage.Navigation.PopModalAsync();
                    }
                    catch (Exception ex)
                    {
                        Crashes.TrackError(ex);
                    }
                });
            }
        }
        #endregion
    }
}
