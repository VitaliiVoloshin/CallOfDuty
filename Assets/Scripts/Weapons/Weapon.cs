using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace ShooterFeatures
{
    public enum WeaponType
    {
        shotgun,
        autorifle
    }

    public class Weapon: MonoBehaviour
    {
        public Transform shootPoint;
        public ActorController owner;
        public WeaponStatsController stats;
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
            stats = GetComponent<WeaponStatsController>();
            m_ShootLogic.shootPoint = shootPoint;
            m_CurrentAmmo = stats.bulletsInMagazine;
            m_Speed = NormilizedShootingSpeed(stats.shotsPerSecond);
            m_MaxAmmo = m_CurrentAmmo;
            bulletColor = BulletColorDependsOnOwner();
        }

        void AddOwnerStatsToWeapon()
        {
            stats.damage *= owner.stats.damageCaused;
            float convert = stats.shotsPerSecond;
            convert *= owner.stats.attackSpeed;
            stats.shotsPerSecond = Mathf.RoundToInt(convert);
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
            Debug.Log(Time.time + " > " + m_NextFire);
            if (Time.time > m_NextFire) {
                if (m_IsReloading) return;
                if (m_CurrentAmmo < 1) {
                    if(this.isActiveAndEnabled)
                    StartCoroutine(Reload());
                } 
                else {
                    m_ShootLogic.shooter = owner;
                    m_ShootLogic.weapon = this;
                    m_ShootLogic.ShootBullets(stats);
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