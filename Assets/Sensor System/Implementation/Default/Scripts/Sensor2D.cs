using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace SensorSystem.Default
{
    public class Sensor2D : MonoBehaviour, ISensor
    {

        #region Variable

        /// <summary>
        /// For delegate event with parameter targetData Struct
        /// </summary>
        public UnityEvent<targetData> callback;

        /// <summary>
        /// For Accumulate filterData
        /// </summary>
        [SerializeField]
        private ScriptableObject[] filtersData = new ScriptableObject[0];

        /// <summary>
        /// For Accumulate area
        /// </summary>
        [SerializeField]
        private ScriptableObject areaData = null;


        public List<ITarget> targets = new List<ITarget>();
        private IArea area = null;
        private List<IFilter> filters = new List<IFilter>();

        [SerializeField]
        private List<string> filSuccess = new List<string>();

        /// <summary>
        /// Just for get direction Movement
        /// </summary>
        public float lastdirection = 1;

        /// <summary>
        /// Vector Movement
        /// </summary>
        public Vector2 movetarget = default;

        /// <summary>
        /// Speed
        /// </summary>
        public float speed = 8;

        /// <summary>
        /// Name Of Object
        /// </summary>
        public string nameOfPlayer;

        /// <summary>
        /// Just RigidBody2D
        /// </summary>
        public Rigidbody2D rb = default;

        #endregion

        #region Property

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
                if (area == null)
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
                if (filters.Count == 0)
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

        #endregion

        #region Mono

        private void Update()
        {

            movetarget = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
            Area.Scanning(Targets, transform, this);
        }

        private void FixedUpdate()
        {

            PlayerMove();

        }

        private void OnDrawGizmos()
        {
            if (areaData == null)
            {
                return;
            }

            area = (IArea)areaData;

            area.DrawArea(transform);
        }

        #endregion

        #region Main

        private void PlayerMove()
        {
            if (movetarget != Vector2.zero)
            {
                if(movetarget.x > 0.1)
                {
                    lastdirection = 1;
                }
                else if (movetarget.x < -0.1)
                {
                    lastdirection = -1;
                }

                rb.velocity = new Vector2(movetarget.x * speed * Time.deltaTime, movetarget.y * speed * Time.deltaTime);
            }
            else
            {
                rb.velocity = new Vector2(0, rb.velocity.y);
            }

        }

        #endregion

        #region Sensor

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

            if ( callback != null)
            {
                callback.Invoke(target.Data);

            }

            callback.RemoveListener(target.DataSend);
        }

        #endregion

    }
}

