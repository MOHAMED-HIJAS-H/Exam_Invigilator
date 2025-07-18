using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exam_Invigilator.Domain.Models
{
    public class Availability
    {
        public int Id { get; set; }
        public DayOfWeek Day { get; set; }
        public int InvigilatorId { get; set; }
        public Invigilator Invigilator { get; set; }
    }

}
