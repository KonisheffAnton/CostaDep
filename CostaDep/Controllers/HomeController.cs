using AutoMapper;
using Costa.Data;
using Costa.Entities;
using Costa.Models;
using Microsoft.AspNetCore.Mvc;

namespace Costa.Controllers
{
    public class HomeController : Controller
    {

        private readonly ApplicationContext _context;
        public HomeController(ApplicationContext context)
        {
            _context = context;

        }
        public async Task<IActionResult> Index()
        {
            var departments = _context.Department.AsQueryable().ToList();

            var departmentViewModels = departments
                .Where(d => d.ParentDepartmentId == null)
                .Select(d => new DepartmentViewModel
                {
                    DepartmentViewModelId = d.Id,
                    Name = d.Name,
                    Code = d.Code,
                    Employees = d.Employees,
                    ChildDepartments = GetChildDepartments(d.Id, departments)
                })
                .ToList();

            return View(departmentViewModels);
        }
        private static List<DepartmentViewModel> GetChildDepartments(Guid parentDepartmentId, IEnumerable<Department> departments)
        {
            return departments
                .Where(d => d.ParentDepartmentId == parentDepartmentId)
                .Select(d => new DepartmentViewModel
                {
                    DepartmentViewModelId = d.Id,
                    Name = d.Name,
                    Code = d.Code,
                    Employees = d.Employees,
                    ChildDepartments = GetChildDepartments(d.Id, departments)
                })
                .ToList();
        }

    }
}
