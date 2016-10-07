using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms.Xaml;

namespace NUnit.XForms
{
    // XamlC seems unsupported on Windows
    //[XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TestResultsPage
    {
        public TestResultsPage()
        {
            InitializeComponent();
        }
    }
}
