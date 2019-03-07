using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryUsers
{
    [Serializable]
    public struct Book
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string AuthorName { get; set; }
        public DateTime PublishDate { get; set; }
        public bool Avaibility { get; set; }

        public override string ToString()
        {
            return string.Format("{0} {1} {2} {3} {4}", 
                Id, Name, AuthorName, PublishDate.Year, Avaibility);
        }
    }
}
