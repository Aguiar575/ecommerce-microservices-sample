namespace Snowflake.Factory.Provider;

public interface IDateTimeProvider {
    DateTime GetUtcNow();
    DateTime GetToday();
}