using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.RequestModels.Report
{
    public class ReportRequestModel
    {
        public int UserId { get; set; }
        public int PostId { get; set; }
        public string title { get; set; }
        public string? Description { get; set; }
        public DateTime Date { get; set; }
    }
}
