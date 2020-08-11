using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Idintitycorepro.ViewModel
{
    public class EditUesrViewModel
    {
        public EditUesrViewModel()
        {
            Cliams = new List<string>();
            Roles = new List<string>();
        }
        public string Id { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required][EmailAddress]
        public string Emil { get; set; }
        public string  City { get; set; }
        public List<string> Cliams { get; set; }
        public List<string> Roles { get; set; }



    }
}
