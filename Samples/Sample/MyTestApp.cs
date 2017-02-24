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
            AddTests(runner);
            var page = new TestRunnerPage(runner);
            page.Title = "XForms test runner";
            MainPage = new NavigationPage(page);
        }

        protected virtual void AddTests(TestRunner runner)
        {
            runner.Add(typeof(MyTest));
        }
    }
}
