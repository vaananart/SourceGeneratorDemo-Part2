//HintName: Trapezoid.g.cs
namespace GeometryFormula.Generation.SourceGenerator.Formulas
{
	public class Trapezoid
	{
		private readonly double _a;
		private readonly double _b;
		private readonly double _h;
		
		public Trapezoid(double a, double b, double h)
		{
			_a = a;
			_b = b;
			_h = h;
		}

		public double Area => ((_a + _b) / 2.0) * _h;
	}
}