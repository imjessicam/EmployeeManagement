using EmployeeManagement.Repositories;
using DAO.Department;
using DTO.Models.Department;
using Microsoft.AspNetCore.Mvc;

namespace WebAPIEmployeeManagement.Controllers
{
    [ApiController]
    [Route("department")]
    public class DepartmentController : ControllerBase
    {
        //private DepartmentRepository _departmentRepository;
        private readonly DepartmentDao _departmentDao;

        public  DepartmentController(DepartmentRepository departmentRepository, DepartmentDao departmentDao)
        {
            //_departmentRepository = departmentRepository;
            _departmentDao = departmentDao;
        }

        [HttpPost]
        [Route("create")]

        public IActionResult Create([FromBody] CreateDepartmentModel department)
        {
            return Ok(_departmentDao.Create(department));
        }

        [HttpPost]
        [Route("createMany")]
        public IActionResult CreateMany([FromBody] List<CreateDepartmentModel> departments)
        {
            return Ok(_departmentDao.CreateMany(departments));
        }

        [HttpGet]
        [Route("find")]
        public IActionResult Find(Guid departmentExternalId)
        {
            var foundDepartment = _departmentDao.Find(departmentExternalId);
            if (foundDepartment == null)
            {
                return NotFound();
            }
            return Ok(foundDepartment);
        }

        [HttpGet]
        [Route("findMany")]
        public IActionResult FindMany([FromQuery] IEnumerable<Guid> departmentsExternalIds)
        {
            var foundDepartments = _departmentDao.FindMany(departmentsExternalIds);

            if (foundDepartments.Count() == departmentsExternalIds.Count())
            {
                var mappedElements = new List<object>();

                foreach (var department in foundDepartments)
                {
                    mappedElements.Add(new { Name = department.Name });
                }
                return Ok(mappedElements);
            }
            return Ok();
        }


        [HttpPut]
        [Route("update")]
        public IActionResult Update([FromBody] UpdateDepartmentModel departmentToUpdate)
        {
            var department = _departmentDao.Find(departmentToUpdate.ExternalId);    
            if(department == null)
            {
                return StatusCode(StatusCodes.Status406NotAcceptable);
            }
            return Ok(_departmentDao.Edit(departmentToUpdate));
        }

        [HttpPut]
        [Route("updateMany")]
        public IActionResult UpdateMany([FromBody] IReadOnlyList<UpdateDepartmentModel> departmentsToUpdate)
        {
            var departmentsExternalId = departmentsToUpdate.Select(x => x.ExternalId).ToList();

            var departments = _departmentDao.FindMany(departmentsExternalId);

            if (departments.Count() == 0)
            {
                return StatusCode(StatusCodes.Status406NotAcceptable);
            }

            return Ok(_departmentDao.EditMany(departmentsToUpdate));
        }

        [HttpDelete]
        [Route("delete")]

        public IActionResult DeleteDepartment(Guid departmentExternalId)
        {
            var department = _departmentDao.Find(departmentExternalId);
            if(department == null)
            {
                return StatusCode(StatusCodes.Status406NotAcceptable);
            }

            _departmentDao.Delete(departmentExternalId);

            return Ok();
        }

        [HttpDelete]
        [Route("deleteMany")]

        public IActionResult DeleteMany(List<Guid> departmentExternalId)
        {
            var foundDepartment = _departmentDao.FindMany(departmentExternalId);
            if (foundDepartment.Count() == 0)
            {
                return StatusCode(StatusCodes.Status406NotAcceptable);
            }

            _departmentDao.DeleteMany(departmentExternalId);

            var objects = foundDepartment.Count();

            return Ok($"Number or removed objects: {objects}.");
        }
    }
}
