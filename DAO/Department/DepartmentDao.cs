using AutoMapper;
using EmployeeManagement.Repositories;
using DTO.Models.Department;


namespace DAO.Department
{
    public class DepartmentDao
    {
        private readonly DepartmentRepository _departmentRepository;
        private readonly IMapper _mapper;

        public DepartmentDao(DepartmentRepository departmentRepository, IMapper mapper)
        {
            _departmentRepository = departmentRepository;
            _mapper = mapper;
        }

        public Guid Create(CreateDepartmentModel createDepartmentModel)
        {
            var department = _mapper.Map<EmployeeManagement.Models.Department>(createDepartmentModel);
            // walidatcja 
            return _departmentRepository.Create(department);
        }

        public IEnumerable<Guid> CreateMany(List<CreateDepartmentModel> createDepartmentModels)
        {
            var departments = _mapper.Map<List<EmployeeManagement.Models.Department>>(createDepartmentModels);
            return _departmentRepository.CreateMany(departments);
        }

        public DepartmentDetails Find(Guid externalId)
        {
            var department = _departmentRepository.Find(externalId);
            return _mapper.Map<DepartmentDetails>(department);
        }

        public IEnumerable<DepartmentDetails> FindMany(IEnumerable<Guid> departmentExternalIds)
        {
            var departments = _departmentRepository.FindMany(departmentExternalIds);
            return _mapper.Map<IEnumerable<DepartmentDetails>>(departments);
        }


        public Guid Edit(UpdateDepartmentModel departmentToUpdate)
        {
            var department = _mapper.Map<EmployeeManagement.Models.Department>(departmentToUpdate);
            return _departmentRepository.Edit(department);
        }

        public IEnumerable<Guid> EditMany(IEnumerable<UpdateDepartmentModel> departmentsToUpdate)
        {
            var departments = _mapper.Map<IEnumerable<EmployeeManagement.Models.Department>>(departmentsToUpdate);
            return _departmentRepository.EditMany((IEnumerable<UpdateDepartmentModel>)departments);
        }

        public void Delete(Guid externalId)
        {
            _departmentRepository.Delete(externalId);
        }

        public void DeleteMany(IEnumerable<Guid> departmentsExternalIds)
        {
            _departmentRepository.DeleteMany(departmentsExternalIds);
        }
    }
}
