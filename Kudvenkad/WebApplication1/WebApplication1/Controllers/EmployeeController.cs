namespace WebApplication1.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;
    using BusinessLayer;

    public class EmployeeController : Controller
    {
        // GET: Employee
        public ActionResult Index()
        {
            EmployeeBusinessLayer employeeBusinessLayer = new EmployeeBusinessLayer();
            List<Employee> employees = employeeBusinessLayer.Employees.ToList();


            return View(employees);
        }

        [HttpGet]
        [ActionName("Create")]
        public ActionResult Create_Get()
        {
            return View();
        }

        [HttpGet]
        [ActionName("Edit")]
        public ActionResult Edit_Get(int Id)
        {
            EmployeeBusinessLayer employeeBusinessLayer = new EmployeeBusinessLayer();
            var employee = employeeBusinessLayer.Employees.Single(emp => emp.Id == Id);

            return View(employee);
        }

        [HttpPost]
        [ActionName("Edit")]
        public ActionResult Edit_Post([Bind(Include = "Id, Gender, City, DateOfBirth")]Employee employee)
        {
            EmployeeBusinessLayer employeeBusinessLayer = new EmployeeBusinessLayer();
            employee.Name =employeeBusinessLayer.Employees.Single(i => i.Id == employee.Id).Name;
           
            if (ModelState.IsValid)
            {
                employeeBusinessLayer.SaveEmployee(employee);
                return RedirectToAction("Index");

            };

            return View(employee);
        }

        


        [HttpPost]
        [ActionName("Create")]
        public ActionResult Create_Post()
        {
            Employee employee = new Employee();
            TryUpdateModel(employee);

            if (ModelState.IsValid)
            {

                EmployeeBusinessLayer employeeBusinessLayer = new EmployeeBusinessLayer();
                employeeBusinessLayer.AddEmployee(employee);
                return RedirectToAction("Index");
            }

            return View();
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            EmployeeBusinessLayer employeeBusinessLayer = new EmployeeBusinessLayer();
            employeeBusinessLayer.DeleteEmployee(id);

            return RedirectToAction("Index");
        }


    }
}