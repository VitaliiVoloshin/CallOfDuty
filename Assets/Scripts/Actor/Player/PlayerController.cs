using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace ShooterFeatures
{
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(CapsuleCollider))]
    public class PlayerController: ActorController
    {
        [SerializeField] private InputController input;
        [SerializeField] private float m_MovementSpeedWithShotgun;
        [SerializeField] private float m_MovementSpeedWithAutoRifle;

        private Ray m_cameraRay;
        private RaycastHit m_cameraRayHit;
        private KeyCode m_shootbutton;
        private string m_verticalAxis;
        private string m_horizontalAxis;
        private Transform m_Transform;

        void Awake()
        {
            SetUpControls();
            m_Transform = gameObject.transform;
        }

        void SetUpControls()
        {
            m_shootbutton = input.shootButton;
            m_verticalAxis = input.verticalAxis;
            m_horizontalAxis = input.horizontalAxis;
        }

        public override void SelfRespawn() {
            RespawnController.instance.RespawnPlayer();
        }




        void Update()
        {
            transform.LookAt(RotationToCursor(transform));

            if (Input.GetAxis(m_verticalAxis) != 0f || Input.GetAxis(m_horizontalAxis) != 0f) {
                if (Input.GetKey(m_shootbutton)) {
                    Shoot();
                    Movement(stats.movementSpeed * MovementSpeedDepensOnActiveWeapon());
                } else {
                    Movement(stats.movementSpeed);
                }

            } else if (Input.GetKey(m_shootbutton)) {
                Shoot();
            }

            if (m_Transform.position.y < -10) {
                TakeDamage(stats.health);
                Death();
            }
        }

        float MovementSpeedDepensOnActiveWeapon()
        {
            if (m_weaponController.activeWeapon.stats.identificator == WeaponType.autorifle) {
                return m_MovementSpeedWithAutoRifle;

            }
            if (m_weaponController.activeWeapon.stats.identificator == WeaponType.shotgun) {

                return m_MovementSpeedWithShotgun;
            } else return 1f;
        }

        public Vector3 RotationToCursor(Transform position)
        {
            m_cameraRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(m_cameraRay, out m_cameraRayHit)) {
                Vector3 targetPosition = new Vector3(m_cameraRayHit.point.x, transform.position.y, m_cameraRayHit.point.z);
                return targetPosition;
            }
            return Vector3.zero;

        }

        void Movement(float speed)
        {
            Vector3 targetVelocity = new Vector3(Input.GetAxis(m_horizontalAxis), 0, Input.GetAxis(m_verticalAxis));
            targetVelocity *= speed * 10;
            Vector3 velocity = GetComponent<Rigidbody>().velocity;
            Vector3 velocityChange = (targetVelocity - velocity);
            velocityChange.x = Mathf.Clamp(velocityChange.x, -10, 10);
            velocityChange.z = Mathf.Clamp(velocityChange.z, -10, 10);
            velocityChange.y = 0;
            GetComponent<Rigidbody>().AddForce(velocityChange, ForceMode.VelocityChange);
        }

        float AngleBetweenTwoPoints(Vector3 a, Vector3 b)
        {
            return Mathf.Atan2(a.y - b.y, a.x - b.x) * Mathf.Rad2Deg;
        }
    }
}