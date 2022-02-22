using ConstantValue.Generation.SourceGenerator.ConstantValues;
namespace ConstantValue.Generation;
public class Circle
{
	private double _radius;

	public Circle(double radius)
	{
		_radius = radius;
	}

	public double GetArea() =>  MathematicalConstantValues.PI * (Math.Pow(_radius, 2));
	public double GetPerimeter() => 2 * _radius * MathematicalConstantValues.PI;
}
