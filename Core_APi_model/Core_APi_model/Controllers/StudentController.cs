using Core_APi_model.Model;
using Core_APi_model.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Core_APi_model.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        public readonly IStudentRepository _studentrepo;

        public StudentController(IStudentRepository Studentrepo) { 
            _studentrepo = Studentrepo;
        }

        [HttpGet]
        public async Task<AcceptedResult> GetStudentList()
        {
            var students = await _studentrepo.GetAll(); // Retrieve the student list from the repository
            return Accepted(students);
        }
        [HttpPost]
        public async Task<IActionResult> CreateStudent(Student student)
        {
            try
            {
                _studentrepo.CreateStudet(student);
                return Ok(student);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}
