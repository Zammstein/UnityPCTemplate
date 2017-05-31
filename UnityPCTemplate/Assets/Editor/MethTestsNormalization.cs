using NUnit.Framework;
using SMP.Utility;

/// <summary>
/// MethMethTestsNormalization
/// <summary>
/// Author: Sam Meyer
/// <summary>
/// Automatic testing class used to verify the NormalizeToScale 
/// utility method with boundary analysis
/// </summary>
public class MethTestsNormalization {

    /// <summary>
    /// Expected value used in each use case to check against the actual output.
    /// </summary>
    private float expectedOutput;

    /// <summary>
    /// Boundary test case where the input value is within the bounds (0 - 1)
    /// </summary>
	[Test]
	public void VerifyNormalizeToScaleInBounds() {
        // setup
        expectedOutput = 15f;

        // action
        float output = Meth.NormalizeToScale(0.5f, 20f, 10f);

        // verification
        Assert.AreEqual(expectedOutput, output);
    }

    /// <summary>
    /// Boundary test case where the input value is on the bounds (0 - 1)
    /// </summary>
    [Test]
    public void VerifyNormalizeToScaleOnBounds() {
        // setup
        expectedOutput = 20f;

        // action
        float output = Meth.NormalizeToScale(1f, 20f, 10f);

        // verification
        Assert.AreEqual(expectedOutput, output);
    }

    /// <summary>
    /// Boundary test case where the input value is out of bounds (0 - 1)
    /// </summary>
    [Test]
    public void VerifyNormalizeToScaleOutBounds() {
        // setup
        expectedOutput = 20f;

        // action
        float output = Meth.NormalizeToScale(2f, 20f, 10f);

        // verification
        Assert.AreEqual(expectedOutput, output);
    }
}
