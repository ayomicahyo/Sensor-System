using UnityEngine;

namespace SensorSystem.Default
{
    [CreateAssetMenu(menuName = "Sensor System/Default/Filter/Health Filter", fileName = "Health Filter")]
    public class HealthFilter : Filter
    {
        [SerializeField]
        private float minimumHealth = 50.0f;
        [SerializeField]
        private float maximumHealth = 100.0f;

        public override bool IsTargetTrue(ITarget target)
        {
            Targetsample health = (Targetsample)target;
            
            if (health.health <= maximumHealth && health.health >= minimumHealth) 
            {
                return true;
            }
            return false;
        }
    }
}