# Getting Started with Extensions.TestOrdering

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
