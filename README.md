# Getting Started with Extensions.TestOrdering

As a rule, xUnit enforces that tests run in random order, and we want to keep the benefits of that, but at the same time handle the special cases when run order is needed. 

Edit the AssemblyInfo.cs file in your xUnit test project, and add the following lines<br><br>


    using System.Reflection;<br>
    using System.Runtime.InteropServices;<br>
    <b>using Xunit;</b><br>
    <b>using XUnit.Extensions.TestOrdering;</b><br><br>
    
    [assembly: AssemblyConfiguration("")]<br>
    [assembly: AssemblyCompany("")]<br>
    [assembly: AssemblyProduct("MyApp.Tests")]<br>
    [assembly: AssemblyTrademark("")]<br><br>
    
    <b>
    [assembly: TestCaseOrderer(DependencyTestCaseOrderer.Name, DependencyTestCaseOrderer.Assembly)]<br>
    [assembly: CollectionBehavior(CollectionBehavior.CollectionPerAssembly, DisableTestParallelization = true)]<br>
    [assembly: TestCollectionOrderer(DependencyTestCollectionOrderer.Name, DependencyTestCollectionOrderer.Assembly)]<br></b>



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
