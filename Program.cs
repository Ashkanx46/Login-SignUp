using login.Models;
using login.Repositories;
using login.Services;

class Program
{
    static void Main()
    {
        var repo = new UserRepository();
        var auth = new AuthService(repo);

        User currentUser = null;

        while (true)
        {
            Console.Clear();

            // Header
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("=================================");
            Console.WriteLine("       User Management App       ");
            Console.WriteLine("=================================");

            if (currentUser != null)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"Welcome, {currentUser.Username}!");
                Console.WriteLine("=================================");
            }

            Console.ForegroundColor = ConsoleColor.White;

            // Menu depends on login state
            if (currentUser == null)
            {
                Console.WriteLine("1. Signup");
                Console.WriteLine("2. Login");
                Console.WriteLine("3. Exit");
            }
            else
            {
                Console.WriteLine("1. Show Users");
                Console.WriteLine("2. Change Password");
                Console.WriteLine("3. Logout");
                Console.WriteLine("4. Exit");
            }

            Console.WriteLine("=================================");
            Console.Write("Choose an option: ");
            string choice = Console.ReadLine();
            Console.WriteLine();

            // Handle choices
            if (currentUser == null)
            {
                if (choice == "1") // Signup
                {
                    Console.Write("Enter new username: ");
                    string username = Console.ReadLine();

                    Console.Write("Enter new password: ");
                    string password = Console.ReadLine();

                    if (auth.Signup(username, password))
                    {
                        currentUser = repo.GetUser(username); // auto-login
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("Signup successful. You are now logged in!");
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Username already exists.");
                    }
                }
                else if (choice == "2") // Login
                {
                    Console.Write("Enter username: ");
                    string username = Console.ReadLine();

                    Console.Write("Enter password: ");
                    string password = Console.ReadLine();

                    if (auth.Login(username, password))
                    {
                        currentUser = repo.GetUser(username);
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine($"Login successful. Welcome {currentUser.Username}!");
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Invalid username or password.");
                    }
                }
                else if (choice == "3") // Exit
                {
                    break;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Invalid option. Please try again.");
                }
            }
            else
            {
                if (choice == "1") // Show Users
                {
                    Console.WriteLine("Registered Users:");
                    var users = repo.GetAllUsers();
                    if (users.Count == 0)
                    {
                        Console.WriteLine("No users found.");
                    }
                    else
                    {
                        foreach (var user in users)
                        {
                            Console.WriteLine($"- {user.Username}");
                        }
                    }
                }
                else if (choice == "2") // Change Password
                        {
                            Console.Write("Enter new password: ");
                            string newPassword = Console.ReadLine();

                            if (auth.ChangePassword(currentUser.Username, newPassword))
                            {
                                Console.ForegroundColor = ConsoleColor.Green;
                                Console.WriteLine("Password changed successfully.");
                            }
                            else
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("Error changing password.");
                            }
                        }

                        else if (choice == "3") // Logout
                {
                    Console.WriteLine($"{currentUser.Username} logged out.");
                    currentUser = null;
                }
                else if (choice == "4") // Exit
                {
                    break;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Invalid option. Please try again.");
                }

                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("\nPress any key to continue...");
                Console.ReadKey();
            }
        }
    }
}
