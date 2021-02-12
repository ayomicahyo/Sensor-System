using UnityEngine;

namespace SensorSystem.Default
{
    public class Health : MonoBehaviour, ITarget
    {
        public float HP = 100.0f;

        public string ID => name;

        public string Tag => tag;
    }
}