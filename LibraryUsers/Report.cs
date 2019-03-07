using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryUsers
{
    [Serializable]
    public struct Report
    {
        public int UserId { get; set; }
        public int BookId { get; set; }
        public DateTime IssueDate { get; set; }
        public DateTime DateReturned { get; set; }
        public bool BookNotReturned { get; set; }

        public static void ReportFix()
        {
            List<Report> reports = new List<Report>();
            reports.Add(new Report());
            Service.SerializeXml("Reports.xml", reports.ToArray());
        }
    }
}
