
using HometrackerLibrary;

/**
* @file Program.cs
* @brief Entry point for the Hometracker system application.
*/
internal class Program {
    /**
    * @brief The main method that serves as the entry point for the application.
    * @param args Command-line arguments.
    */
    private static void Main(string[] args) {
    Hometracker hometracker = new Hometracker();
        bool authenticationResult = hometracker.UserAuthentication();
        hometracker.MainMenu(authenticationResult);
  }
}
