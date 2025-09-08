
using LMS.Entities;

namespace LMS
{
    public class StudentRepo
    {
        public StudentDAL studentDAL = new StudentDAL();
        public List<Student> GetStudents()
        {
            return studentDAL.GetAllStudents();
           
        }
        public void AddNew(Student student)
        {
            studentDAL.AddNewStudent(student);
        }
        public void Delete(int id)
        {
            studentDAL.DeleteStudent(id);
        }
        public void Update(Student student)
        {
            studentDAL.UpdateStudent(student);
        }
        public List<Student> Search(string searchText)
        {
            
            List<Student> students = studentDAL.GetAllStudents();
            List<Student> filtered = new List<Student>();
            if (string.IsNullOrWhiteSpace(searchText))
                return students;

            searchText = searchText.ToLower();
            foreach (Student student in students)
            {
                if ((student.Name != null && student.Name.ToLower().Contains(searchText)) ||
                    (student.Email != null && student.Email.ToLower().Contains(searchText)) ||
                    (student.Class != null && student.Class.ToLower().Contains(searchText)) ||
                    (student.Contact != null && student.Contact.ToLower().Contains(searchText)))


                {
                    filtered.Add(student);
                }
            }

            return filtered;
        }


    }
}
