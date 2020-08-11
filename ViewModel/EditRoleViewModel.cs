using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Idintitycorepro.ViewModel
{

    public class EditRoleViewModel
    {
        public EditRoleViewModel()
        {
            User = new List<string>();
        }
        public string Id { get; set; }
        [Required]
        public string Name { get; set; }
        public List<string> User { get; set; }
    }
}
