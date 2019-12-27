using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ShooterFeatures
{

    public class GrenadeThrower: MonoBehaviour
    {
        public LineRenderer trajectory;
        public GameObject targetArea;
        public float grenadeDamage = 50;
        public float max_force = 20f;
        public float min_force = 5f;
        public float trajectoryWidth = 0.4f;

        [SerializeField] private int m_trajectoryDetalization = 50;
        [SerializeField] private Sprite grenadeIcon;
        [SerializeField] private InputController InputController;

        private float m_ThrowForce;
        private LineRenderer m_trajectory;
        private GameObject m_targetArea;
        private Transform m_Transform;
        private Ray m_cameraRay;
        private RaycastHit m_cameraRayHit;
        private KeyCode launchButton;

        private void Awake()
        {
            m_Transform = gameObject.transform;
            m_trajectory = Instantiate(trajectory);
            m_targetArea = Instantiate(targetArea);
            launchButton = InputController.grenadeThrow;
        }

        private void Update()
        {
            if (Input.GetKeyDown(launchButton)) {
                m_ThrowForce = 0;
            }

            if (Input.GetKey(launchButton)) {
                ShowForce();

                if (m_targetArea.activeSelf) {
                    m_ThrowForce = Vector3.Distance(transform.position, RotationToCursor(transform));
                }

                if (m_ThrowForce > max_force) {
                    m_ThrowForce = max_force;
                }
                if (m_ThrowForce < min_force) {
                    m_ThrowForce = min_force;
                }

                if (m_ThrowForce != 0 && m_targetArea != null) {
                    CastParabola(m_ThrowForce);
                } else {
                    HideForce();
                }
            }

            if (Input.GetKeyUp(launchButton)) {
                Launch(CastParabola(m_ThrowForce));
                HideForce();

            }
        }

        private void OnEnable()
        {
            HideForce();
        }

        private void OnDisable()
        {
            HideForce();
        }

        public void ShowForce()
        {
            if (m_trajectory != null && m_targetArea != null) {
                m_trajectory.gameObject.SetActive(true);
                m_targetArea.SetActive(true);
            }
        }

        private void HideForce()
        {
            if (m_trajectory != null && m_targetArea != null) {
                m_trajectory.gameObject.SetActive(false);
                m_targetArea.SetActive(false);
            }
        }

        void Launch(Vector3[] trajectory)
        {
            if (trajectory != null) {
                GameObject grenade = Instantiate(Resources.Load("Grenade"), transform.position, transform.rotation) as GameObject;
                Grenade grenadeComponent = grenade.GetComponent<Grenade>();
                grenadeComponent.trajectory = trajectory;
                grenadeComponent.explotionDamage = grenadeDamage;
                grenadeComponent.grenadeIcon = grenadeIcon;
                grenadeComponent.owner = GetComponentInParent<ActorController>();
            }
        }

        Vector3[] CastParabola(float range)
        {
            Vector3[] point;
            point = new Vector3[m_trajectoryDetalization];
            point = MakeTrajectory(range, true);
            return point;
        }

        Vector3[] RecastParabola(float range)
        {
            Vector3[] point;
            point = new Vector3[m_trajectoryDetalization];
            point = MakeTrajectory(range, false);
            return point;
        }

        Vector3[] MakeTrajectory(float lenght, bool withRaycast)
        {
            Vector3[] trajectoryPoints = new Vector3[m_trajectoryDetalization];
            bool detectRaycast = false;
            Vector3 raycastPoint = Vector3.zero;

            int numberOnInstances = m_trajectoryDetalization;
            int instanceCount = numberOnInstances;
            float i = 0;

            while (instanceCount > 0) {
                Vector3 parabolaPoint;
                Vector3 direction = transform.position + transform.forward * i;
                direction.y = GameObject.FindGameObjectWithTag("Ground").transform.position.y;
                Vector3 worldPoint;
                parabolaPoint = Vector3.zero;
                parabolaPoint.y = (-Mathf.Pow(i, 2) + lenght * i) / lenght;
                parabolaPoint += direction;
                worldPoint = parabolaPoint;
                trajectoryPoints[numberOnInstances - instanceCount] = worldPoint;
                i += lenght / numberOnInstances;
                instanceCount--;

                if (withRaycast) {
                    if (!detectRaycast) {
                        RaycastHit hit;
                        if (instanceCount % 5 == 0) {
                            if (Physics.Raycast(trajectoryPoints[numberOnInstances - instanceCount - 1], transform.forward, out hit, 5f)) {
                                Debug.DrawRay(trajectoryPoints[numberOnInstances - instanceCount - 1], transform.forward * 5, Color.red);
                                bool dd = CheckForActiveObject(hit.transform.gameObject);
                                if (CheckForActiveObject(hit.transform.gameObject)) {
                                    detectRaycast = true;
                                    raycastPoint = trajectoryPoints[numberOnInstances - instanceCount - 1];
                                }
                            }
                        }
                    }
                }
            }

            if (withRaycast) {
                if (detectRaycast) {
                    float lul = Vector3.Distance(transform.position, raycastPoint);
                    Vector3[] redraw = RecastParabola(lul);
                    m_trajectory.SetPositions(redraw);
                    m_targetArea.transform.position = redraw[redraw.Length - 1];
                    return redraw;
                } else {
                    
                    m_trajectory.SetPositions(trajectoryPoints);
                    m_targetArea.transform.position = trajectoryPoints[trajectoryPoints.Length - 1];
                    TrajectorySetup();
                    return trajectoryPoints;

                }
            } else {
                
                m_trajectory.SetPositions(trajectoryPoints);
                if (trajectoryPoints.Length == m_trajectoryDetalization) {
                    m_targetArea.transform.position = trajectoryPoints[trajectoryPoints.Length - 1];
                }
                TrajectorySetup();
                return trajectoryPoints;
            }
        }

        void TrajectorySetup()
        {
            m_trajectory.SetVertexCount(m_trajectoryDetalization);
            m_trajectory.SetWidth(trajectoryWidth, trajectoryWidth);
        }

        bool CheckForActiveObject(GameObject unit)
        {
            if (unit.GetComponent<Bullet>() ||
                unit.GetComponent<ActorController>() ||
                unit.GetComponent<Grenade>()) {
                return false;
            } else {
                return true;
            }
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
    }
}