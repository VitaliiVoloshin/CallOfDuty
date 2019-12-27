using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ShooterFeatures
{
    public class UIHealthBarController: MonoBehaviour
    {
        public Image healthBar;

        private Camera m_Camera;
        private float m_MaxHealth;
        private Quaternion m_Rotation;
        private ActorController m_Owner;

        private void Awake()
        {
            m_Owner = GetComponentInParent<ActorController>();
            m_Rotation = transform.rotation;
        }

        void Start()
        {
            m_Camera = Camera.main;
        }



        private void Update()

        {
            if (m_Owner != null) {

                if (m_Owner.fraction == Fraction.GreenTeam)
                    healthBar.color = Color.green;

                if (m_Owner.fraction == Fraction.RedTeam)
                    healthBar.color = Color.red;

            }

            if (m_Owner != null) {
                healthBar.fillAmount = m_Owner.stats.health / m_Owner.stats.maxHealth;
            }
        }

        void LateUpdate()
        {
            transform.rotation = m_Rotation;
            transform.LookAt(transform.position + m_Camera.transform.rotation * Vector3.forward, m_Camera.transform.rotation * Vector3.up);
        }
    }
}