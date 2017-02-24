# NUnit.XForms

[![Build Status](https://www.bitrise.io/app/92e6b2fbb10d4810.svg?token=8e2QCm9jI5DkTtrvDSWveQ)](https://www.bitrise.io/app/92e6b2fbb10d4810)

[![Build status](https://ci.appveyor.com/api/projects/status/u2cfbc7cnlahj8b8?svg=true)](https://ci.appveyor.com/project/gabornemeth/nunit-xforms)

NUnit.XForms is a test runner for Xamarin Forms applications. It gives you much more flexibility for running tests, than the built-in Xamarin NUnitLite test project. It works with both NUnit and NUnitLite.

To use NUnit.XForms, you should be familiar with Xamarin Forms: http://developer.xamarin.com/guides/cross-platform/xamarin-forms/

For the simplest use-case, see the example below.

```csharp
using NUnit.XForms;
...

public class TestApp : Xamarin.Forms.Application
{
	public TestApp()
	{
		var runner = new TestRunner();
		// Add your test types/assemblies
		runner.Add(typeof(YourTestType));
		var page = new TestRunnerPage(runner);
		MainPage = new NavigationPage(page);
	}
}
```
# Getting Started with NUnit.XForms

First, you have to setup Xamarin Forms. See the tutorials below to learn how to achieve this on different platforms.

Android, iOS, Windows Phone 8.1 Silverlight guide [here](http://developer.xamarin.com/guides/cross-platform/xamarin-forms/introduction-to-xamarin-forms/)

Windows Store (beta) guide [here](http://developer.xamarin.com/guides/cross-platform/xamarin-forms/windows/)

After that your only job is to setup a ```TestRunnerPage``` and navigate to it.

```csharp
using NUnit.XForms;

...

var runner = new TestRunner();
// Add your test types/assemblies
runner.Add(typeof(MyTestType));
runner.Add(typeof(MyTestType2).Assembly);
var page = new TestRunnerPage(runner);
// now you can display 'page'
```

See the samples for further code. All samples have been built using Visual Studio 2013.

Being a PCL (Portable Class Library), NUnit.XForms should work on iOS also, but has not been tested. 

### Nuget

Can also be grabbed from [Nuget] (https://www.nuget.org/packages/NUnit.XForms/).

### Known issues
* SetupFixture, TearDownFixture not supported yet

### Support

Feature requests are welcome. Please post as new issue.

If found this project useful, please consider to support further development.

[![Donate](https://img.shields.io/badge/Donate-PayPal-green.svg)](https://www.paypal.com/cgi-bin/webscr?cmd=_s-xclick&hosted_button_id=NEPJXX7AZ9YBQ)
