using AutoMapper;
using Costa.Data;
using Costa.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Costa.Controllers
{
    public class DepartmentController : Controller
    {

        private readonly ApplicationContext _context;
        private readonly IMapper _mapper;

        public DepartmentController(IMapper mapper, ApplicationContext context)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> DepartmentList()
        {
            var department = await _context.Department.AsQueryable().ToListAsync();
            return View(department);
        }

        [HttpGet]
        public IActionResult Create()
        {
            var departments = _context.Department.AsQueryable().ToList();
            ViewBag.Departments = departments;
            return View();
        }

        [HttpPost]
        public IActionResult Create(Department department)
        {
            if (ModelState.IsValid)
            {
                _context.Department.Add(department);
                _context.SaveChanges();
                return RedirectToAction("DepartmentList");
            }

            var departments = _context.Department.AsQueryable().ToList();
            ViewBag.Departments = departments;
            return View(department);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid? id)
        {
            var department = await _context.Department.FindAsync(id);
            if (department == null)
            {
                return NotFound();
            }

            ViewBag.Departments = await _context.Department.Where(dep=>dep.Id!=id).ToListAsync();

            return View(department);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Department department)
        {
            if (ModelState.IsValid)
            {
                var parentDepartment = await _context.Department.FindAsync(department.ParentDepartmentId);
                department.ParentDepartment = parentDepartment;

                _context.Update(department);
                await _context.SaveChangesAsync();

                return RedirectToAction("DepartmentList");
            }

            ViewBag.Departments = await _context.Department.ToListAsync();

            return View(department);
        }

        [HttpGet]
        [ActionName("Delete")]
        public async Task<IActionResult> ConfirmDelete(Guid? id)
        {
            if (id != null)
            {
                var department = await _context.Department
                    .FromSqlRaw("SELECT * FROM Department WHERE Id = {0}", id).FirstOrDefaultAsync();
                if (department != null)
                    return View(department);
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Guid? id)
        {

            var department = _context.Department
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

            _context.Department.Remove(department);
            _context.SaveChanges();

            return RedirectToAction("DepartmentList");
        }
    }
}
