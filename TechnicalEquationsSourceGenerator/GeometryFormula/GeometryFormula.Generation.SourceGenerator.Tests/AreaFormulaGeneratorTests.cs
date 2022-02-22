using GeometryFormula.Generation.SourceGenerator.Tests.TestSupport;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

using System.Collections.Generic;
using System.Collections.Immutable;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

using VerifyXunit;

using Xunit;

namespace GeometryFormula.Generation.SourceGenerator.Tests;

[UsesVerify]
public class AreaFormulaGeneratorTests
{
	[Fact]
    public async Task<Task> AreaFormulaTest()
    {
		//Arrange
		var assembly = Assembly.GetExecutingAssembly();
		string resourcePath = assembly
			.GetManifestResourceNames()
		.Where(x => x.Contains(".json"))
		.FirstOrDefault()!;
		
		var fileContent = string.Empty;
		using (Stream stream = assembly.GetManifestResourceStream(resourcePath)!)
		using (StreamReader reader = new StreamReader(stream))
		{
			fileContent = await reader.ReadToEndAsync();
		}
		var directoryString = Path.GetDirectoryName(assembly.Location);
		var additionalText = new MockAdditionalText(directoryString + "\\areaformula.json", fileContent);
		CSharpCompilation compilation = CSharpCompilation.Create(
		assemblyName: "AreaFormulaGeneratorTests"
		);
		var generator = new AreaFormulaGenerator();
		GeneratorDriver driver = CSharpGeneratorDriver.Create(generator)
		.AddAdditionalTexts( ImmutableArray.CreateRange(new List<AdditionalText> {additionalText }));

		//Action
		driver = driver.RunGenerators(compilation);

		//Assert
		return Verifier.Verify(driver);
	}
}