//HintName: Parallelogram.g.cs
namespace GeometryFormula.Generation.SourceGenerator.Formulas
{
	public class Parallelogram
	{
		private readonly double _a;
		private readonly double _h;
		
		public Parallelogram(double a, double h)
		{
			_a = a;
			_h = h;
		}

		public double Area => _a * _h;
	}
}