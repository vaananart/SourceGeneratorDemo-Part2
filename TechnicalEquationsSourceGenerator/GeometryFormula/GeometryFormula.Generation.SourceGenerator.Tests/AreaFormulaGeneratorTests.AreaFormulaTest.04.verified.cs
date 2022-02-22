//HintName: Triangle.g.cs
namespace GeometryFormula.Generation.SourceGenerator.Formulas
{
	public class Triangle
	{
		private readonly double _b;
		private readonly double _h;
		
		public Triangle(double b, double h)
		{
			_b = b;
			_h = h;
		}

		public double Area => (_b * _h) / 2.0;
	}
}