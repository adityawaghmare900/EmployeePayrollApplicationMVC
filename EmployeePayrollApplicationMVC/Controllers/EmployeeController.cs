

using ManagerLayer.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ModelLayer;
using System.Collections.Generic;
using System.Linq;

namespace EmployeePayrollApplicationMVC.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IEmpManager empManager;

        public EmployeeController(IEmpManager empManager)
        {
            this.empManager = empManager;
        }
        public IActionResult GetAllEmployee()
        {
            List<EmployeeModel> employees = new List<EmployeeModel>();
            employees=empManager.GetAllEmployee().ToList();
            return View(employees);
        }

        [HttpGet]
        public IActionResult AddEmployee()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddEmployee([Bind] EmployeeModel employee)
        {
            if(ModelState.IsValid)
            {
                empManager.AddEmployee(employee);
                return RedirectToAction("GetAllEmployee");
            }
            return View(employee);   
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            if (id == null)
            {
                return NotFound();
            }
            EmployeeModel employee = empManager.GetEmployeeById(id);

            if (employee == null)
            {
                return NotFound();
            }
            return View(employee);
        }

        [HttpPost]
       
        public IActionResult Edit(int id, [Bind] EmployeeModel employee)
        {
            if (id != employee.EmployeeId)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                empManager.UpdateEmployee(employee);
                return RedirectToAction("GetAllEmployee");
            }
            return View(employee);
        }


        [HttpGet]
        public IActionResult Delete(int id)
        {
            if (id == null)
            {
                return NotFound();
            }
            EmployeeModel employee = empManager.GetEmployeeById(id);

            if (employee == null)
            {
                return NotFound();
            }
            return View(employee);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            empManager.DeleteEmployee(id);
            return RedirectToAction("GetAllEmployee");
        }

        [HttpGet]
        public IActionResult Details(int id)
        {
            if (id == null)
            {
                return NotFound();
            }
            EmployeeModel employee = empManager.GetEmployeeById(id);

            if (employee == null)
            {
                return NotFound();
            }
            return View(employee);
        }

        [HttpGet]
        public IActionResult login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult login(EmpLogin login)
        {
            var emp=empManager.login(login);
            if (emp != null)
            {
                HttpContext.Session.SetInt32("EmployeeId", emp.EmployeeId);
                return RedirectToAction("GetAllEmployee");
            }
            return RedirectToAction("GetAllEmployee");
        }


        [HttpGet]
        public IActionResult CheckId(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            EmployeeModel employee = empManager.GetEmployeeById(id);
            if (ModelState.IsValid)
            {
                if (employee.EmployeeId == id)
                {

                    empManager.UpdateEmployee(employee);
                    return RedirectToAction("GetAllEmployee");
                }
                else
                {
                    empManager.AddEmployee(employee);
                    return RedirectToAction("GetAllEmployee");
                }
            }
            return View(employee);
        }

        [HttpGet]
        public IActionResult AddOrUpdateEmployee()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddOrUpdateEmployee(EmployeeModel employee)
        {
            EmployeeModel employeeModel = new EmployeeModel();
            if(ModelState.IsValid)
            {
                employeeModel = empManager.AddOrUpdateEmployee(employee);
                return RedirectToAction("GetAllEmployee");
            }
            return View(employeeModel);
        }


    }
}
