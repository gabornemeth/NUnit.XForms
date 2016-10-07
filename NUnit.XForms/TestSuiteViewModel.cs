using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace NUnit.XForms
{
    /// <summary>
    /// MVVM based NUnit test runner
    /// Test results can be easily databound in XAML
    /// </summary>
    public class TestSuiteViewModel : TestRunnerViewModel
    {
        public override async Task RunInternalAsync()
        {
            Results.Clear();
            foreach (TestFixtureViewModel fixture in _fixtures)
            {
                await fixture.RunAsync();
                foreach (var result in fixture.Results)
                {
                    Results.Add(result);
                }
            }
        }

        public override IEnumerable<TestRunnerViewModel> Tests
        {
            get { return _fixtures; }
        }

        private readonly List<TestFixtureViewModel> _fixtures;

        public TestSuiteViewModel(TestRunner runner)
        {
            //_model = runner;
            _fixtures = new List<TestFixtureViewModel>();
            foreach (var fixture in runner.Fixtures)
                _fixtures.Add(new TestFixtureViewModel(fixture, this));
            Results = new ObservableCollection<TestResultInfo>();
        }
    }
}
