using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using DohrniiFoundation.Droid.Services;
using DohrniiFoundation.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;

[assembly: Dependency(typeof(LocalBaseUrl))]
namespace DohrniiFoundation.Droid.Services
{
    public class LocalBaseUrl : ILocalBaseUrl
    {
        public string Get()
        {
            return "file:///android_asset/";
        }
    }
}