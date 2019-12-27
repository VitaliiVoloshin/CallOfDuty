using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace ShooterFeatures
{
    [CreateAssetMenu(fileName = "New WeaponData", menuName = "Weapon Data", order = 51)]

    

    public class WeaponData: ScriptableObject
    {
        [SerializeField]
        private Sprite m_Icon;
        [SerializeField]
        private WeaponType m_Identificator;
        [SerializeField]
        private int m_ShotsPerSecond;
        [SerializeField]
        private float m_ReloadSpeed;
        [SerializeField]
        private float m_Damage;
        [SerializeField]
        private int m_BulletsPerShoot;
        [SerializeField]
        private float m_ShootingRange;
        [SerializeField]
        private int m_Spreading;
        [SerializeField]
        private int m_BulletsInMagazine;

        public float spreadingDegree {
            get {
                return 180 - 2 * Mathf.Rad2Deg * Mathf.Atan(m_ShootingRange * 2 / m_Spreading);
            }
        }

        public Sprite icon => m_Icon;
        public WeaponType identificator => m_Identificator;
        public int shotsPerSecond => m_ShotsPerSecond;
        public float reloadSpeed => m_ReloadSpeed;
        public float damage => m_Damage;
        public int bulletsPerShoot => m_BulletsPerShoot;
        public float shootingRange => m_ShootingRange;
        public int spreading => m_Spreading;
        public int bulletsInMagazine => m_BulletsInMagazine;

    }
}