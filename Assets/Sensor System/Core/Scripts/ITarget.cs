namespace SensorSystem
{
    public interface ITarget
    {

        targetData Data
        {
            get;
        }

        string ID
        {
            get;
        }

        string Tag
        {
            get;
        }

        void DataSend(targetData data);
    }
}
