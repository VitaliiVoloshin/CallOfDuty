using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace ShooterFeatures
{
    public class Spawner: MonoBehaviour
    {
        public void Spawn(Object unit)
        {
            GameObject spawn = Instantiate(unit, transform.position, Quaternion.identity) as GameObject;
            spawn.transform.parent = FindObjectOfType<UnitHolder>().gameObject.transform;
        }
    }
}