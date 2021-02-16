using System.Collections.Generic;
using UnityEngine;

namespace SensorSystem.Default
{
    [CreateAssetMenu(menuName = "Sensor System/Default/Area/Circle Area", fileName = "Circle Area")]
    public class CircleArea : Area
    {
        [SerializeField]
        private float radius = 1.0f;
        [SerializeField]
        private Color radiusColor = Color.blue;

        public override void DrawArea(Transform transform)
        {
            Vector3 viewAngleA = DirFromAngle(-viewAngle / 2, false , transform , true);
            Vector3 viewAngleB = DirFromAngle(viewAngle / 2, false , transform , true);

            Gizmos.color = radiusColor;
            Gizmos.DrawLine(transform.position, transform.position + viewAngleA * radius);
            Gizmos.DrawLine(transform.position, transform.position + viewAngleB * radius);

            Gizmos.color = radiusColor;
            Gizmos.DrawWireSphere(transform.position, radius);
        }

        public override void Scanning(List<ITarget> Targets , Transform obj , ISensor sensor)
        {
            DrawFieldOfView(obj , radius , true);

            targetInRadius.Clear();
            sensor.FilSucces.Clear();
            Targets.Clear();

            Collider[] coll = Physics.OverlapSphere(obj.position , radius);

            if(coll != null)
            {
               
                for(int i = 0 ; i < coll.Length; i++)
                {
                    Transform other = coll[i].transform;
                    Vector3 dirPlayer = (other.position - obj.position).normalized;

                    if (Vector3.Angle(obj.forward, dirPlayer) < viewAngle / 2)
                    {
                        float distancePlayer = Vector3.Distance(obj.position , other.position);

                        if (!Physics.Raycast(obj.position, dirPlayer, distancePlayer, obstacleMask))
                        {
                            targetInRadius.Add(other.GetComponent<Transform>());
                            Targets.Add(other.GetComponent<ITarget>());
                            sensor.Filtering();
                        
                        }
                    }
                    
                }
            }

            base.Scanning(Targets , obj , sensor);
        }
    }

}