
namespace HometrackerLibrary.Tests {
public class HometrackerTests {
  [Fact]
  public void TestAdd() {
    Hometracker calc = new Hometracker();
    Assert.Equal(4, calc.Add(2, 2));
  }

  [Fact]
  public void TestSubtract() {
    Hometracker calc = new Hometracker();
    Assert.Equal(2, calc.Subtract(4, 2));
  }

  [Fact]
  public void TestMultiply() {
    Hometracker calc = new Hometracker();
    Assert.Equal(8, calc.Multiply(2, 4));
  }

  [Fact]
  public void TestDivide() {
    Hometracker calc = new Hometracker();
    Assert.Equal(2, calc.Divide(4, 2));
  }

  [Fact]
  public void TestDivideByZero() {
    Hometracker calc = new Hometracker();
    Assert.Throws<DivideByZeroException>(() => calc.Divide(4, 0));
  }
}
}
