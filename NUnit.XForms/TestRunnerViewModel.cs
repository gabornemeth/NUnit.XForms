//
// TestRunnerViewModel.cs
//
// Author:
//    Gabor Nemeth (gabor.nemeth.dev@gmail.hu)
//
//    Copyright (C) 2015, Gabor Nemeth
//

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace NUnit.XForms
{
    /// <summary>
    /// Viewmodel for test runner
    /// </summary>
    public abstract class TestRunnerViewModel : BindableObject
    {
        /// <summary>
        /// Tesztfuttatás eredményei
        /// </summary>
        public ObservableCollection<TestResultInfo> Results { get; protected set; }

        protected TestRunnerViewModel Parent { get; set; }

        public abstract IEnumerable<TestRunnerViewModel> Tests
        {
            get;
        }


        private bool _isRunning;
        /// <summary>
        /// If the tests are already running
        /// </summary>
        public bool IsRunning
        {
            get
            {
                return _isRunning;
            }
            set
            {
                if (_isRunning != value)
                {
                    _isRunning = value;
                    OnPropertyChanged();
                }
            }
        }

        private bool _isSelected;
        /// <summary>
        /// If this instance is selected to be run
        /// </summary>
        public bool IsSelected
        {
            get
            {
                return _isSelected;
            }
            set
            {
                if (_isSelected != value)
                {
                    _isSelected = value;
                    OnPropertyChanged();

                    if (_isSelected)
                    {
                        // if it has been selected, select the parent also
                        if (Parent != null)
                            Parent.IsSelected = true;
                        if (Tests != null)
                        {
                            if (Tests.All(test => test.IsSelected != _isSelected))
                            {
                                foreach (var test in Tests)
                                    test.IsSelected = _isSelected;
                            }
                        }
                    }
                    else
                    {
                        if (Tests != null)
                        {
                            // has been deselected, let's deselect the children
                            foreach (var test in Tests)
                                test.IsSelected = false;
                        }

                    }
                }
            }
        }

        /// <summary>
        /// Run the tests
        /// </summary>
        public abstract Task RunInternalAsync();

        public async Task RunAsync()
        {
            if (!IsSelected)
                return;

            await RunInternalAsync();
        }

        public virtual string Name
        {
            get { return ""; }
        }

        private Command _runCommand;
        public Command RunCommand
        {
            get
            {
                return _runCommand ?? (_runCommand = new Command(async () =>
                {
                    if (IsSelected == false)
                        return;

                    IsRunning = true;
                    await RunAsync();
                    IsRunning = false;
                    // Eredemények megtekintése
                    await Application.Current.MainPage.Navigation.PushAsync(new TestResultsPage { BindingContext = Results });
                }));
            }
        }

        private Command _selectAllCommand;
        public Command SelectAllCommand
        {
            get
            {
                return _selectAllCommand ?? (_selectAllCommand = new Command(() =>
                {
                    if (Tests == null)
                        return;

                    foreach (var test in Tests)
                    {
                        test.IsSelected = true;
                    }
                }));
            }
        }

        private Command _deselectAllCommand;
        public Command DeselectAllCommand
        {
            get
            {
                return _deselectAllCommand ?? (_deselectAllCommand = new Command(() =>
                {
                    if (Tests == null)
                        return;

                    foreach (var test in Tests)
                    {
                        test.IsSelected = false;
                    }
                }));
            }
        }

        public TestRunnerViewModel()
        {
            Results = new ObservableCollection<TestResultInfo>();
            _isSelected = true;
        }
    }
}
