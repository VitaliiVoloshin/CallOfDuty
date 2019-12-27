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
        private string m_identificator;
        [SerializeField]
        private int m_shotsPerSecond;
        [SerializeField]
        private float m_reloadSpeed;
        [SerializeField]
        private float m_damage;
        [SerializeField]
        private int m_bulletsPerShoot;
        [SerializeField]
        private float m_shootingRange;
        [SerializeField]
        private int m_spreading;
        [SerializeField]
        private int m_bulletsInMagazine;

        private float m_spreadingDegree;

        public float spreadingDegree {
            get {
                return 180 - 2 * Mathf.Rad2Deg * Mathf.Atan(m_shootingRange * 2 / m_spreading);
            }
        }

        public Sprite icon {
            get {
                return m_Icon;
            }
        }
        public string Identificator {
            get {
                return m_identificator;
            }
        }

        public int ShotsPerSecond {
            get {
                return m_shotsPerSecond;
            }

            set {
                m_shotsPerSecond = value;
            }
        }

        public float RealadSpeed {
            get {
                return m_reloadSpeed;
            }
        }

        public float Damage {
            get {
                return m_damage;
            }

            set {
                m_damage = value;
            }
        }

        public int BulletsPerShoot {
            get {
                return m_bulletsPerShoot;
            }
        }

        public float ShootingRange {
            get {
                return m_shootingRange;
            }
        }

        public int Spreading {
            get {
                return m_spreading;
            }
        }

        public int bulletsInMagazine {
            get {
                return m_bulletsInMagazine;
            }
        }

    }
}