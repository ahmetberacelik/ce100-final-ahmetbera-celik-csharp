using System;
using System.IO;
using Xunit;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace HometrackerLibrary.Tests {
public class HometrackerTests {

        [Fact]
        public void MainMenuIncorrectLoginTest()
        {
            var hometracker = new Hometracker
            {
                IsTestMode = true
            };
            bool authenticationResult = false;

            bool result = hometracker.MainMenu(authenticationResult);

            Assert.False(result);

            Console.SetOut(new StreamWriter(Console.OpenStandardOutput()));
            Console.SetIn(new StreamReader(Console.OpenStandardInput()));
        }

        [Fact]
        public void MainMenuInvalidTest()
        {
            var input = new StringReader("abc\n\n48\n\n5\n");
            Console.SetIn(input);

            var output = new StringWriter();
            Console.SetOut(output);

            var hometracker = new Hometracker
            {
                IsTestMode = true
            };

            bool authenticationResult = true;

            bool result = hometracker.MainMenu(authenticationResult);

            Assert.True(result);

            Console.SetOut(new StreamWriter(Console.OpenStandardOutput()));
            Console.SetIn(new StreamReader(Console.OpenStandardInput()));
        }

        [Fact]
        public void MainMenuValidTest()
        {
            var input = new StringReader("1\n8\n2\n\n3\n\n4\n3\n5\n");
            Console.SetIn(input);

            var output = new StringWriter();
            Console.SetOut(output);

            var hometracker = new Hometracker
            {
                IsTestMode = true
            };

            bool authenticationResult = true;

            bool result = hometracker.MainMenu(authenticationResult);

            Assert.True(result);

            Console.SetOut(new StreamWriter(Console.OpenStandardOutput()));
            Console.SetIn(new StreamReader(Console.OpenStandardInput()));
        }

        [Fact]
        public void LoginGuestModeTest()
        {
            var input = new StringReader("3\n");
            Console.SetIn(input);

            var output = new StringWriter();
            Console.SetOut(output);

            var hometracker = new Hometracker
            {
                IsTestMode = true
            };

            bool result = hometracker.UserAuthentication();

            Assert.True(result);

            Console.SetOut(new StreamWriter(Console.OpenStandardOutput()));
            Console.SetIn(new StreamReader(Console.OpenStandardInput()));
        }

        [Fact]
        public void UserAuthenticationTestCorrectLogin()
        {
            var input = new StringReader("1\nEnes Koy\n123456\n\n");
            Console.SetIn(input);

            var output = new StringWriter();
            Console.SetOut(output);

            var hometracker = new Hometracker
            {
                IsTestMode = true
            };

            bool result = hometracker.UserAuthentication();

            Assert.True(result);

            Console.SetOut(new StreamWriter(Console.OpenStandardOutput()));
            Console.SetIn(new StreamReader(Console.OpenStandardInput()));
        }
        [Fact]
        public void UserAuthenticationTestIncorrectLogin()
        {
            var input = new StringReader("1\nInvalid User\n123456\n4\n");
            Console.SetIn(input);

            var output = new StringWriter();
            Console.SetOut(output);

            var hometracker = new Hometracker
            {
                IsTestMode = true
            };

            bool result = hometracker.UserAuthentication();

            Assert.False(result);

            Console.SetOut(new StreamWriter(Console.OpenStandardOutput()));
            Console.SetIn(new StreamReader(Console.OpenStandardInput()));
        }
        [Fact]
        public void UserAuthenticationRegisterTest()
        {
            var input = new StringReader("2\nTestUser\n123456\n\n");
            Console.SetIn(input);

            var output = new StringWriter();
            Console.SetOut(output);

            var hometracker = new Hometracker
            {
                IsTestMode = true
            };

            bool result = hometracker.UserAuthentication();

            Assert.True(result);

            Console.SetOut(new StreamWriter(Console.OpenStandardOutput()));
            Console.SetIn(new StreamReader(Console.OpenStandardInput()));
        }
        [Fact]
        public void UserAuthenticationInvalidInputTest()
        {
            var input = new StringReader("invalid\n\n454\n\n4\n");
            Console.SetIn(input);

            var output = new StringWriter();
            Console.SetOut(output);

            var hometracker = new Hometracker
            {
                IsTestMode = true
            };

            bool result = hometracker.UserAuthentication();

            Assert.False(result);

            Console.SetOut(new StreamWriter(Console.OpenStandardOutput()));
            Console.SetIn(new StreamReader(Console.OpenStandardInput()));
        }
        [Fact]
        public void UtilityLoggingGuestModeTest()
        {
            var input = new StringReader("");
            Console.SetIn(input);

            var output = new StringWriter();
            Console.SetOut(output);

            var hometracker = new Hometracker
            {
                IsTestMode = true
            };
            bool guestMode = true;

            bool result = hometracker.UtilityLogging(guestMode);

            Assert.False(result);

            Console.SetOut(new StreamWriter(Console.OpenStandardOutput()));
            Console.SetIn(new StreamReader(Console.OpenStandardInput()));
        }
        [Fact]
        public void UtilityLoggingTest()
        {
            var input = new StringReader("1\n100\n2\n100\n3\n100\n4\n1\n\n4\n2\n\n4\n4545\n\n5\n1\n\n5\n2\n\n5\ni\n\n5\n45\n\n6\n\n7\n\n8\n");
            Console.SetIn(input);

            var output = new StringWriter();
            Console.SetOut(output);

            var hometracker = new Hometracker
            {
                IsTestMode = true
            };
            bool guestMode = false;

            bool result = hometracker.UtilityLogging(guestMode);

            Assert.True(result);

            Console.SetOut(new StreamWriter(Console.OpenStandardOutput()));
            Console.SetIn(new StreamReader(Console.OpenStandardInput()));
        }
        [Fact]
        public void UtilityLoggingInvalidInputTest()
        {
            var input = new StringReader("invalid\n\n454\n\n8\n");
            Console.SetIn(input);

            var output = new StringWriter();
            Console.SetOut(output);

            var hometracker = new Hometracker
            {
                IsTestMode = true
            };
            bool guestMode = false;

            bool result = hometracker.UtilityLogging(guestMode);

            Assert.True(result);

            Console.SetOut(new StreamWriter(Console.OpenStandardOutput()));
            Console.SetIn(new StreamReader(Console.OpenStandardInput()));
        }
        [Fact]
        public void ReminderSetupTest()
        {
            var input = new StringReader("2\n\n1\ntest reminder\n10\n\n2\n\n3\n");
            Console.SetIn(input);

            var output = new StringWriter();
            Console.SetOut(output);

            var hometracker = new Hometracker
            {
                IsTestMode = true
            };

            bool guestMode = false;

            bool result = hometracker.ReminderSetup(guestMode);

            Assert.True(result);

            Console.SetOut(new StreamWriter(Console.OpenStandardOutput()));
            Console.SetIn(new StreamReader(Console.OpenStandardInput()));
        }

        [Fact]
        public void ReminderSetupInvalidTest()
        {
            var input = new StringReader("invalid\n\n454\n\n3\n");
            Console.SetIn(input);

            var output = new StringWriter();
            Console.SetOut(output);

            var hometracker = new Hometracker
            {
                IsTestMode = true
            };

            bool guestMode = false;

            bool result = hometracker.ReminderSetup(guestMode);

            Assert.True(result);

            Console.SetOut(new StreamWriter(Console.OpenStandardOutput()));
            Console.SetIn(new StreamReader(Console.OpenStandardInput()));
        }
        [Fact]
        public void ReminderSetupInvalidTextTest()
        {
            var input = new StringReader("1\ntest reminder\ninvalid\n\n3\n");
            Console.SetIn(input);

            var output = new StringWriter();
            Console.SetOut(output);

            var hometracker = new Hometracker
            {
                IsTestMode = true
            };

            bool guestMode = false;

            bool result = hometracker.ReminderSetup(guestMode);

            Assert.True(result);

            Console.SetOut(new StreamWriter(Console.OpenStandardOutput()));
            Console.SetIn(new StreamReader(Console.OpenStandardInput()));
        }
        [Fact]
        public void CalculateAndShowTest()
        {
            var input = new StringReader("\n");
            Console.SetIn(input);

            var output = new StringWriter();
            Console.SetOut(output);

            var hometracker = new Hometracker
            {
                IsTestMode = true
            };
            bool guestMode = false;

            bool result = hometracker.CalculateAndShowExpenses(guestMode);

            Assert.False(result);

            Console.SetOut(new StreamWriter(Console.OpenStandardOutput()));
            Console.SetIn(new StreamReader(Console.OpenStandardInput()));
        }
    }
}
