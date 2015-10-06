using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace NUnit.XForms
{
    partial class TestRunnerPage
    {
        public TestRunnerPage(TestRunner runner)
        {
            InitializeComponent();
            BindingContext = new TestSuiteViewModel(runner);
        }

        internal TestRunnerPage(TestRunnerViewModel runner)
        {
            InitializeComponent();
            BindingContext = runner;
        }
    }
}

