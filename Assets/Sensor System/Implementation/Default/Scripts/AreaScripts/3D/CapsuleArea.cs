using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace SensorSystem.Default
{
    [CreateAssetMenu(menuName = "Sensor System/Default/Area/Capsule Area", fileName = "Capsule Area")]
    public class CapsuleArea : Area
    {
        [SerializeField]
        private float radius = 1.0f;
        [SerializeField]
        private float high = 0f;
        [SerializeField]
        private Color radiusColor = Color.blue;

        [SerializeField]
        private int lines = 4;

        public Vector3 TopPosititon(Transform trans)
        {
            return new Vector3(trans.position.x , trans.position.y + high / 2 , trans.position.z); 
        }

        public Vector3 BottomPosititon(Transform trans)
        {
            return new Vector3(trans.position.x, trans.position.y - high / 2, trans.position.z);
        }

        public override void DrawArea(Transform transform)
        {
            Vector3 viewAngleA = DirFromAngle(-viewAngle / 2, false, transform, true);
            Vector3 viewAngleB = DirFromAngle(viewAngle / 2, false, transform, true);

            Gizmos.color = radiusColor;
            Gizmos.DrawLine(transform.position, transform.position + viewAngleA * radius);
            Gizmos.DrawLine(transform.position, transform.position + viewAngleB * radius);

            Handles.color = radiusColor;
            Handles.DrawWireDisc(TopPosititon(transform), transform.up, radius + 0.1f);
            Handles.DrawWireDisc(BottomPosititon(transform), transform.up, radius + 0.1f);

            float stepAngleSize = 360 / lines;

            for (int i = 0; i <= lines; i++)
            {
                float angle = transform.eulerAngles.y - viewAngle / 2 + stepAngleSize * i;
                Handles.DrawLine(TopPosititon(transform) + DirFromAngle(angle, true , transform , true) , BottomPosititon(transform) + DirFromAngle(angle, true, transform, true));
            }
        }

        public override void Scanning(List<ITarget> Targets, Transform obj, ISensor sensor)
        {
            DrawFieldOfView(obj, radius, true);

            targetInRadius.Clear();
            sensor.FilSucces.Clear();
            Targets.Clear();

            float stepAngleSize = 360 / lines;

            for (int i = 0; i <= lines; i++)
            {
                float angle = obj.eulerAngles.y - viewAngle / 2 + stepAngleSize * i;
                RaycastHit hit;
                bool hits = Physics.Linecast(TopPosititon(obj) + DirFromAngle(angle, true, obj, true), BottomPosititon(obj) + DirFromAngle(angle, true, obj, true), out hit);

                if (hits != false)
                {
                    Vector3 dirPlayer = (hit.collider.gameObject.GetComponent<Transform>().position - obj.position).normalized;

                    if (Vector3.Angle(obj.forward, dirPlayer) < viewAngle / 2)
                    {

                        if (hits == true)
                        {
                            Debug.Log(hit);
                            targetInRadius.Add(hit.collider.gameObject.GetComponent<Transform>());
                            Targets.Add(hit.collider.gameObject.GetComponent<ITarget>());
                            sensor.Filtering();
                        }
                    }
                }
            }

            base.Scanning(Targets, obj, sensor);
        }
    }

}