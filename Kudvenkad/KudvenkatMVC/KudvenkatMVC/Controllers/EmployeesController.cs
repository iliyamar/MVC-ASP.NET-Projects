using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using KudvenkatMVC.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace KudvenkatMVC.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly EmployeeDbContext _context;

        public EmployeesController(EmployeeDbContext context)
        {
            _context = context;
        }

        // GET: Employees
        public async Task<IActionResult> Index()
        {
            var employeeDbContext = _context.Employees.Include(e => e.Department);
            return View(await employeeDbContext.ToListAsync());
        }


        public async Task<IActionResult> EmplyeesByDepartment()
        {
            var employees = await _context.Employees.Include(e => e.Department)
                                                      .GroupBy(x => x.Department.Name)
                                                      .Select(y => new DepartmentTotals
                                                      {
                                                          Name = y.Key,
                                                          Total = y.Count()
                                                      }
                                                      ).OrderBy(y=>y.Total).ToListAsync();
            return View(employees);
        }


        // GET: Employees/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await _context.Employees
                .Include(e => e.Department)
                .SingleOrDefaultAsync(m => m.EmployeeId == id);
            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }

        // GET: Employees/Create
        public IActionResult Create()
        {
           
            var departmentsData = _context.Departments
               .Select(d => new SelectListItem
               {
                   Text = d.Name,
                   Value = d.Id.ToString(),
               }).ToList();
            departmentsData.Insert(0, (new SelectListItem { Text = "Enter Department", Value = "",  Selected =true }));

            ViewData["DepartmentNames"] = departmentsData;

           // ViewData["DepartmentId"] = new SelectList(_context.Departments, "Id", "Id");
          

            return View();
        }

        // POST: Employees/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EmployeeId,Gender,City,DepartmentId")] Employee employee)
        {
            if (string.IsNullOrEmpty(employee.Name))
            {
                ModelState.AddModelError("Name", "The Name field is requred");
            }

            if (ModelState.IsValid)
            {
                _context.Add(employee);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            var departmentsData = _context.Departments
                .Select(d => new SelectListItem
                {
                    Text = d.Name,
                    Value = d.Id.ToString(),
                }).ToList();
            departmentsData.Insert(0, (new SelectListItem { Text = "Enter Department", Value = "", Selected = true })); 

            ViewData["DepartmentNames"] = departmentsData;

            return View(employee);
        }

        // GET: Employees/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await _context.Employees.SingleOrDefaultAsync(m => m.EmployeeId == id);
            if (employee == null)
            {
                return NotFound();
            }
            //ViewData["DepartmentId"] = new SelectList(_context.Departments, "Id", "Name", employee.DepartmentId);
            var departmentsData = _context.Departments
               .Select(d => new SelectListItem
               {
                   Text = d.Name,
                   Value = d.Id.ToString(),
               }).ToList();
            departmentsData.Insert(0, (new SelectListItem { Text = "Enter Department", Value = "", Selected = true }));

            ViewData["DepartmentNames"] = departmentsData;
            return View(employee);
        }

        // POST: Employees/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([Bind( "EmployeeId","Gender","City","DepartmentId")] Employee employee)
        {
            Employee employeeFromDb = _context.Employees.Single(x => x.EmployeeId == employee.EmployeeId);
          //employeeFromDb.EmployeeId = employee.EmployeeId;
            employeeFromDb.Gender = employee.Gender;
            employeeFromDb.City = employee.City;
            employeeFromDb.DepartmentId = employee.DepartmentId;
           // employee.Name = employeeFromDb.Name;
           // employeeFromDb.Name = employee.Name;

            //if (id != employee.EmployeeId)
            //{
            //    return NotFound();
            //}

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(employeeFromDb);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmployeeExists(employee.EmployeeId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }

            var departmentsData = _context.Departments
               .Select(d => new SelectListItem
               {
                   Text = d.Name,
                   Value = d.Id.ToString(),
               }).ToList();
            departmentsData.Insert(0, (new SelectListItem { Text = "Enter Department", Value = "", Selected = true }));

            ViewData["DepartmentNames"] = departmentsData;
           // ViewData["DepartmentId"] = new SelectList(_context.Departments, "Id", "Name", employee.DepartmentId);
            return View(employee);
        }

        // GET: Employees/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await _context.Employees
                .Include(e => e.Department)
                .SingleOrDefaultAsync(m => m.EmployeeId == id);
            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }

        // POST: Employees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var employee = await _context.Employees.SingleOrDefaultAsync(m => m.EmployeeId == id);
            _context.Employees.Remove(employee);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EmployeeExists(int id)
        {
            return _context.Employees.Any(e => e.EmployeeId == id);
        }

    
    }
}
