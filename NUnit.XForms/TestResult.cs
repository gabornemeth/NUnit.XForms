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
    /// <summary>
    /// Result of a test
    /// </summary>
    public class TestResult
    {
        public string Name { get; set; }
        public string Details { get; set; }
        public bool Success { get; set; }
    }
}
