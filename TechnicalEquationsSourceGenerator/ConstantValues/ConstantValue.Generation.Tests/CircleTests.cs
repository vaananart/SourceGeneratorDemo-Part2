using NUnit.Framework;

namespace ConstantValue.Generation.Tests;

public class CircleTests
{
	private Circle _circle;

	[SetUp]
    public void Setup()
    {
		_circle = new Circle(7);
    }

    [Test]
    public void GetAreaTest()
    {
        //Arrange

		//Action
		var area = _circle.GetArea();

		//Assert
		Assert.AreEqual(154, area);
    }

	[Test]
	public void GetPerimeter()
	{ 
		//Arrange

		//Action
		var perimeter = _circle.GetPerimeter();

		//Assert
		Assert.AreEqual(44, perimeter);
	}
}