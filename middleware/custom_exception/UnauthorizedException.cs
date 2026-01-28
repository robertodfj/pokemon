namespace Pokemon.middleware.custom_exception
{
    public class UnauthorizedException : ApiException
    {
        public UnauthorizedException(string message) : base(401, message)
        {
        }
    }
}