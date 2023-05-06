using EmployeeManagement.Database;
using EmployeeManagement.Models;
using DTO.Models.Department;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagement.Repositories
{
    public class DepartmentRepository
    {
        private readonly IDbContextFactory<DatabaseContext> _factory;

        public DepartmentRepository(IDbContextFactory<DatabaseContext> factory)
        {
            _factory = factory;
        }

        //
        // 2 sposob
        public Guid Create(Department department)
        {
            using var context = _factory.CreateDbContext();
            var externalId = Guid.NewGuid();

            department.ExternalId = externalId;
            context.Departments.Add(department);

            context.SaveChanges();

            return externalId;
        }


        public IEnumerable<Guid> CreateMany(List<Department> departments)
        {
            using var context = _factory.CreateDbContext();

            var departmentsExternalIds = new List<Guid>();

            if (departments.GroupBy(x => x).Where(g => g.Count() > 1).Count() == 0 && departments.Count > 0)
            {
                foreach (var department in departments)
                {
                    var externalId = Guid.NewGuid();
                    department.ExternalId = externalId;
                    context.Departments.Add(department);
                    departmentsExternalIds.Add(externalId);
                }
                context.SaveChanges();
                return departmentsExternalIds;
            }
            return new List<Guid>();
        }

        public Department GetDepartment(Guid externalId)
        {
            using var context = _factory.CreateDbContext();
            return context.Departments.FirstOrDefault(x => x.ExternalId == externalId);
        }



        public Department Find(Guid externalID)
        {
            using var context = _factory.CreateDbContext();
            return context.Departments.FirstOrDefault(x => x.ExternalId == externalID);
        }

        public IEnumerable<Department> FindMany(IEnumerable<Guid> departmentsExternalIds)
        {
            using var context = _factory.CreateDbContext();
            return context.Departments.Where(x => departmentsExternalIds.Contains(x.ExternalId)).ToList();
        }

        //public IEnumerable<Guid> FindManyDepartments(List<Guid> externalId)
        //{
        //    using var context = _factory.CreateDbContext();
        //    var foundDepartments = context.Departments.Where(x => externalId.Contains(x.ExternalId)).ToList();
        //    return foundDepartments.Select(x => x.ExternalId);
        //}

        //public Guid EditDepartment(Guid externalId, UpdateDepartmentModel departmentToUpdate)
        //{
        //    var department = Find(externalId);

        //    department.Name = departmentToUpdate.Name;

        //    using var context = _factory.CreateDbContext();

        //    context.Update(department);
        //    context.SaveChanges();

        //    return department.ExternalId;
        //}

        public Guid Edit(Department department)
        {
            using var context = _factory.CreateDbContext();

            var currentDepartment = Find(department.ExternalId);
            currentDepartment.Name = department.Name;

            context.Update(currentDepartment);
            context.SaveChanges();

            return currentDepartment.ExternalId;
        }

        public IEnumerable<Guid> EditMany(IEnumerable<UpdateDepartmentModel> departmentsToUpdate)
        {
            //var departments = FindManyDepartments(externalId);

            var containsDuplicates = departmentsToUpdate.GroupBy(x => x.Name).Select(x => x.Count()).FirstOrDefault(x => x > 1) == null;

            if (!containsDuplicates)
            {
                var departmentsIds = departmentsToUpdate.Select(x => x.ExternalId).ToList();

                using var context = _factory.CreateDbContext();

                var foundDepartments = context.Departments.Where(x => departmentsIds.Contains(x.ExternalId)).ToList();

                var isAllExist = foundDepartments.Count() == departmentsToUpdate.Count();

                if (isAllExist)
                {
                    var mergedTwoList = departmentsToUpdate.Zip((foundDepartments), (toUpdate, founded) => new { ToUpdate = toUpdate, Founded = founded }).ToList();

                    foreach (var pairData in mergedTwoList)
                    {
                        pairData.Founded.Name = pairData.ToUpdate.Name;
                        context.Update(pairData.Founded);

                    }
                    //context.Update(mergedTwoList.Select(x => x.Founded).ToList());

                    context.SaveChanges();

                    return foundDepartments.Select(x => x.ExternalId);
                }
                return null;
            }
            return null;
        }

        public void Delete(Guid externalId)
        {
            using var context = _factory.CreateDbContext();

            var departmentToDelete = Find(externalId);

            context.Remove(departmentToDelete);
            context.SaveChanges();
        }

        public void DeleteMany(IEnumerable<Guid> departmentsExternalIds)
        {
            using var context = _factory.CreateDbContext();

            var departmentsToDelete = FindMany(departmentsExternalIds);

            context.RemoveRange(departmentsToDelete);
            context.SaveChanges();
        }
    }
}
