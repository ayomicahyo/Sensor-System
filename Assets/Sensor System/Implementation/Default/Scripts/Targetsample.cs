using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SensorSystem.Default
{
    public class Targetsample : MonoBehaviour, ITarget
    {
        public float health = 100;

        [SerializeField]
        private string id = string.Empty;

        [SerializeField]
        private string tag = string.Empty;

        public string ID
        {
            get 
            {
                return id;
            }
            set { }
        }

        public string Tag
        {
            get
            {
                return tag;
            }
            set { }
        }

        // Update is called once per frame
        void Update()
        {

        }
    }

}
