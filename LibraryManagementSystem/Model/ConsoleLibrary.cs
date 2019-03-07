using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraryUsers;

namespace LibraryManagementSystem.Model
{
    public class ConsoleLibrary
    {
        public void Start()
        {
            Console.Clear();
            Console.WriteLine("Welcome to library management system!");
            int choice;
            Console.WriteLine("1 - Registration\n2 - LogIn");
            choice = Int32.Parse(Console.ReadLine());
            switch (choice)
            {
                case 1:
                    Registration();
                    break;
                case 2:
                        UserMenu(LogIn());
                    break;
                //case 3:
                //    LoginAdmin();
                //    break;
                default:
                    return;
            }
        }
        public User Registration()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Enter new login:");
                string login = Console.ReadLine();
                try
                {
                    ServiceUser.CheckLogin(login);
                }
                catch (InvalidOperationException)
                {
                    List<User> users = new List<User>();
                    Service.SerializeXml<User[]>("Users.xml", users.ToArray());
                }
                if (ServiceUser.CheckLogin(login) == false)
                {
                    Console.WriteLine("This login is already taken!");
                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey();
                    continue;
                }
                else
                {
                    Console.WriteLine("Enter password:");
                    string password = Console.ReadLine();
                    Console.WriteLine("Enter your name:");
                    string name = Console.ReadLine();
                    Console.WriteLine("Enter your address:");
                    string address = Console.ReadLine();
                    Console.WriteLine("Enter your phone number:");
                    string phone = Console.ReadLine();
                    return ServiceUser.RegisterUser(login, password, name, address, phone);
                }
            }

        }
        public User LogIn()
        {
            while (true)
            {
                Console.WriteLine("Enter login:");
                string login = Console.ReadLine();
                Console.WriteLine("Enter Password:");
                string password = Console.ReadLine();

                try
                {
                    return ServiceUser.LoginUser(login, password);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    Console.WriteLine();
                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey();
                    continue;
                }
            }

        }
        public Administrator LoginAdmin()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Enter login");
                string login = Console.ReadLine();
                Console.WriteLine("Enter password");
                string password = Console.ReadLine();

                try
                {
                    return Administrator.LoginAdmin(login, password);
                }
                catch (IndexOutOfRangeException e)
                {
                    Console.WriteLine(e.Message);
                    Console.WriteLine("Please register new admin.");
                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey();
                    return RegisterAdmin(login, password);
                }
                catch (InvalidOperationException)
                {
                    Console.WriteLine("Please register new admin.");
                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey();
                    return RegisterAdmin(login, password);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }

            }
        }
        public Administrator RegisterAdmin(string login, string password)
        {
            Console.WriteLine("Enter name:");
            string name = Console.ReadLine();
            Console.WriteLine("Enter address:");
            string address = Console.ReadLine();
            Console.WriteLine("Enter phone number:");
            string phone = Console.ReadLine();
            return Administrator.RegisterAdmin(login, password, name, address, phone);
        }

        public void UserMenu(User user)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Welcome, {0}!", user.Name);
                Console.WriteLine("Account info:");
                Console.WriteLine("Login: {0}\nIssued Books ID:", user.Login);
                if (user.IssuedBooksIdList.Count == 0) Console.WriteLine("None.");
                else
                    for (int i = 0; i < user.IssuedBooksIdList.Count; i++)
                    {
                        Console.Write(user.IssuedBooksIdList[i]);
                        if (i != user.IssuedBooksIdList.Count - 1)
                            Console.Write(", ");
                    }
                Console.WriteLine();
                int choice;
                Console.WriteLine
                    ("1 - show all books\n2 - search book by name\n3 - search book by id\n4 - issue book\n5 - return book");
                choice = Int32.Parse(Console.ReadLine());
                switch (choice)
                {
                    case 1:
                        ShowAllBooks();
                        break;
                    case 2:
                        SearchBookByName();
                        break;
                    case 3:
                        SearchBookById();
                        break;
                    case 4:
                        IssueBook(user);

                        break;
                    case 5:
                        ReturnBook(user);
                        break;
                    default:
                        Console.WriteLine("Choose from 1 to 5!");
                        Console.WriteLine("Press any key to continue...");
                        Console.ReadKey();
                        break;
                }
            }
        }


        public void ShowAllBooks()
        {
            Console.Clear();
            try
            {
                List<Book> books = Administrator.GetAllBooks();
                foreach (Book item in books)
                {
                    Console.WriteLine(item);
                }
                Console.WriteLine("Press any key...");
                Console.ReadKey();
            }
            catch (InvalidOperationException)
            {
                List<Book> books = new List<Book>();
                books.Add(new Book()
                {
                    Id = 1,
                    Name = "C#",
                    AuthorName = "Kirill",
                    PublishDate = DateTime.Now,
                    Avaibility = true
                });
                Service.SerializeXml("Books.xml", books.ToArray());
            }
        }
        public void SearchBookByName()
        {
            Console.WriteLine("Enter book name:");
            string name = Console.ReadLine();
            Console.WriteLine(ServiceUser.SearchBook(name));
            Console.WriteLine("Press any key...");
            Console.ReadKey();
        }
        public void SearchBookById()
        {
            Console.WriteLine("Enter book id:");
            int id = Int32.Parse(Console.ReadLine());
            Console.WriteLine(ServiceUser.SearchBook(id));
            Console.WriteLine("Press any key...");
            Console.ReadKey();
        }

        public User IssueBook(User user)
        {
            Console.WriteLine("Enter book id:");
            int id = Int32.Parse(Console.ReadLine());
            try
            {
                user.IssueBook(id);
            }
            catch (InvalidOperationException)
            {
                List<Report> reports = new List<Report>();
                reports.Add(new Report()
                {
                    IssueDate = DateTime.Now,
                    DateReturned = DateTime.Now,
                    BookNotReturned = true
                });
                Service.SerializeXml("Reports.xml", reports.ToArray());
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return user;
        }
        public User ReturnBook(User user)
        {
            Console.WriteLine("Enter book id:");
            int id = Int32.Parse(Console.ReadLine());
            try
            {
                user.ReturnBook(id);
            }
            catch (InvalidOperationException)
            {
                List<Report> reports = new List<Report>();
                reports.Add(new Report()
                {
                    IssueDate = DateTime.Now,
                    DateReturned = DateTime.Now,
                    BookNotReturned = true
                });
                Service.SerializeXml("Reports.xml", reports.ToArray());
            }
            return user;
        }
    }

}

