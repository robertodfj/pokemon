namespace Pokemon.middleware
{
    public class NotFoundException : ApiException
    {
        public NotFoundException(string message) : base(404, message)
        {
        }
    }
}