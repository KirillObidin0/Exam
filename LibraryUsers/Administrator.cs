using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryUsers
{
    [Serializable]
    public class Administrator:User
    {
        public Administrator() { }
        public Administrator(string login, string password, string name, string address, string phone) 
            :base(login, password, name, address, phone) { }
   
        public static List<Book> GetAllBooks()
        {
            return Service.DeSerializeXml<Book[]>("Books.xml").ToList();
        }
        public static List<Report> GetUnreturnedBooksReports()
        {
            List<Report> Allreports = Service.DeSerializeXml<Report[]>("Reports.xml").ToList();
            return Allreports.Where(i => i.BookNotReturned = true).ToList();
        }
        public static User SearchUser(int id)
        {
            List<User> users = Service.DeSerializeXml<User[]>("Users.xml").ToList();
            return users.Where(i => i.Id == id).First();
        }
        public static Administrator RegisterAdmin
            (string login, string password, string name, string address, string phone)
        {
            Administrator admin = new Administrator(login, password, name, address, phone);
            List<Administrator> admins = new List<Administrator>();
            admins.Add(admin);
            Service.SerializeXml("Admins.xml", admins.ToArray());
            return admin;
        }
        public static Administrator LoginAdmin(string login, string password)
        {
            List<Administrator> admins = new List<Administrator>(Service.DeSerializeXml<Administrator[]>("Admins.xml"));
            if (admins.Count == 0)
                throw new IndexOutOfRangeException("admin list is empty.");
            if (admins.Where(i => i.Login == login).First().Password == password)
                return admins.Where(i => i.Login == login).First();
            else throw new Exception("Incorrect login or password!");
        }
    }
}
