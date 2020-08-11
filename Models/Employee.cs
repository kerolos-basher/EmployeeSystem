using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Idintitycorepro.Models
{
    public class Employee
    {
        public int ID { get; set; }
        [Required]
        public string Name { get; set; }
        public string Email { get; set; }
        [Required]
        public Dpt Department { get; set; }
        public string photourl { get; set; }
    }
}
