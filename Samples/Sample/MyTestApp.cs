using System;
using NUnit.XForms;
using Xamarin.Forms;

namespace Sample
{
    class MyTestApp : Xamarin.Forms.Application
    {
        public MyTestApp()
        {
            var runner = new TestRunner();
            runner.Add(typeof(MyTest));
            var page = new TestRunnerPage(runner);
            page.Title = "XForms test runner";
            MainPage = new NavigationPage(page);
        }
    }
}
