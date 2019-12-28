using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace ShooterFeatures
{
    public class SimpleShooter: ActorController
    {

        void Awake()
        {
            name = transform.gameObject.name;
        }

        private void Update()
        {
            //Shoot();
        }
    }
}
