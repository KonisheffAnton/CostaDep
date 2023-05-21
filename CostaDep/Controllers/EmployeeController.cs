using AutoMapper;
using Costa.Data;
using Costa.Entities;
using Costa.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Costa.Controllers
{
    public class EmployeeController : Controller
    {

        private readonly ApplicationContext _context;
        private readonly IMapper _mapper;

        public EmployeeController(IMapper mapper, ApplicationContext context)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> EmployeesByDepartment(Guid? departmentId)
        {
            var departments = _context.Department.AsQueryable().ToList();
            ViewBag.Departments = departments;

            if (!departmentId.HasValue && departments.Any())
            {
                departmentId = departments.First().Id;
            }

            ViewBag.SelectedDepartmentId = departmentId.ToString();

            var employees = await _context.Employee
                     .FromSqlRaw("SELECT * FROM /*Employee*/Empoyee WHERE DepartmentId = {0}", departmentId).ToListAsync();

            var employeeDtos = _mapper.Map<IEnumerable<EmployeeViewModel>>(employees);

            return View(employeeDtos);

        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var departments = _context.Department.AsQueryable().ToList();
            ViewBag.Departments = departments;
            return View(new EmployeeViewModel { });
        }

        [HttpPost]
        public async Task<IActionResult> Create(EmployeeViewModel employeeViewModel)
        {
            var employee = _mapper.Map<Employee>(employeeViewModel);
            if (ModelState.IsValid)
            {
                _context.Employee.Add(employee);
                _context.SaveChanges();
                return RedirectToAction("EmployeesByDepartment");
            }

            var departments = _context.Department.AsQueryable().ToList();
            ViewBag.Departments = departments;

            return View(employeeViewModel);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(decimal? id)
        {
            var employee = await _context.Employee
                .FromSqlRaw("SELECT * FROM /*Employee*/Empoyee WHERE Id = {0}", id).FirstOrDefaultAsync();
            if (employee == null)
            {
                return NotFound();
            }

            ViewBag.Departments = _context.Department.AsQueryable().ToList();

            return View(_mapper.Map<EmployeeViewModel>(employee));
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EmployeeViewModel employeeViewModel)
        {
            var employee = _mapper.Map<Employee>(employeeViewModel);

            if (ModelState.IsValid)
            {
                _context.Attach(employee);
                _context.Entry(employee).State = EntityState.Modified;
                _context.SaveChanges();
                return RedirectToAction("EmployeesByDepartment");
            }

            ViewBag.Departments = _context.Department.ToList();

            return View(employeeViewModel);
        }

        [HttpGet]
        [ActionName("Delete")]
        public async Task<IActionResult> ConfirmDelete(decimal? id)
        {
            if (id != null)
            {
                var employee = await _context.Employee
                    .FromSqlRaw("SELECT * FROM /*Employee*/Empoyee WHERE Id = {0}", id).FirstOrDefaultAsync();
                if (employee != null)
                    return View(employee);
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Delete(decimal? id)
        {
            if (id != null)
            {
                var employee = await _context.Employee
                    .FromSqlRaw("SELECT * FROM /*Employee*/Empoyee WHERE Id = {0}", id).FirstOrDefaultAsync();
                if (employee != null)
                {
                    _context.Employee.Remove(employee);
                    await _context.SaveChangesAsync();
                    return RedirectToAction("EmployeesByDepartment");
                }
            }
            return NotFound();
        }
    }
}
