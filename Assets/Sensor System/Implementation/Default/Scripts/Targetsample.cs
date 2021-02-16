using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SensorSystem.Default
{
    public class Targetsample : MonoBehaviour, ITarget
    {
        public float health = 100;
        public string nameTarget = string.Empty;
        public float baseDamage = 100;

        public targetData data = default;

        [SerializeField]
        private string id = string.Empty;

        [SerializeField]
        private new string tag = string.Empty;

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

        public targetData Data
        {
            get 
            {
                return data;
            }
        }

        void Awake()
        {
            data.name = nameTarget;
            data.baseDamage = baseDamage;
            data.tag = tag;
        }


        // Update is called once per frame
        void Update()
        {

        }

        public void DataSend(targetData data)
        {
            Debug.Log(data.name);
            Debug.Log(data.baseDamage);
            Debug.Log("DataPas");
        }


    }

}
