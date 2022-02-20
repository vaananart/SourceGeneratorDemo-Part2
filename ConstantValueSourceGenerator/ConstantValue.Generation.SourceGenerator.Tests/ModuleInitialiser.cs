using System.Runtime.CompilerServices;

using VerifyTests;

namespace ConstantValue.Generation.SourceGenerator.Tests
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
