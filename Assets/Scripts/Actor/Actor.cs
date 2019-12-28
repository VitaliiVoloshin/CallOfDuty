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
    public class ActorController: MonoBehaviour
    {
        public string nickname;
        public Fraction fraction;

        public ActorStatsController stats;
        protected WeaponController m_weaponController;
        private RandomNicknameGenerator nicknameGenerator = new RandomNicknameGenerator();

        private void Start()
        {
            m_weaponController = GetComponentInChildren<WeaponController>();
            stats = GetComponent<ActorStatsController>();
            AddToUnitHolder(UnitHolder.instance);
            nickname = nicknameGenerator.GetRandomNickname();
        }


        private void OnDisable()
        {
            if (RespawnController.instance) {
                RespawnController.instance.RespawnRequest(this);
            }
        }

        public virtual void SelfRespawn()
        {
            RespawnController.instance.DefaultRespawn(this);
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
    }
}