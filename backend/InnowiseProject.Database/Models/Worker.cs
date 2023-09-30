using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace InnowiseProject.Database.Models
{
    public class Worker : IdentityUser
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public bool IsSystem { get; set; }

        public ICollection<Department> Departments { get; set; }
    }
}
