using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.PlatformConfiguration.AndroidSpecific;
using Xamarin.Forms.Xaml;

namespace MySerenity.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TherapistDashboard
    {
        public TherapistDashboard()
        {
            NavigationPage.SetHasNavigationBar(this, false);
            var colorTypeConverter = new ColorTypeConverter();
            BarBackgroundColor = (Xamarin.Forms.Color)colorTypeConverter.ConvertFromInvariantString("#85aed0");
            On<Android>().SetToolbarPlacement(ToolbarPlacement.Bottom);
            InitializeComponent();
        }
    }
}