using System.Collections.Generic;
using UnityEngine;

namespace SensorSystem.Default
{
    public class Area : ScriptableObject, IArea
    {


        public virtual void DrawArea(Transform transform)
        {
            
        }

        public virtual void Scanning(List<ITarget> Targets, Transform obj , ISensor sensor)
        {
            
        }
    }
}