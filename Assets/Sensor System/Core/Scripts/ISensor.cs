using System.Collections.Generic;

namespace SensorSystem
{
    public interface ISensor
    {
        List<ITarget> Targets
        {
            get;
            set;
        }

        IArea Area
        {
            get;
        }

        List<string> FilSucces { get; }

        List<IFilter> Filters
        {
            get;
        }

        void Filtering();
    }
}