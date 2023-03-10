using Moq;
using Snowflake.Factory.Model;
using Snowflake.Factory.Provider;

namespace SnowFlakeFactory.Tests.SnowFlakeModelTests;

public class SnowFlakeModelTests
{
    Mock<IDateTimeProvider> _dateTimeProvider;

    public SnowFlakeModelTests() =>
        _dateTimeProvider = new Mock<IDateTimeProvider>();

    [Fact]
    public void Should_Throw_ArgumentOutOfRangeException_If_Epoch_Is_In_Future()
    {
        DateTime getToday = new DateTime(2012, 12, 20, 21, 15, 30, 420);
        DateTime epoch = new DateTime(2012, 12, 22, 20, 30, 30, 663);
        _dateTimeProvider.Setup(e => e.GetToday()).Returns(getToday);

        Assert.Throws<ArgumentOutOfRangeException>(() => 
            new SnowflakeModel(_dateTimeProvider.Object){
                Epoch = epoch,
                DatacenterIdBits = 1,
                WorkerIdBits = 1
            });
    }

    [Fact]
    public void Should_Throw_ArgumentOutOfRangeException_If_DatacenterIdBits_Is_Less_Than_zero()
    {
        DateTime getToday = new DateTime(2012, 12, 20, 21, 15, 30, 420);
        DateTime epoch = new DateTime(2012, 12, 20, 20, 30, 30, 663);
        _dateTimeProvider.Setup(e => e.GetToday()).Returns(getToday);

        Assert.Throws<ArgumentOutOfRangeException>(() => 
            new SnowflakeModel(_dateTimeProvider.Object){
                Epoch = epoch,
                DatacenterIdBits = 0,
                WorkerIdBits = 1
            });
    }

    [Fact]
    public void Should_Throw_ArgumentOutOfRangeException_If_WorkerIdBits_Is_Less_Than_zero()
    {
        DateTime getToday = new DateTime(2012, 12, 20, 21, 15, 30, 420);
        DateTime epoch = new DateTime(2012, 12, 20, 20, 30, 30, 663);
        _dateTimeProvider.Setup(e => e.GetToday()).Returns(getToday);

        Assert.Throws<ArgumentOutOfRangeException>(() => 
            new SnowflakeModel(_dateTimeProvider.Object){
                Epoch = epoch,
                DatacenterIdBits = 1,
                WorkerIdBits = 0
            });
    }
}