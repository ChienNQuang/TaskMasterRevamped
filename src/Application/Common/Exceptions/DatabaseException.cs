namespace Application.Common.Exceptions;

public class DatabaseException : Exception
{
    public DatabaseException() : base("Operation cannot be done by the database")
    {
    }
}