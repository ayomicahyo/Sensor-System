using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SensorSystem.Default
{
    public class Sensor : MonoBehaviour, ISensor
    {

        #region Variable

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
        #endregion

        public List<ITarget> Targets
        {
            get
            {
                /*filSuccess.Clear(); GABISA PAKE gini
                Debug.Log("Added");
                foreach (var target in Targets)
                {
                    foreach (var filter in Filters)
                    {

                        if (filter.IsTargetTrue(target))
                        {
                            Debug.Log("FILTER SUCCESS");
                            filSuccess.Add(target.ID);

                        }

                    }
                }*/

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



        private void Update()
        {
            movetarget = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
            Move(movetarget);
            Area.Scanning(Targets , transform , this);
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

        private void Move(Vector2 movetarget)
        {
            transform.Translate(movetarget*Time.deltaTime*speed);
        }

        public void Filtering()
        {
            
            foreach (var target in Targets)
            {
                foreach (var filter in Filters)
                {

                    if (filter.IsTargetTrue(target))
                    {
                        FilSucces.Add(target.ID);
                        break;

                    }

                }
            }
        }
    }
}
