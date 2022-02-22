//HintName: Reactangle.g.cs
namespace GeometryFormula.Generation.SourceGenerator.Formulas
{
	public class Reactangle
	{
		private readonly double _a;
		private readonly double _b;
		
		public Reactangle(double a, double b)
		{
			_a = a;
			_b = b;
		}

		public double Area => _a * _b;
	}
}