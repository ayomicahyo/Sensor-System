using UnityEngine;

namespace SensorSystem.Default
{
    public class Filter : ScriptableObject, IFilter
    {
        public virtual bool IsTargetTrue(ITarget target)
        {
            return false;
        }
    }

}