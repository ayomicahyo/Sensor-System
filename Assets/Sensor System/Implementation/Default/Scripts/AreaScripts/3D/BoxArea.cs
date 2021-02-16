using System.Collections.Generic;
using UnityEngine;

namespace SensorSystem.Default
{
    [CreateAssetMenu(menuName = "Sensor System/Default/Area/Box Area", fileName = "Box Area")]
    public class BoxArea : Area
    {
        [SerializeField]
        private Vector3 size = Vector3.one;
        [SerializeField]
        private Color sizeColor = Color.green;

        public override void DrawArea(Transform transform)
        {
            Gizmos.color = sizeColor;

            Vector3 viewAngleA = DirFromAngle(-viewAngle / 2, false, transform , true);
            Vector3 viewAngleB = DirFromAngle(viewAngle / 2, false, transform , true);

            Gizmos.DrawLine(transform.position, transform.position + viewAngleA * size.x);
            Gizmos.DrawLine(transform.position, transform.position + viewAngleB * size.x);

            Gizmos.DrawWireCube(transform.position, size);
        }

        public override void Scanning(List<ITarget> Targets, Transform obj , ISensor sensor)
        {
            DrawFieldOfView(obj , size.x , true);
            targetInRadius.Clear();
            sensor.FilSucces.Clear();
            Targets.Clear();

            Collider[] coll = Physics.OverlapBox(obj.position , size/2);

            if (coll != null)
            {

                for (int i = 0; i < coll.Length; i++)
                {
                    Transform other = coll[i].transform;
                    Vector3 dirPlayer = (other.position - obj.position).normalized;

                    if (Vector3.Angle(obj.forward, dirPlayer) < viewAngle / 2)
                    {
                        float distancePlayer = Vector3.Distance(obj.position, other.position);

                        if (!Physics.Raycast(obj.position, size, distancePlayer, obstacleMask))
                        {
                            targetInRadius.Add(other.GetComponent<Transform>());
                            Targets.Add(other.GetComponent<ITarget>());
                            sensor.Filtering();
                        }
                    }

                }
            }

            base.Scanning(Targets, obj , sensor);
        }
    }
}
