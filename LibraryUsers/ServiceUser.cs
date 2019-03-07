using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryUsers
{
    public static class ServiceUser
    {
        public static bool CheckLogin(string login)
        {
            List<User> users = Service.DeSerializeXml<User[]>("Users.xml").ToList();
                if (users.Find(i=>i.Login==login)==null)
                    return true;
                else return false;
            
           
        }
        public static User RegisterUser(string login, string password, string name, string address, string phone)
        {
            User user = new User(login, password, name, address, phone);
            List<User> users = new List<User>(Service.DeSerializeXml<User[]>("Users.xml"));
            if (users.Count!=0)
            {
                users.GroupBy(i => i.Id);
                user.Id = users[users.Count - 1].Id + 1;
            }
            else user.Id = 1;
            users.Add(user);
            Service.SerializeXml<User[]>("Users.xml", users.ToArray());
            return user;
        }
        public static User LoginUser(string login, string password)
        {
            List<User> users = new List<User>(Service.DeSerializeXml<User[]>("Users.xml"));
            if (users == null)
                throw new Exception("User list is empty.");
            if (users.Where(i => i.Login == login).First().Password == password)
                return users.Where(i => i.Login == login).First();
            else throw new Exception("Incorrect login or password!");
        }

        public static Book SearchBook(string name)
        {
            List<Book> books = Service.DeSerializeXml<Book[]>("Books.xml").ToList();
            try
            {
                return books.Where(i => i.Name == name).First();
            }
            catch
            {
                throw new Exception("Book not found!");
            }
        }
        public static Book SearchBook(int id)
        {
            List<Book> books = Service.DeSerializeXml<Book[]>("Books.xml").ToList();
            try
            {
                return books.Where(i => i.Id == id).First();
            }
            catch
            {
                throw new Exception("Book not found!");
            }
        }
    }
}
