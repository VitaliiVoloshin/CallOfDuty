using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace ShooterFeatures
{
    public class ActorStatsController: MonoBehaviour
    {
        [SerializeField] private ActorStats stats;
        [HideInInspector] public float movementSpeed;
        [HideInInspector] public float attackSpeed;
        [HideInInspector] public float damageCaused;
        [HideInInspector] public float damageTaken;
        [HideInInspector] public float health;
        [HideInInspector] public float maxHealth;

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
