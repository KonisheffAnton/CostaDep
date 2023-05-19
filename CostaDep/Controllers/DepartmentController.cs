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
    public class DepartmentController : Controller
    {

        private ApplicationContext _context;
        private readonly IMapper _mapper;

        public DepartmentController(IMapper mapper, ApplicationContext context)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IActionResult> DepartmentList()
        {
            var department = await _context.Departments.AsQueryable().ToListAsync();
            return View(department);
        }

        public IActionResult Create()
        {
            var departments = _context.Departments.FromSqlRaw("SELECT * FROM Departments").ToList();
            ViewBag.Departments = departments;
            return View();
        }

        [HttpPost]
        public IActionResult Create(Department department)
        {
            if (ModelState.IsValid)
            {
                _context.Departments.Add(department);
                _context.SaveChanges();
                return RedirectToAction("DepartmentList");
            }

            var departments = _context.Departments.ToList();
            ViewBag.Departments = departments;
            return View(department);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            var department = await _context.Departments.FindAsync(id);
            if (department == null)
            {
                return NotFound();
            }

            ViewBag.Departments = await _context.Departments.Where(dep=>dep.Id!=id).ToListAsync();

            return View(department);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Department department)
        {
            if (ModelState.IsValid)
            {
                var parentDepartment = await _context.Departments.FindAsync(department.ParentDepartmentId);
                department.ParentDepartment = parentDepartment;

                _context.Update(department);
                await _context.SaveChangesAsync();

                return RedirectToAction("DepartmentList");
            }

            ViewBag.Departments = await _context.Departments.ToListAsync();

            return View(department);
        }

        [HttpGet]
        [ActionName("Delete")]
        public async Task<IActionResult> ConfirmDelete(int? id)
        {
            if (id != null)
            {
                var department = await _context.Departments
                    .FromSqlRaw("SELECT * FROM Departments WHERE Id = {0}", id).FirstOrDefaultAsync();
                if (department != null)
                    return View(department);
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int? id)
        {

            var department = _context.Departments
                .Include(dep => dep.ChildDepartments)
                .Include(dep => dep.Employees)
                .FirstOrDefault(dep => dep.Id == id);

            if (department == null)
            {
                return NotFound();
            }

            var childDepartments = department.ChildDepartments.ToList();
            foreach (var childDepartment in childDepartments)
            {
                childDepartment.ParentDepartmentId = department.ParentDepartmentId;
            }

            _context.Departments.Remove(department);
            _context.SaveChanges();

            return RedirectToAction("DepartmentList");
        }
    }
}
