using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.AndroidSpecific;
using Xamarin.Forms.Xaml;

namespace MySerenity.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class VideoCallingPage : ContentPage
    {
        public VideoCallingPage()
        {
            InitializeComponent();
            MyWebView.Source = "https://myserenityvideocalling.z33.web.core.windows.net/#56d97";
        }
    }
}