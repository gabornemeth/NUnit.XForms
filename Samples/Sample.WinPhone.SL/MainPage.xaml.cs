using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Sample.WinPhone.SL.Resources;

namespace Sample.WinPhone.SL
{
    public partial class MainPage
    {
        // Constructor
        public MainPage()
        {
            InitializeComponent();

            SupportedOrientations = SupportedPageOrientation.Portrait;
            Xamarin.Forms.Forms.Init();
            LoadApplication(new MyTestApp());
        }
    }
}