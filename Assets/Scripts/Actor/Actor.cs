using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShooterFeatures
{
    public enum Fraction
    {
        RedTeam,
        GreenTeam
    }

    [RequireComponent(typeof(ActorStatsController))]
    public abstract class ActorController: MonoBehaviour
    {
        public string nickname;
        public Fraction fraction;

        public ActorStatsController stats;
        protected WeaponController m_weaponController;

        private void Start()
        {
            m_weaponController = GetComponentInChildren<WeaponController>();
            stats = GetComponent<ActorStatsController>();
            AddToUnitHolder(UnitHolder.instance);
            nickname = GetRandomNickname();
        }


        private void OnDisable()
        {
            if (RespawnController.instance) {
                RespawnController.instance.RespawnRequest(gameObject);
            }
        }

        public void Death()
        {
            if (stats != null)
                if (stats.health <= 0) {
                    gameObject.SetActive(false);
                } else return;
        }

        protected void AddToUnitHolder(UnitHolder unitHolder)
        {
            unitHolder.units.Add(gameObject);
            transform.parent = unitHolder.transform;
        }

        public void TakeDamage(float damage)
        {
            stats.health -= damage * stats.damageTaken;
        }

        public void Shoot()
        {
            if (m_weaponController && stats)
                m_weaponController.activeWeapon.Shoot();
        }

        string GetRandomNickname() {
            string[] nicknameParts1 = new string[] { "Ge", "Me", "Ta", "Bo", "Ke", "Ra", "Ne", "Mi" };
            string[] nicknameParts2 = new string[] { "oo", "ue", "as", "to", "ra", "me", "io", "so" };
            string[] nicknameParts3 = new string[] { "se", "matt", "lace", "fo", "cake", "end" };
            string nicknamePart1 = nicknameParts1[Random.Range(0, nicknameParts1.Length)];
            string nicknamePart2 = nicknameParts2[Random.Range(0, nicknameParts2.Length)];
            string nicknamePart3 = nicknameParts3[Random.Range(0, nicknameParts3.Length)];
            return nicknamePart1 + nicknamePart2 + nicknamePart3;
        }
    }
}