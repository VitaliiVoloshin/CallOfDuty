using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShooterFeatures
{
    public class UnitHolder: MonoBehaviour
    {
        public List<GameObject> units = new List<GameObject>();
        public static UnitHolder instance;

        void Awake()
        {
            instance = this;
        }
    }
}