//
// TestViewModel.cs
//
// Author:
//    Gabor Nemeth (gabor.nemeth.dev@gmail.hu)
//
//    Copyright (C) 2015, Gabor Nemeth
//

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NUnit.XForms
{
    /// <summary>
    /// ViewModel for test
    /// </summary>
    public class TestViewModel : TestRunnerViewModel
    {
        private readonly Test _test;

        internal TestViewModel(Test test, TestFixtureViewModel parent)
        {
            _test = test;
            Parent = parent;
        }

        public override string Name
        {
            get { return _test.Method.Name; }
        }

        public override async Task RunInternalAsync()
        {
            Results.Clear();
            await Task.Run(() => { _test.Run(); });
            Results.Add(_test.Result);
        }

        public override System.Collections.Generic.IEnumerable<TestRunnerViewModel> Tests
        {
            get { return new List<TestRunnerViewModel> { this }; }
        }
    }
}
