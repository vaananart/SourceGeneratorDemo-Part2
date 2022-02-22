using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

using System.Reflection;
using System.Text;

namespace GeometryFormula.Generation.SourceGenerator;

[Generator]
public class AreaFormulaGenerator : IIncrementalGenerator
{
	public void Initialize(IncrementalGeneratorInitializationContext context)
	{
		//NOTE: This gave the idea.
		//https://stackoverflow.com/questions/67071355/source-generators-dependencies-not-loaded-in-visual-studio
		AppDomain.CurrentDomain.AssemblyResolve += (_, args) =>
		{
			AssemblyName name = new(args.Name);
			var executingAssembly = Assembly.GetExecutingAssembly();
			var resourceName = executingAssembly.GetManifestResourceNames().Where(x => x.Contains(name.Name)).FirstOrDefault();
			using Stream resourceStream = Assembly.GetExecutingAssembly().GetManifestResourceStream(resourceName);
			using MemoryStream memoryStream = new MemoryStream();
			resourceStream.CopyTo(memoryStream);
			return Assembly.Load(memoryStream.ToArray());
		};

		var additionaltexts = context.AdditionalTextsProvider.Collect();
		var compilationAndAdditionaltexts = context.CompilationProvider.Combine(additionaltexts);
		context.RegisterImplementationSourceOutput(additionaltexts, async (spc, source) =>
		{
			var assembly = Assembly.GetExecutingAssembly();
			string resourcePath = assembly
				.GetManifestResourceNames()
				.Where(x => x.Contains("AreaFormula.template"))
				.FirstOrDefault();

			var count = source.Count();
			var fileContent = string.Empty;
			using (Stream stream = assembly.GetManifestResourceStream(resourcePath))
			using (StreamReader reader = new StreamReader(stream))
			{
				fileContent = await reader.ReadToEndAsync();
			}

			var template = fileContent;
			var jsonObj = JsonConvert.DeserializeObject(value: source[0].GetText()!.ToString()) as JObject;

			foreach (JProperty property in jsonObj!.Properties())
			{ 
				var name = property.Name;
				var value = property.Value;
				var inputs = value["inputs"] as JArray;

				fileContent = fileContent.Replace("##SHAPEAREA##", name);

				string constructorParamString = string.Empty;
				string privateVariablesString = string.Empty;
				string constructorParamAssignmentString = string.Empty;


				var formula = value["formula"]!.ToString();
				string processingFormulaString = formula;
				string constructorContentString = string.Empty ;
				foreach (var inputName in from JValue inputElement in inputs
										  let inputName = inputElement.ToString()
										  select inputName)
				{
					constructorParamString += $"double {inputName},";
					processingFormulaString = processingFormulaString.Replace(inputName, $"_{inputName}");
					privateVariablesString += $"private readonly double _{inputName};\n\t\t";
					constructorContentString += $"_{inputName} = {inputName};\n\t\t\t";
				}

				constructorParamString = constructorParamString.TrimEnd(',');
				constructorParamString = constructorParamString.Replace(",", ", ");
				constructorContentString = constructorContentString.TrimEnd('\n','\t','\t');

				fileContent = fileContent.Replace("##CONSTRUCTOR##", $"{name}({constructorParamString})\n\t\t{{\n\t\t\t{constructorContentString}\n\t\t}}");
				fileContent = fileContent.Replace("##AREAFORMULA##", $"{processingFormulaString};");
				fileContent = fileContent.Replace("##PRIVATEVARAIBLES##", $"{privateVariablesString}");

				spc.AddSource($"{name}.g.cs", SourceText.From(fileContent, Encoding.UTF8));
				fileContent = template;
			}
		});
	}
}
