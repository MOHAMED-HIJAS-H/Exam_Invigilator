using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exam_Invigilator.Domain.Models
{
    public class Allocation
    {
        public int Id { get; set; }
        public int InvigilatorId { get; set; }
        public int VenueId { get; set; }
        public DayOfWeek Day { get; set; }
        public bool IsApproved { get; set; }

        public Invigilator Invigilator { get; set; }
        public Venue Venue { get; set; }
    }

}
