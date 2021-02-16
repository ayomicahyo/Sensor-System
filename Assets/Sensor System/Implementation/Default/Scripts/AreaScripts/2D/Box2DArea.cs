using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace SensorSystem.Default
{
    [CreateAssetMenu(menuName = "Sensor System/Default/Area/Box 2D Area", fileName = "Box 2D Area")]
    public class Box2DArea : Area
    {
        [SerializeField]
        private Vector2 size = Vector2.one;
        [SerializeField]
        private Color sizeColor = Color.green;

        private float angleTemp = 0;

        public override void DrawArea(Transform transform)
        {

            Vector3 viewAngleA = default;
            Vector3 viewAngleB = default;

            Gizmos.color = sizeColor;

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


            Gizmos.DrawLine(transform.position, transform.position + viewAngleA * (size.x + 0.2f));
            Gizmos.DrawLine(transform.position, transform.position + viewAngleB * (size.x + 0.2f));

            Gizmos.DrawWireCube(transform.position, size);
        }

        public override void Scanning(List<ITarget> Targets, Transform obj, ISensor sensor)
        {


            DrawFieldOfView(obj, size.x, false);

            targetInRadius.Clear();
            sensor.FilSucces.Clear();
            Targets.Clear();

            Collider2D[] coll = Physics2D.OverlapBoxAll(obj.position, size / 2 , angleTemp);

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

            base.Scanning(Targets, obj, sensor);
        }
    }

}