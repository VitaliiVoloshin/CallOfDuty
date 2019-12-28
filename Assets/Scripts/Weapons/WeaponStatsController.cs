using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace ShooterFeatures
{
    public class WeaponStatsController: MonoBehaviour
    {
        [SerializeField] private WeaponData m_WeaponData;

        [HideInInspector] public Sprite icon;
        [HideInInspector] public WeaponType identificator;
        public int shotsPerSecond;
        [HideInInspector] public float reloadSpeed;
        [HideInInspector] public float damage;
        [HideInInspector] public int bulletsPerShoot;
        [HideInInspector] public float shootingRange;
        [HideInInspector] public int spreading;
        [HideInInspector] public int bulletsInMagazine;

        public float spreadingDegree => m_WeaponData.spreadingDegree;

        private void Awake()
        {
            icon = m_WeaponData.icon;
            identificator = m_WeaponData.identificator;
            shotsPerSecond = m_WeaponData.shotsPerSecond;
            reloadSpeed = m_WeaponData.reloadSpeed;
            damage = m_WeaponData.damage;
            bulletsPerShoot = m_WeaponData.bulletsPerShoot;
            shootingRange = m_WeaponData.shootingRange;
            spreading = m_WeaponData.spreading;
            bulletsInMagazine = m_WeaponData.bulletsInMagazine;
        }
    }
}