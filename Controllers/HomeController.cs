using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.DataAccess;
using WebApplication1.Models;

namespace WebApplication1
{
    public class HomeController : Controller
    {
        SchoolDbContext dbContext;
        public HomeController(SchoolDbContext context)
        {
            dbContext = context;
        }

        public IActionResult Index()
        {
            populateData();

            return View();
        }

        public async Task<IActionResult> Queries()
        {

            /*
             
            //Select All Query

           List<Enrollment> allrecords= dbContext.Enrollments.Include(s => s.Student).Include(s => s.Course).Include(s=>s.Course.College).ToList();
            Console.WriteLine("Feteched Records");
            ViewData["title"] = "All Records";
            ViewData["data"] = allrecords;
            return View("data");
            
             */

            

            //Select by Name Query

            List<Enrollment> ross_records = dbContext.Enrollments.Include(s => s.Student).Include(s => s.Course).Include(s => s.Course.College).Where(s => s.Student.Name.Equals("Ross")).ToList();
            Console.WriteLine("Feteched Records for Ross");
            ViewData["title"] = "Records of Ross";
            ViewData["data"] = ross_records;
            return View("data");
            
           


           /*

            //Task for Students to query based on Course Abbreviation

            //Select Query based on Collge abbreviation

            List<Enrollment> MCOB_records = dbContext.Enrollments.Include(s => s.Student).Include(s => s.Course).Include(s => s.Course.College).Where(s => s.Course.College.Abbreviation.Contains("CoE")).ToList();
            Console.WriteLine("Feteched Records for MCOB College");
            ViewData["title"] = "Records of MCOB College:";
            ViewData["data"] = MCOB_records;
            return View("data");

            */



            /* 

            // Group By Query on Student Name and College Abbrevation.

            var group_data = dbContext.Enrollments.Include(s => s.Student).Include(s => s.Course).Include(s => s.Course.College).GroupBy(s => new { s.Student.Name, s.Course.College.Abbreviation }).Select(s => new
            {
                Name = s.Key.Name,
                College=s.Key.Abbreviation,
                total_score=s.Sum(s=>s.score)

            }).ToList();

            ViewData["title"] = "Sum of Scores Grouped By College and Student Name";

            ViewData["data"] = group_data;

            return View("group");

            */
            

           
            /*
             
            //Excercise for Student 

            // Write a Group by Query to get sum of total scores by College Name or College Abbrevation.
           
            var group_data = dbContext.Enrollments.Include(s => s.Student).Include(s => s.Course).Include(s => s.Course.College).GroupBy(s => new { s.Course.College.Name }).Select(s => new
            {
                College=s.Key.Name,
                total_score=s.Sum(s=>s.score)

            }).ToList();

            ViewData["title"] = "Sum of all studnet Scores Grouped By College";

            ViewData["data"] = group_data;

            return View("collgeGroupby");

            */
            

        }


        void populateData()
        {
            Random rnd = new Random();
                
            string[] Colleges = {"Muma College of Business, MCOB",
                "College of Engineering, CoE", "College of Arts and Sciences, CAS"};
            string[] Courses = {"ISM 6225, Distributed Information Systems",
                "ISM 6218, Advanced Database Management Systems",
                "ISM 6328, Information Security and IT Risk Management"};
            string[] Students = { "Monica", "Sara","Adam","Jude","Callie","Ross","Stark","Chandler","Phoebe","Carrie","Triston"};
            string[] Grades = { "A", "A-", "B+", "B", "B-" };
            int[] scores = { 95, 91, 87, 82, 75 };

            College[] colleges = new College[Colleges.Length];
            Course[] courses = new Course[Courses.Length];
            Student[] students = new Student[Students.Length];

            for (int i = 0; i < Colleges.Length; i++)
            {
                College college = new College
                {
                    Name = Colleges[i].Split(",")[0],
                    Abbreviation = Colleges[i].Split(",")[1]
                };

                dbContext.Colleges.Add(college);
                colleges[i] = college;
            }

            for (int i = 0; i < Courses.Length; i++)
            {
                Course course = new Course
                {

                    Number = Courses[i].Split(",")[0],
                    Name = Courses[i].Split(",")[1],
                    College = colleges[rnd.Next(colleges.Length)]
                };

                dbContext.Courses.Add(course);
                courses[i] = course;
            }

            for (int i = 0; i < Students.Length; i++)
            {
                Student student = new Student
                { 
                    Name = Students[i] 
                };
                
                dbContext.Students.Add(student);
                students[i] = student;
            }

            foreach (Student student in students)
            {
                foreach (Course course in courses)
                {
                    Enrollment enrollment = new Enrollment
                    {
                        Course = course,
                        Student = student,
                        Grade = Grades[rnd.Next(Grades.Length)],
                        score=scores[rnd.Next(scores.Length)]
                    };

                    dbContext.Enrollments.Add(enrollment);
                }
            }

            dbContext.SaveChanges();
        }
    }
}
