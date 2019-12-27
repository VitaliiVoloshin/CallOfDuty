using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace ShooterFeatures
{
    public class Weapon: MonoBehaviour
    {
        public Transform shootPoint;
        public ActorController owner;
        public WeaponData _weaponData;
        public Color bulletColor;
        public float _oneBulletDamage;

        private ShootLogic m_ShootLogic = new ShootLogic();
        private BattleGrounObserver m_KillJournal;
        private float m_Speed;
        private float m_NextFire;
        private int m_CurrentAmmo;
        private int m_MaxAmmo;
        private bool m_IsReloading;

        void Start()
        {
            m_KillJournal = BattleGrounObserver.instance;
            m_ShootLogic.shootPoint = shootPoint;
            m_CurrentAmmo = _weaponData.bulletsInMagazine;
            m_Speed = NormilizedShootingSpeed(_weaponData.ShotsPerSecond);
            m_MaxAmmo = m_CurrentAmmo;
            bulletColor = BulletColorDependsOnOwner();
        }

        void AddOwnerStatsToWeapon()
        {
            _weaponData.Damage *= owner.stats.damageCaused;
            float convert = _weaponData.ShotsPerSecond;
            convert *= owner.stats.attackSpeed;
            _weaponData.ShotsPerSecond = Mathf.RoundToInt(convert);
        }

        float NormilizedShootingSpeed(float speed)
        {
            return 1 / speed;
        }

        Color BulletColorDependsOnOwner()
        {
            if (owner is PlayerController) {
                return Color.green;
            } else
                return Color.red;
        }

        public void Shoot()
        {
            if (Time.time > m_NextFire) {
                if (m_IsReloading) return;
                if (m_CurrentAmmo < 1) {
                    if(this.isActiveAndEnabled)
                    StartCoroutine(Reload());
                } 
                else {
                    m_ShootLogic.shooter = owner;
                    m_ShootLogic.weapon = this;
                    m_ShootLogic.ShootBullets(_weaponData);
                    m_CurrentAmmo--;
                }
                m_NextFire = Time.time + m_Speed;
            }
        }

        IEnumerator Reload()
        {
            m_IsReloading = true;
            yield return new WaitForSeconds(2f);
            m_CurrentAmmo = m_MaxAmmo;
            m_IsReloading = false;
        }
        private void OnDisable()
        {
            m_IsReloading = false;
        }
    }
}