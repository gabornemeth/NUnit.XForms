//
// TestResult.cs
//
// Author:
//    Gabor Nemeth (gabor.nemeth.dev@gmail.hu)
//
//    Copyright (C) 2015, Gabor Nemeth
//
        
using System;

namespace NUnit.XForms
{
    public enum TestResult
    {
        Success,
        Fail,
        Ignored
    }

    /// <summary>
    /// Result of a test
    /// </summary>
    public class TestResultInfo
    {
        public string Name { get; set; }
        public string Details { get; set; }
        public TestResult Success { get; set; }
    }
}
