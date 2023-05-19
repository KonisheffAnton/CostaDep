using AutoMapper;
using Costa.Data;
using Costa.Entities;
using Costa.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
            var departments = _context.Departments.ToList();

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
        private static List<DepartmentViewModel> GetChildDepartments(int parentDepartmentId, IEnumerable<Department> departments)
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
