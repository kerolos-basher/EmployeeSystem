using Idintitycorepro.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Idintitycorepro.Models
{
    public class MokeReposatoryEmployee : IReposatoryEmployee
    {
        private ApplicationDbContext _context;
        public MokeReposatoryEmployee(ApplicationDbContext context)
        {
            _context = context;
        }
        public IEnumerable<Employee> GetAllEmployees()
        {
           return _context.Employees.ToList();
        }

        public Employee GetEmployee(int id)
        {
            return _context.Employees.FirstOrDefault(e => e.ID == id);
        }
        public bool CrreatNew(Employee e) 
        {
           
            if (e != null )
            {
                _context.Add(e);
                _context.SaveChanges();
                return true;
            }
            return false;
        }

        public bool UPdateEmployee(Employee emp)
        {
            if (emp != null)
            {
                var empfdb = _context.Employees.FirstOrDefault(e => e.ID == emp.ID);
                empfdb.Name = emp.Name;
                empfdb.Email = emp.Email;
                empfdb.Department = emp.Department;
                empfdb.photourl = emp.photourl;
                _context.SaveChanges();
                return true;
            }
            return false;
        }

        public bool DeletEmployee(int? id)
        {
            if (id != null)
            {
                var empfdb = _context.Employees.FirstOrDefault(e => e.ID ==id);
                _context.Remove(empfdb);
                _context.SaveChanges();
                return true;
            }
            return false;
        }
    }
}
