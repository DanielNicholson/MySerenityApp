using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace MySerenity.Helpers
{

    public interface IAuth
    {
        bool RegisterUser(string email, string password);
        bool LoginUser(string email, string password);
        bool IsUserAuthenticated();
        string GetCurrentUserID();
    }


    public class Auth
    {
        public static IAuth auth = DependencyService.Get<IAuth>();

        public static bool RegisterUser(string email, string password)
        {

            return true;
        }

        public static bool LoginUser(string email, string password)
        {

            return true;
        }

        public static bool IsUserAuthenticated()
        {

            return true;
        }

        public static string GetCurrentUserID()
        {

            return "";
        }
    }
}
