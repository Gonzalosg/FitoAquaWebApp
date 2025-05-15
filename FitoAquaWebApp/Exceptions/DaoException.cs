namespace FitoAquaWebApp.Exceptions
{
    public class DaoException : Exception
    {
        public DaoException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        public DaoException(string message)
           : base(message)
        {
        }
    }
}
