using DohrniiFoundation.iOS.Services;
using DohrniiFoundation.IServices;
using Foundation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UIKit;
using Xamarin.Forms;

[assembly: Dependency(typeof(LocalBaseUrl))]
namespace DohrniiFoundation.iOS.Services
{
    public class LocalBaseUrl : ILocalBaseUrl
    {
        public string Get()
        {
            return NSBundle.MainBundle.BundlePath;
        }
    }
}