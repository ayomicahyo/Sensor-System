using System.Collections.Generic;
using UnityEngine;

namespace SensorSystem.Default
{
    [CreateAssetMenu(menuName = "Sensor System/Default/Area/Box Area", fileName = "Box Area")]
    public class BoxArea : Area
    {
        [SerializeField]
        private Vector2 size = Vector2.one;
        [SerializeField]
        private Color sizeColor = Color.green;

        public LayerMask obstacleMask = default;

        public List<string> targetInRadius = new List<string>();

        public override void DrawArea(Transform transform)
        {
            Gizmos.color = sizeColor;
            Gizmos.DrawWireCube(transform.position, size);
        }

        public override void Scanning(List<ITarget> Targets, Transform obj , ISensor sensor)
        {
            targetInRadius.Clear();
            sensor.FilSucces.Clear();
            Targets.Clear();

            Collider2D[] coll = Physics2D.OverlapBoxAll(obj.position , size , obj.eulerAngles.z);

            if (coll != null)
            {

                for (int i = 0; i < coll.Length; i++)
                {
                    Transform other = coll[i].transform;
                    Vector2 dirPlayer = new Vector2(other.position.x - obj.position.x, other.position.y - obj.position.y);

                    //if(Vector2.Angle(dirPlayer, selfGO.transform.right) < 0)
                    //{
                    float distancePlayer = Vector2.Distance(obj.position, other.position);

                    if (!Physics2D.Raycast(obj.position, dirPlayer, distancePlayer, obstacleMask))
                    {
                        targetInRadius.Add(other.GetComponent<ITarget>().ID);
                        Targets.Add(other.GetComponent<ITarget>());
                        sensor.Filtering();
                    }
                    //}

                }
            }

            base.Scanning(Targets, obj , sensor);
        }
    }
}
