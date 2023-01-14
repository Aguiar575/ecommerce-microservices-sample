namespace Snowflake.Factory.Provider;

public class DateTimeProvider : IDateTimeProvider 
{
    public DateTime GetUtcNow() =>
        DateTime.UtcNow;

    public DateTime GetToday() => 
        DateTime.Today;
}