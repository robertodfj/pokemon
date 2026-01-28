namespace Pokemon.middleware.custom_exception
{
    public class ConflictException : ApiException
    {
        public ConflictException(string message) : base(409, message)
        {
        }
    }
}