namespace InnowiseProject.WebApi.DTO
{
    public class DepartmentDetailsDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<ProductDTO> Products { get; set; }
        public IEnumerable<WorkerDetailsDTO> Workers { get; set; }
    }
}
