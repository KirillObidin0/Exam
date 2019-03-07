using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraryUsers;

namespace LibraryUsers.Interfaces
{
    public interface IUser
    {
        int Id { get; set; }
        string Login { get; set; }
        string Password { get; set; }
        string Name { get; set; }
        string Address { get; set; }
        string Phone { get; set; }
        List<int> IssuedBooksIdList { get; set; }

        void ChangePassword(string password);

        void IssueBook(int id);
        void ReturnBook(int id);
    }
}
