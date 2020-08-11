using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Idintitycorepro.Models
{
    public interface IReposatoryEmployee
    {
        Employee GetEmployee(int id);
        IEnumerable<Employee> GetAllEmployees();
        bool CrreatNew(Employee e);
        bool UPdateEmployee(Employee em);
        bool DeletEmployee(int? id);

    }
}
