//
// TestFixtureViewModel.cs
//
// Author:
//    Gabor Nemeth (gabor.nemeth.dev@gmail.hu)
//
//    Copyright (C) 2015, Gabor Nemeth
//

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace NUnit.XForms
{
    /// <summary>
    /// Viewmodel teszt osztályhoz
    /// </summary>
    public class TestFixtureViewModel : TestRunnerViewModel
    {
        private readonly TestFixture _fixture;
        private string _name;

        public override string Name
        {
            get
            {
                return _name;
            }
        }

        private readonly List<TestViewModel> _tests;

        public override IEnumerable<TestRunnerViewModel> Tests
        {
            get
            {
                return _tests;
            }
        }

        internal TestFixtureViewModel(TestFixture fixture, TestSuiteViewModel parent)
        {
            _fixture = fixture;
            Parent = parent;
            _tests = new List<TestViewModel>();
            foreach (var test in _fixture.Tests)
            {
                _tests.Add(new TestViewModel(test, this));
            }
            DetermineName();
        }

        private void DetermineName()
        {
            var sb = new StringBuilder();
            sb.Append(_fixture.Type.Name);
            // konstruktor paramétereit is felsoroljuk
            var constructorArguments = _fixture.TestFixtureAttribute.ConstructorArguments;
            if (constructorArguments != null)
            {
                for (var i = 0; i < constructorArguments.Count; i++)
                {
                    var values =
                        (constructorArguments[i].Value as IEnumerable<CustomAttributeTypedArgument>).ToArray();
                    sb.Append(i == 0 ? " (" : ", ");
                    sb.Append(values[0].Value);
                    if (i == constructorArguments.Count - 1)
                        sb.Append(")");
                }
            }

            _name = sb.ToString();
        }

        public override async Task RunInternalAsync()
        {
            Results.Clear();
            try
            {
                foreach (var test in _tests)
                {
                    await test.RunAsync();
                    foreach (var result in test.Results)
                    {
                        Results.Add(result);
                    }
                }
            }
            catch (Exception ex)
            {
                Results.Add(new TestResult { Details = TestHelper.GetExceptionDetails(ex), Name = this.Name, Success = false });
            }
        }

        private Command _showCommand;
        /// <summary>
        /// Részletek megjelenítése
        /// </summary>
        public Command ShowCommand
        {
            get
            {
                return _showCommand ?? (_showCommand = new Command(() =>
                {
                    Application.Current.MainPage.Navigation.PushAsync(new TestRunnerPage(this));
                }));
            }
        }
    }
}
