using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Common.Templates
{
    //[CreateAssetMenu(menuName = "DemoData", order = 1, fileName = "Template")]
    public class ScriptableTemplate : ScriptableObject
    {
        [SerializeField] float defaultHealth = 100;
        [SerializeField] float defaultMagic = 100;
        [HideInInspector] public GameObject myObject;
        [HideInInspector] public float health;
        [HideInInspector] public float magic;
        [HideInInspector] public Vector3 currentPosition;

        public void ResetData()
        {
            myObject = null;
            health = defaultHealth;
            magic = defaultMagic;
            currentPosition = Vector3.zero;
        }
    }
}
