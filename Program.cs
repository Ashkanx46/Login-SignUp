using login.Models;
using login.Repositories;
using login.Services;
using login.Data;
using Microsoft.EntityFrameworkCore;





class Program
{


    
    static void Main()
    {

        var options = new DbContextOptionsBuilder<AppDbContext>()
        .UseSqlite("Data Source=users.db")
        .Options;

        var db = new AppDbContext(options);
        db.Database.EnsureCreated(); // اگه دیتابیس وجود نداشت، بسازش



        var repo = new UserRepository(db);
        

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
                Console.WriteLine("4. Delete Account"); // 👈 اضافه شد
                Console.WriteLine("5. Exit");

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

                    if (auth.Signup(username!, password!))
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
                    string? username = Console.ReadLine();
                    if (string.IsNullOrWhiteSpace(username))
                    {
                        Console.WriteLine("Username cannot be empty!");
                        continue;
                    }

                    Console.Write("Enter password: ");
                    string? password = Console.ReadLine();
                    if (string.IsNullOrWhiteSpace(password))
                    {
                        Console.WriteLine("Password cannot be empty!");
                        continue;
                    }

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
string? newPassword = Console.ReadLine();

if (string.IsNullOrWhiteSpace(newPassword))
{
    Console.WriteLine("Password cannot be empty!");
    continue;
}

                            if (auth.ChangePassword(currentUser.Username, newPassword!))
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
                else if (choice == "4") // حذف اکانت
                {
                    if (currentUser != null)
                    {
                        if (auth.DeleteAccount(currentUser.Username))
                        {
                            Console.WriteLine("Your account has been deleted successfully.");
                            currentUser = null; // چون اکانتش حذف شده
                        }
                        else
                        {
                            Console.WriteLine("Failed to delete account.");
                        }
                    }
                }

                else if (choice == "5") // Exit
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
