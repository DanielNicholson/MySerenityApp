using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using MySerenity.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

[assembly: Xamarin.Forms.Dependency(typeof(MySerenity.Droid.Dependencies.AuthAndroid))]
namespace MySerenity.Droid.Dependencies
{
    class AuthAndroid : IAuth
    {
        public string GetCurrentUserID()
        {
            
            throw new NotImplementedException();
        }

        public bool IsUserAuthenticated()
        {
            throw new NotImplementedException();
        }

        public bool LoginUser(string email, string password)
        {
            throw new NotImplementedException();
        }

        public bool RegisterUser(string email, string password)
        {
            throw new NotImplementedException();
        }
    }
}