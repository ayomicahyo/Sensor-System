using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace SensorSystem.Default
{
    public class Sensor3D : MonoBehaviour, ISensor
    {

        #region Variable

        private UnityEvent<targetData> callback = new UnityEvent<targetData>();

        public targetData tarData = default;

        [SerializeField]
        private ScriptableObject[] filtersData = new ScriptableObject[0];
        [SerializeField]
        private ScriptableObject areaData = null;

        public List<ITarget> targets = new List<ITarget>();
        private IArea area = null;
        private List<IFilter> filters = new List<IFilter>();

        [SerializeField]
        private List<string> filSuccess = new List<string>();

        public Vector2 movetarget = default;
        public float speed = 8;

        public string nameOfPlayer;
        public float rotationSpeed = 5;
        public Rigidbody rb = default;
        private float dirX = 0;
        private float dirZ = 0;
        private Vector3 checkMove = default;
        private Vector3 direction = Vector3.zero;
        #endregion

        public List<ITarget> Targets
        {
            get
            {
                return targets;
            }
            set { }
        }


        public IArea Area
        {
            get
            {
                if(area == null) 
                {
                    area = (IArea)areaData;
                }

                return area;
            }
        }

        public List<IFilter> Filters
        {
            get
            {
                if(filters.Count == 0)
                {
                    foreach (var filter in filtersData)
                    {
                        filters.Add((IFilter)filter);
                    }
                }

                return filters;
            }
        }

        public List<string> FilSucces
        {
            get 
            {
                return filSuccess; 
            }
        }

        public targetData TargetData 
        {
            get 
            {
                return tarData;
            }
        }

        private void Update()
        {

            movetarget = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
            //Move(movetarget);
            Area.Scanning(Targets, transform, this);

            dirX = Input.GetAxis("Horizontal");
            dirZ = Input.GetAxis("Vertical");
            checkMove = new Vector3(dirX, 0, dirZ);

            if (checkMove != Vector3.zero)
                RotationDirect();
        }

        private void FixedUpdate()
        {

            PlayerMove();

        }

        private void RotationDirect()
        {

            direction = new Vector3(dirX, 0f, dirZ);

            if (checkMove != Vector3.zero)
            {

                transform.rotation = Quaternion.Slerp(a: transform.rotation, b: Quaternion.LookRotation(direction), t: rotationSpeed * Time.deltaTime);
            }
        }

        private void PlayerMove()
        {
            if (checkMove != Vector3.zero)
            {
                rb.velocity = new Vector3(dirX * speed * Time.deltaTime, rb.velocity.y, dirZ * speed * Time.deltaTime);
            }
            else
            {
                rb.velocity = new Vector3(0, rb.velocity.y, 0);
            }

        }

        private void OnDrawGizmos()
        {
            if(areaData == null)
            {
                return;
            }

            area = (IArea)areaData;

            area.DrawArea(transform);
        }

        /*private void Move(Vector2 movetarget)
        {
            transform.Translate(movetarget*Time.deltaTime*speed);
        }*/

        public void Filtering()
        {
            FilSucces.Clear();
            foreach (var target in Targets)
            {
                foreach (var filter in Filters)
                {

                    if (filter.IsTargetTrue(target))
                    {
                        FilSucces.Add(target.ID);
                        Callback(target);
                        break;

                    }

                }
            }
        }

        public void Callback(ITarget target)
        {
            callback.AddListener(target.DataSend);

            if (callback != null)
            {
                callback.Invoke(target.Data);

            }

            callback.RemoveListener(target.DataSend);
        }
    }
}
