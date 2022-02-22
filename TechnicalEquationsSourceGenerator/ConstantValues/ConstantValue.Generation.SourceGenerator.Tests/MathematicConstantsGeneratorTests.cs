using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

using System.Threading.Tasks;

using VerifyXunit;

using Xunit;
namespace ConstantValue.Generation.SourceGenerator.Tests;

[UsesVerify]
public class MathematicConstantsGeneratorTests
{
    [Fact]
    public Task GeneratedMathematicalConstantValuesVerificationTest()
    {
		//Arrange
		CSharpCompilation compilation = CSharpCompilation.Create(
		assemblyName: "MathematicalConstantGeneratorTests");
		var generator = new MathematicalConstantsGenerator();
		GeneratorDriver driver = CSharpGeneratorDriver.Create(generator);

		//Acion
		driver = driver.RunGenerators(compilation);

		//Assert
		return Verifier.Verify(driver);
	}
}