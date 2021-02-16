using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace SensorSystem.Default
{
    [CreateAssetMenu(menuName = "Sensor System/Default/Area/Circle 2D Area", fileName = "Circle 2D Area")]
    public class Triangle2DArea : Area
    {
        [SerializeField]
        private float size = 0;

        [SerializeField]
        private float radius = 1.0f;
        [SerializeField]
        private Color radiusColor = Color.blue;

        private float angleTemp = 0;

        public override void DrawArea(Transform transform)
        {

            Vector3 viewAngleA = default;
            Vector3 viewAngleB = default;

            Gizmos.color = radiusColor;

            if (transform.GetComponent<Sensor2D>().lastdirection == 1)
            {
                viewAngleA = DirFromAngle(-(viewAngle / 2) + 90, false, transform, false);
                viewAngleB = DirFromAngle((viewAngle / 2) + 90, false, transform, false);
            }
            else if (transform.GetComponent<Sensor2D>().lastdirection == -1)
            {
                viewAngleA = DirFromAngle(-(viewAngle / 2) - 90, false, transform, false);
                viewAngleB = DirFromAngle((viewAngle / 2) - 90, false, transform, false);
            }


            Gizmos.DrawLine(transform.position, transform.position + viewAngleA * (radius + 0.2f));
            Gizmos.DrawLine(transform.position, transform.position + viewAngleB * (radius + 0.2f));

            Handles.color = radiusColor;
            Gizmos.DrawLine(new Vector3(transform.position.x + size / 2, transform.position.y, transform.position.z),
                new Vector3(transform.position.x - size / 2, transform.position.y, transform.position.z));

            Gizmos.DrawLine(new Vector3(transform.position.x + size / 2, transform.position.y, transform.position.z),
                 new Vector3(transform.position.x, transform.position.y + size / 2, transform.position.z));

            Gizmos.DrawLine(new Vector3(transform.position.x - size / 2, transform.position.y, transform.position.z),
                 new Vector3(transform.position.x, transform.position.y + size / 2, transform.position.z));
        }

        public override void Scanning(List<ITarget> Targets, Transform obj, ISensor sensor)
        {


            DrawFieldOfView(obj, radius, false);

            targetInRadius.Clear();
            sensor.FilSucces.Clear();
            Targets.Clear();

            /*
            Collider2D[] coll = Physics2D.OverlapCircleAll(obj.position, radius);

            if (coll != null)
            {

                for (int i = 0; i < coll.Length; i++)
                {
                    Transform other = coll[i].transform;
                    Vector2 dirPlayer = (other.position - obj.position).normalized;

                    if (obj.GetComponent<Sensor2D>().lastdirection == 1)
                    {
                        angleTemp = viewAngle;

                        if (Vector2.Angle(obj.position, dirPlayer) < angleTemp / 2)
                        {

                            float distancePlayer = Vector2.Distance(obj.position, other.position);

                            if (!Physics2D.Raycast(obj.position, dirPlayer, distancePlayer, obstacleMask))
                            {
                                targetInRadius.Add(other.GetComponent<Transform>());
                                Targets.Add(other.GetComponent<ITarget>());
                                sensor.Filtering();

                            }
                        }
                    }
                    else
                    {
                        angleTemp = viewAngle + 180;

                        if (Vector2.Angle(obj.position, dirPlayer) > angleTemp / 2)
                        {

                            float distancePlayer = Vector2.Distance(obj.position, other.position);

                            if (!Physics2D.Raycast(obj.position, dirPlayer, distancePlayer, obstacleMask))
                            {
                                targetInRadius.Add(other.GetComponent<Transform>());
                                Targets.Add(other.GetComponent<ITarget>());
                                sensor.Filtering();

                            }
                        }
                    }



                }
            }
            */

            base.Scanning(Targets, obj, sensor);
        }
    }

}