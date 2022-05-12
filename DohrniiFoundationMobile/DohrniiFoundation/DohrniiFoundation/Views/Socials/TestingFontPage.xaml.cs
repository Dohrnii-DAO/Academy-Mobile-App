using DohrniiFoundation.Helpers;
using DohrniiFoundation.IServices;
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
    public partial class TestingFontPage : ContentPage
    {
        public TestingFontPage()
        {
            InitializeComponent();
            //HtmlWebViewSource html = new HtmlWebViewSource();
            //html.BaseUrl = DependencyService.Get<ILocalBaseUrl>().Get(); //"file:///android_asset/";
            //html.Html = StringConstant.HtmlContentWithFont;
            //webview.Source = html; //StringConstant.HtmlContentWithFont;
        }
    }
}