//HintName: Square.g.cs
namespace GeometryFormula.Generation.SourceGenerator.Formulas
{
	public class Square
	{
		private readonly double _a;
		
		public Square(double a)
		{
			_a = a;
		}

		public double Area => _a * _a;
	}
}