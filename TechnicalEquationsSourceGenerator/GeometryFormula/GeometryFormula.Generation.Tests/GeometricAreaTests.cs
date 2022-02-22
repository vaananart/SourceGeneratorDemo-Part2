using NUnit.Framework;
namespace GeometryFormula.Generation.Tests;

public class GeometricAreaTests
{
	[Test]
	public void SimpleAreaCalulationTest()
	{
		//Arrange
		var area = new AreaCalculatorFacade();

		//Action
		var parallelogram = area.ForParallelogram(2, 4);
		var rectanglArea = area.ForReactangle(4, 2);
		var squareArea = area.ForSquare(4);
		var trapezoidArea = area.ForTrapezoid(2, 4, 3);
		var triangleArea = area.ForTriangle(3, 4);

		//Assert
		Assert.AreEqual(8, parallelogram);
		Assert.AreEqual(8, rectanglArea);
		Assert.AreEqual(16, squareArea);
		Assert.AreEqual(9, trapezoidArea);
		Assert.AreEqual(6, triangleArea);
	}
}