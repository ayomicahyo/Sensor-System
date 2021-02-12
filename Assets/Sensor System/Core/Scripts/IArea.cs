using System.Collections.Generic;
using UnityEngine;

namespace SensorSystem
{
    public interface IArea
    {
        void Scanning(List<ITarget> Targets, Transform obj, ISensor sensor);
        void DrawArea(Transform transform);
    }
}