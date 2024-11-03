using Core_APi_model.Model;

namespace Core_APi_model.Repository
{
    public interface IStudentRepository
    {
        Task<List<Student>>GetAll();
        Task<Student> CreateStudet(Student student);
        Task<Student> UpdateStudent(Student student);
        Task<int> DeleteStudent(int Id);
    }
}
