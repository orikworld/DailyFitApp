using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CRSTNative.Client.Infrastructure.Core.Views.Implementations
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BaseMasterDetailPage : MasterDetailPage
    {
        public BaseMasterDetailPage()
        {
            InitializeComponent();
           
        }
    }
}