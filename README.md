# Getting Started with Extensions.TestOrdering

As a rule, xUnit enforces that tests run in random order, and we want to keep the benefits of that, but at the same time handle the special cases when run order is needed. 

Edit the **AssemblyInfo.cs** file in your xUnit test project, and add the following lines<br><br>

```
using System.Reflection;
using System.Runtime.InteropServices;
using Xunit;
using XUnit.Extensions.TestOrdering;

[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("")]
[assembly: AssemblyProduct("MyApp.Tests")]
[assembly: AssemblyTrademark("")]

[assembly: TestCaseOrderer(DependencyTestCaseOrderer.Name, DependencyTestCaseOrderer.Assembly)]
[assembly: CollectionBehavior(CollectionBehavior.CollectionPerAssembly, DisableTestParallelization = true)]
[assembly: TestCollectionOrderer(DependencyTestCollectionOrderer.Name, DependencyTestCollectionOrderer.Assembly)]
```


# Setting up test dependencies

In the example below, we want to make sure that MyFirstTest runs before MySecondTest because MySecondTest depends on MyFirstTest already having ran.

    [Fact]
    public async Task MyFirstTest()
    {
        
    }
  
    [Fact]
    [TestDependency(nameof(MyFirstTest))]
    public async Task MySecondTest()
    {
        
    }
