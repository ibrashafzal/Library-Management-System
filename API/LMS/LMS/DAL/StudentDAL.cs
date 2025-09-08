using LMS.Entities;

namespace LMS
{
    public class StudentDAL
    {
        public List<Student> GetAllStudents()
        {
            using (var db = new LmsContext())
            {
                return db.Students.ToList();
            }
        }
        public void AddNewStudent(Student student)
        {
            using(var db = new LmsContext())
            {
                student.CreatedAt = DateTime.Now;
                db.Students.Add(student);
                db.SaveChanges();
            }
        }
        public void DeleteStudent(int id)
        {
            using(var db = new LmsContext())
            {
               Student student =db.Students.Where(s => s.Id == id).FirstOrDefault();
                db.Students.Remove(student);
                db.SaveChanges();
            }
        }
        public void UpdateStudent(Student student)
        {
            using (var db = new LmsContext())
            {
                var existingstudent = db.Students.Find(student.Id);
                if (existingstudent != null)
                {
                    existingstudent.Name = student.Name;
                    existingstudent.Email = student.Email;
                    existingstudent.Class = student.Class;
                    existingstudent.Contact = student.Contact;
                    db.SaveChanges() ;
                }

            }
        }  
    }
}
