namespace InnowiseProject.WebApi.DTO
{
    public class WorkerDetailsDTO
    {
        public string Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }
        public IEnumerable<DepartmentDTO> Departments { get; set; }
    }
}
