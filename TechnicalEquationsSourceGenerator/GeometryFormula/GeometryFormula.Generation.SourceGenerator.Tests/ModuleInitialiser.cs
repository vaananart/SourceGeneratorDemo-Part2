
using System.Runtime.CompilerServices;

using VerifyTests;

namespace GeometryFormula.Generation.SourceGenerator.Tests
{
	public class ModuleInitialiser
	{
		[ModuleInitializer]
		public static void Init()
		{
			VerifySourceGenerators.Enable();
		}
	}
}
