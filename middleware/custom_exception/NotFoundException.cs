namespace Pokemon.middleware.custom_exception
{
    public class NotFoundException : ApiException
    {
        public NotFoundException(string message) : base(404, message)
        {
        }
    }
}