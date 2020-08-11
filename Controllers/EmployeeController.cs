using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Idintitycorepro.Models;
using Idintitycorepro.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace Idintitycorepro.Controllers
{
    public class EmployeeController : Controller
    {
        private IReposatoryEmployee _iReposatoryEmployee;
        private IHostingEnvironment _hostingEnvironment;

        public EmployeeController(IReposatoryEmployee iReposatoryEmployee,IHostingEnvironment hostingEnvironment)
        {
            _iReposatoryEmployee = iReposatoryEmployee;
            _hostingEnvironment = hostingEnvironment;
        }
       
       
        public IActionResult Index()
        {
            return View();
        }
        public  IActionResult AllEmployees()
        {
            var Employees =  _iReposatoryEmployee.GetAllEmployees();
            return View(Employees);
        }
       
        public IActionResult Employeedetail(int id)
        {
            var Employee = _iReposatoryEmployee.GetEmployee(id);
            if (Employee == null)
            {
                Response.StatusCode = 404;
                return View("ErrorView", id);
            }
            return View(Employee);
        }
        [HttpGet]
        [Authorize]
        public IActionResult CreateNew()
        {
            return View();
        }
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public IActionResult CreateNew(EmployeeViewModel mode)
        {
            string uniqname = null;
            if (ModelState.IsValid)
            {
                if (mode.photo != null)
                {
                    var uploadFilepath = Path.Combine(_hostingEnvironment.WebRootPath, "images");
                    uniqname = Guid.NewGuid().ToString() + "_" + mode.photo.FileName;
                    string filePath = Path.Combine(uploadFilepath, uniqname);
                    mode.photo.CopyTo(new FileStream(filePath, FileMode.Create));
                }
                Employee Emp = new Employee {
                Name = mode.Name,
                Email = mode.Email,
                Department = mode.Department,
                photourl = uniqname
                };

                var r = _iReposatoryEmployee.CrreatNew(Emp);
                if (r)
                {
                    return RedirectToAction("AllEmployees");
                }
            }
            return View(mode);
        }
        [HttpGet]
       
        [Authorize(Roles = "Admin")]
        public IActionResult EditEmployee(int id)
        {
            var Employee = _iReposatoryEmployee.GetEmployee(id);
            EditEmployeeViewModel employeeViewModel = new EditEmployeeViewModel()
            { 
                Id = Employee.ID,
                Name = Employee.Name,
                Email = Employee.Email,
                Department= Employee.Department,
                ExistingPhotoPath = Employee.photourl
            };
            return View(employeeViewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public IActionResult EditEmployee(EditEmployeeViewModel EditEmp)
        {
            string uniqname = null;
            if (ModelState.IsValid)
            {
                if (EditEmp.photo != null)
                {
                    var uploadFilepath = Path.Combine(_hostingEnvironment.WebRootPath, "images");
                    uniqname = Guid.NewGuid().ToString() + "_" + EditEmp.photo.FileName;
                    string filePath = Path.Combine(uploadFilepath, uniqname);
                    EditEmp.photo.CopyTo(new FileStream(filePath, FileMode.Create));
                }
                Employee Emp = new Employee
                {
                    ID = EditEmp.Id,
                    Name = EditEmp.Name,
                    Email = EditEmp.Email,
                    Department = EditEmp.Department,
                    photourl = uniqname
                };


                var r = _iReposatoryEmployee.UPdateEmployee(Emp);
                if (r)
                {
                    return RedirectToAction("AllEmployees");
                }
            }
            return View(EditEmp);
        }
        [Authorize]
        public IActionResult DeleteEmployee(int? id)
        {
            if (id != null)
            {
                var r = _iReposatoryEmployee.DeletEmployee(id);
                if (r)
                {
                    return RedirectToAction("AllEmployees");
                }
            }
            return RedirectToAction("AllEmployees");
        }
       
    }
}
