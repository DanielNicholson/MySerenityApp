using Foundation;
using MySerenity.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using UIKit;
using Xamarin.Forms;

[assembly: Xamarin.Forms.Dependency(typeof(MySerenity.iOS.Dependencies.AuthIOS))]
namespace MySerenity.iOS.Dependencies
{
    public class AuthIOS : IAuth
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
            return true;
        }
    }
}