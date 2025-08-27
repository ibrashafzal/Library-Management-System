using System.Collections.Generic;
using System.Linq;

namespace Library_Management.DAL
{
    public class StudentDAL
    {
        public List<Student> GetAllStudents()
        {
            using (var db = new LMSEntities2())
            {
                return db.Students.ToList();
            }
        }
        public void AddNewStudent(Student student)
        {
            using(var db = new LMSEntities2())
            {
                db.Students.Add(student);
                db.SaveChanges();
            }
        }
        public void DeleteStudent(int id)
        {
            using(var db = new LMSEntities2())
            {
               Student student =db.Students.Where(s => s.Id == id).FirstOrDefault();
                db.Students.Remove(student);
                db.SaveChanges();
            }
        }
        public void UpdateStudent(Student student)
        {
            using (var db = new LMSEntities2())
            {
                var existingstudent = db.Students.Find(student.Id);
                if (existingstudent != null)
                {
                    existingstudent.Name = student.Name;
                    existingstudent.Email = student.Email;
                    existingstudent.Class = student.Class;
                    existingstudent.contact = student.contact;
                    db.SaveChanges() ;
                }

            }
        }
        



    }
}
