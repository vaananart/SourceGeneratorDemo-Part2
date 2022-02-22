using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;

using System.Reflection;
using System.Text;

namespace ConstantValue.Generation.SourceGenerator
{
	[Generator]
	public class MathematicalConstantsGenerator : IIncrementalGenerator
	{
		public void Initialize(IncrementalGeneratorInitializationContext context)
		{
			context.RegisterPostInitializationOutput(async ctx => {

				var assembly = Assembly.GetExecutingAssembly();
				string resourcePath = assembly
					.GetManifestResourceNames()
					.Where(x => x.Contains("MathematicalConstantValues.template"))
					.FirstOrDefault();

				var fileContent = string.Empty;
				using (Stream stream = assembly.GetManifestResourceStream(resourcePath))
				using (StreamReader reader = new StreamReader(stream))
				{
					fileContent = await reader.ReadToEndAsync();
				}

				ctx.AddSource("MathematicalConstants.g.cs",
				SourceText.From(fileContent, Encoding.UTF8));

			});
		}
	}
}
