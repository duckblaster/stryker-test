using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BTW.Shared.Aspire.Testing;

/// <summary>Provides an opt-in gate for Aspire integration tests.</summary>
public static class AspireIntegrationTestGate
{
    private const string EnvironmentVariable = "BTW_RUN_ASPIRE_INTEGRATION_TESTS";

    /// <summary>Marks the current test inconclusive unless Aspire integration tests are enabled.</summary>
    /// <remarks>Set <c>BTW_RUN_ASPIRE_INTEGRATION_TESTS=1</c> to enable the tests.</remarks>
    public static void AssertEnabled()
    {
        if (string.Equals(Environment.GetEnvironmentVariable(EnvironmentVariable), "1", StringComparison.Ordinal))
        {
            return;
        }

        Assert.Inconclusive($"Set {EnvironmentVariable}=1 to run Aspire integration tests.");
    }
}
