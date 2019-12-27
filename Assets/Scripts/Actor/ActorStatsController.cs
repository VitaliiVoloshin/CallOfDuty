using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace ShooterFeatures
{
    public class ActorStatsController: MonoBehaviour
    {
        [SerializeField] private ActorStats stats;
        public float movementSpeed;
        public float attackSpeed;
        public float damageCaused;
        public float damageTaken;
        public float health;
        public float maxHealth;

        private void Awake()
        {
            
            health = stats.health;
            maxHealth = health;
            attackSpeed = stats.attackSpeed;
            movementSpeed = stats.movementSpeed;
            damageCaused = stats.damageCaused;
            damageTaken = stats.damageTaken;
        }
    }
}
