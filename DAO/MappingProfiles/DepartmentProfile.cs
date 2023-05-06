using AutoMapper;
using DTO.Models.Department;

namespace DAO.MappingProfiles
{
    public class DepartmentProfile : Profile
    {
        public DepartmentProfile()
        {
            // Zdefiniowanie Mappowania
            CreateMap<EmployeeManagement.Models.Department, DepartmentDetails>();
            CreateMap<UpdateDepartmentModel, EmployeeManagement.Models.Department>();
            CreateMap<CreateDepartmentModel, EmployeeManagement.Models.Department>();

        }

    }
}
