using Core_APi_model.Model;

namespace Core_APi_model.Repository
{
    public class StudentRepository : IStudentRepository
    {
        List<Student> students=new List<Student>();
        
        public async Task<Student> CreateStudet(Student student)
        {
           students.Add(student);
            return student;
        }

        public async Task<int> DeleteStudent(int Id)
        {
           students.RemoveAll(x => x.Id == Id);
            return Id;
        }

        public async  Task<List<Student>> GetAll()
        {
            return students.ToList();
        }

        public async Task<Student> UpdateStudent(Student student)
        {
            students.RemoveAll(x=>x.Id == student.Id);
            students.Add(student);
            return student;
        }
    }
}
