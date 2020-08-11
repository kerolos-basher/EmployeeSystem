using Idintitycorepro.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Idintitycorepro.ViewModel
{
    public class EmployeeViewModel
    {
      
        [Required]
        public string Name { get; set; }
        public string Email { get; set; }
        [Required]
        public Dpt Department { get; set; }
        public IFormFile photo { get; set; }
    }
}
