using BTW.Shared.Aspire.Testing;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BTW.Shared.Aspire.Testing.Tests;

[TestClass]
[DoNotParallelize]
public sealed class AspireIntegrationTestGateTests
{
    private const string EnvironmentVariable = "BTW_RUN_ASPIRE_INTEGRATION_TESTS";

    [TestMethod]
    public void AssertEnabled_WhenEnvironmentVariableIsOne_ReturnsNormally()
    {
        WithEnvironmentVariable("1", AspireIntegrationTestGate.AssertEnabled);
    }

    [TestMethod]
    public void AssertEnabled_WhenEnvironmentVariableIsMissing_MarksTestInconclusive()
    {
        AssertInconclusiveWhenEnvironmentVariableIs(null);
    }

    [TestMethod]
    public void AssertEnabled_WhenEnvironmentVariableHasAnotherValue_MarksTestInconclusive()
    {
        AssertInconclusiveWhenEnvironmentVariableIs("0");
    }

    private static void AssertInconclusiveWhenEnvironmentVariableIs(string? value)
    {
        WithEnvironmentVariable(value, () =>
        {
            var exception = Assert.ThrowsExactly<AssertInconclusiveException>(AspireIntegrationTestGate.AssertEnabled);

            Assert.AreEqual(
                "Assert.Inconclusive. Set BTW_RUN_ASPIRE_INTEGRATION_TESTS=1 to run Aspire integration tests.",
                exception.Message);
        });
    }

    private static void WithEnvironmentVariable(string? value, Action action)
    {
        var originalValue = Environment.GetEnvironmentVariable(EnvironmentVariable);
        try
        {
            Environment.SetEnvironmentVariable(EnvironmentVariable, value);
            action();
        }
        finally
        {
            Environment.SetEnvironmentVariable(EnvironmentVariable, originalValue);
        }
    }
}
