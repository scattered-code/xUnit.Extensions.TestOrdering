namespace XUnit.Extensions.TestOrdering {
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Xunit.Abstractions;
    using Xunit.Sdk;

    public sealed class DependencyTestCaseOrderer : ITestCaseOrderer {
        public const string Name = "XUnit.Extensions.TestOrdering." + nameof(DependencyTestCaseOrderer);
        public const string Assembly = "XUnit.Extensions.TestOrdering";
        private readonly IMessageSink diagnosticMessageSink;

        public DependencyTestCaseOrderer(IMessageSink diagnosticMessageSink)
        {
            this.diagnosticMessageSink = diagnosticMessageSink;
        }

        public IEnumerable<TTestCase> OrderTestCases<TTestCase>(IEnumerable<TTestCase> testCases) where TTestCase : ITestCase {


            //var result = testCases.ToList();  // Run them in discovery order
            //var message = new DiagnosticMessage("Ordered {0} test cases", result.Count);
            //diagnosticMessageSink.OnMessage(message);
            //return result;
            return DependencySorter.Sort(testCases.Select(x => new TestCaseWrapper(x))).Select(x => x.TestCase).Cast<TTestCase>();
        }

        private sealed class TestCaseWrapper : IDependencyIndicator<TestCaseWrapper> {
            public TestCaseWrapper(ITestCase testCase) {
                this.TestCase = testCase;

                var attributeInfo =
                    testCase.TestMethod.Method.GetCustomAttributes(typeof(TestDependencyAttribute))
                        .OfType<ReflectionAttributeInfo>()
                        .SingleOrDefault();

                if (attributeInfo != null) {
                    var attribute = (TestDependencyAttribute) attributeInfo.Attribute;

                    this.TestMethodDependency = attribute.MethodDependency;
                }
            }

            public string TestMethod => this.TestCase.TestMethod.Method.Name;
            public string TestMethodDependency { get; }

            public ITestCase TestCase { get; }

            public bool IsDependencyOf(TestCaseWrapper other) {
                if (other.TestMethodDependency == null) {
                    return false;
                }

                return string.Equals(this.TestMethod, other.TestMethodDependency, StringComparison.Ordinal);
            }

            public bool HasDependencies => !String.IsNullOrEmpty(this.TestMethodDependency);

            public bool Equals(TestCaseWrapper other) {
                if (ReferenceEquals(null, other)) return false;
                if (ReferenceEquals(this, other)) return true;
                return Equals(this.TestCase, other.TestCase);
            }

            public override bool Equals(object obj) {
                if (ReferenceEquals(null, obj)) return false;
                if (ReferenceEquals(this, obj)) return true;
                return obj is TestCaseWrapper && this.Equals((TestCaseWrapper) obj);
            }

            public override int GetHashCode() {
                return this.TestCase?.GetHashCode() ?? 0;
            }

            public override string ToString() => this.TestCase.DisplayName;
        }
    }
}