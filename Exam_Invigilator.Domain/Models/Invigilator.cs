namespace Exam_Invigilator.Domain.Models
{
    public class Invigilator
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Contact { get; set; }
        public List<Availability> Availabilities { get; set; }
    }

}
