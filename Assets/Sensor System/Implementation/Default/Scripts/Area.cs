using System.Collections.Generic;
using UnityEngine;

namespace SensorSystem.Default
{
    public class Area : ScriptableObject, IArea
    {

        public LayerMask obstacleMask = default;

        public float meshResolution;
        [Range(0, 360)]
        public float viewAngle;

        public List<Transform> targetInRadius = new List<Transform>();


        public virtual void DrawArea(Transform transform)
        {
            
        }

        public virtual void Scanning(List<ITarget> Targets, Transform obj , ISensor sensor)
        {
            
        }

        public void DrawFieldOfView(Transform obj , float size , bool is3D)
        {
            int stepCount = Mathf.RoundToInt(viewAngle * meshResolution);
            float stepAngleSize = viewAngle / stepCount;

            foreach (var tar in targetInRadius)
            {
                Debug.DrawLine(obj.position, tar.position);
            }

        }

        public Vector3 DirFromAngle(float angleInDegrees, bool angleIsGlobal, Transform obj , bool is3D)
        {
            if (!angleIsGlobal)
            {
                angleInDegrees += obj.eulerAngles.y;
            }
            if (is3D)
            {
                return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
            }
            else
            {
                return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), Mathf.Cos(angleInDegrees * Mathf.Deg2Rad), 0 );
            }
        }
    }
}