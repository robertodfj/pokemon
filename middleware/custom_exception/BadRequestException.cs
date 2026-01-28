namespace Pokemon.middleware.custom_exception
{
    public class BadRequestException : ApiException
    {
        public BadRequestException(string message) : base(400, message)
        {
        }
    }
}