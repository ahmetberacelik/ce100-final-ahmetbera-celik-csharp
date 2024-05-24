internal class Program {
  private static void Main(string[] args) {
    Console.WriteLine("Hometracker Application Running..");
    var hometrackerLibrary = new HometrackerLibrary.Hometracker();
    hometrackerLibrary.Add(2, 2);
    hometrackerLibrary.Multiply(2, 2);
    hometrackerLibrary.Subtract(2, 2);
    hometrackerLibrary.Divide(2, 2);
  }
}
