using LibraryUsers.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryUsers
{
    [Serializable]
    public class User:IUser
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public List<int> IssuedBooksIdList { get; set; }

        public User()
        {

        }
        public User(string login, string password, string name, string address, string phone)
        {
            Login = login;
            Password = password;
            Name = name;
            Address = address;
            Phone = phone;
        }

        public void ChangePassword(string password) { Password = password; }

        public void IssueBook(int id)
        {
            List<Book> books = Service.DeSerializeXml<Book[]>("Books.xml").ToList();
            Book book = books.Where(i => i.Id == id).First();
            if (book.Avaibility == false)
                throw new Exception("Books is not avaible!");
            else
            {
                book.Avaibility = false;
                IssuedBooksIdList.Add(book.Id);
                for (int i = 0; i < books.Count; i++)
                {
                    if (books[i].Id == book.Id)
                        books[i] = book;
                }
                Service.SerializeXml<Book[]>("Books.xml", books.ToArray());
                Report report = new Report()
                {
                    BookId = book.Id,
                    UserId = Id,
                    IssueDate = DateTime.Now,
                    BookNotReturned = true
                };
                List<Report> reports = Service.DeSerializeXml<Report[]>("Reports.xml").ToList();
                reports.Add(report);
                Service.SerializeXml("Reports.xml", reports.ToArray());
                List<User> users = Service.DeSerializeXml<User[]>("Users.xml").ToList();
                for (int i = 0; i < users.Count; i++)
                {
                    if (users[i].Id == Id)
                        users[i] = this;
                }
                Service.SerializeXml<User[]>("Users.xml", users.ToArray());
            }
                    
        }
        public void ReturnBook(int id)
        {
            List<Book> books = Service.DeSerializeXml<Book[]>("Books.xml").ToList();
            Book book = books.Where(i => i.Id == id).First();
            if (book.Avaibility == true)
                throw new Exception("Books is avaible!");
            else
            {
                book.Avaibility = true;
                IssuedBooksIdList.Remove(book.Id);
                for (int i = 0; i < books.Count; i++)
                {
                    if (books[i].Id == book.Id)
                        books[i] = book;
                }
                Service.SerializeXml<Book[]>("Books.xml", books.ToArray());
                List<Report> reports = Service.DeSerializeXml<Report[]>("Reports.xml").ToList();
                Report report = reports
                    .Where(i => i.BookId == book.Id && i.UserId == Id && i.BookNotReturned == true)
                    .First();
                report.DateReturned = DateTime.Now;
                report.BookNotReturned = false;
                for (int i = 0; i < reports.Count; i++)
                {
                    if (reports[i].BookId == book.Id
                        && reports[i].UserId == Id
                        && reports[i].BookNotReturned == true)
                        reports[i] = report;
                }
                Service.SerializeXml("Reports.xml", reports.ToArray());
                List<User> users = Service.DeSerializeXml<User[]>("Users.xml").ToList();
                for (int i = 0; i < users.Count; i++)
                {
                    if (users[i].Id == Id)
                        users[i] = this;
                }
                Service.SerializeXml<User[]>("Users.xml", users.ToArray());
            }
        }

        
    }
}
