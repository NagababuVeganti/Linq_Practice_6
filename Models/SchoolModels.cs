namespace WebApplication1.Models
{
    public class College
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Abbreviation { get; set; }
        public List<Course> Courses { get; set; }
    }

    public class Course
    {
        public int Id { get; set; }
        public string Number { get; set; }
        public string Name { get; set; }
        public College College { get; set; }
        public List<Enrollment> Enrollments { get; set; }
    }
    public class Student
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Enrollment> Enrollments { get; set; }
    }
    public class Enrollment
    {
        public int Id { get; set; }
        public Course Course { get; set; }
        public Student Student { get; set; }
        public string Grade { get; set; }
        public int score { get; set; }
    }
}