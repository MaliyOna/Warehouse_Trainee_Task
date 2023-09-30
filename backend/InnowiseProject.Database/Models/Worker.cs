using Microsoft.AspNetCore.Identity;

namespace InnowiseProject.Database.Models
{
    public class Worker : IdentityUser
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public ICollection<Department> Departments { get; set; }
    }
}
