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
            //Select All records in the Enrollments table.

           List<Enrollment> allrecords= dbContext.Enrollments.Include(s => s.Student).Include(s => s.Course).Include(s=>s.Course.College).ToList();
            Console.WriteLine("Feteched Records");
            ViewData["title"] = "All Records";
            ViewData["data"] = allrecords;
            return View("data");
            
            */
            

           

            //Select All records in the Enrollments table for student Ross.

            List<Enrollment> ross_records = dbContext.Enrollments.Include(s => s.Student).Include(s => s.Course).Include(s => s.Course.College).Where(s => s.Student.Name.Equals("Ross")).ToList();
            Console.WriteLine("Feteched Records for Ross");
            ViewData["title"] = "Records of Ross";
            ViewData["data"] = ross_records;
            return View("data");

            




            /*

             //Task for Students to query based on Course Abbreviation

             //Select All records in the Enrollments table with  college Abbreviation "CoE"

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
                
            string[] Colleges = {
                                 "Muma College of Business, MCOB",
                                 "College of Engineering, CoE",
                                 "College of Arts and Sciences, CAS",
                                 "College of Nursing, CON",
                                 "Morsani College of Medicine,MCOM",
                                 "College of Public Health,COPH"
            };
            string[] Courses = {
                "ISM 6225, Distributed Information Systems",
                "ISM 6218, Advanced Database Management Systems",
                "ISM 6137, Statistical Data Mining",
                "ISM 6419, Data Visualization",
                "ISM 6930, Blockchain Fundamentals",
                "ISM 6562, Big Data for Business",
                "ISM 6328, Information Security and IT Risk Management",
                "QMB 6304, Analytical Methods For Business",
                "ISM 6136, Data Mining",
                "NUR 3125, Pathophysiology for Nursing Practice",
                "NUR 3145, Pharmacology in Nursing Practice",
                "NUR 4165, Nursing Inquiry",
                "NUR 3078, Information Technology Skills for Nurses",
                "NUR 4169, Evidence-Based Practice for Baccalaureate Prepared Nurse",
                "NUR 4634, Population Health",
                "NSP 3147, Web-Based Education for Staff Development"

            };
            
            string[] Students = {
                "Monica", "Sara","Adam","Jude","Callie","Ross","Stark",
                "Chandler","Phoebe","Carrie","Tristan","sally","Robert",
                "Sid","Warner","Joey","Andy","Conner","Ruby","Kate"
            };
            string[] Grades = { "A", "A-", "B+", "B", "B-" };
            int[] scores = { 95, 91, 87, 82, 75,66,55,80,63,90,45,60};

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
