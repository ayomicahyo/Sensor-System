namespace SensorSystem
{
    public interface IFilter
    {
        bool IsTargetTrue(ITarget target);
    }
}
