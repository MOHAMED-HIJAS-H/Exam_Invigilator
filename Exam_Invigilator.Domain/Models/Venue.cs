using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exam_Invigilator.Domain.Models
{
    public class Venue
    {
        public int Id { get; set; }
        public string Name { get; set; }     // e.g., IT Block
        public string HallNumber { get; set; }  // e.g., Hall 201
    }

}
