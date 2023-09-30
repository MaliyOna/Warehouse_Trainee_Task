namespace InnowiseProject.WebApi.Exceptions
{
    public class BadRequestException : Exception
    {
        public BadRequestException(string message)
            : base(message)
        {

        }
    }
}
