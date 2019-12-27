using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace ShooterFeatures
{
    public class RespawnController: MonoBehaviour
    {
        public static RespawnController instance;
        public GameObject respawnUI;
        private UnitHolder m_UnitHolder;
        private GameObject[] m_SpawnPoints;
        private float m_NextScan;
        private GameObject m_Player;

        private void Awake()
        {
            instance = this;
        }
        private void OnEnable()
        {
            m_UnitHolder = UnitHolder.instance;
        }
        void Start()
        {
            m_SpawnPoints = new GameObject[GameObject.FindGameObjectsWithTag("Respawn").Length];
            m_SpawnPoints = GameObject.FindGameObjectsWithTag("Respawn");
        }

        public void RespawnRequest(GameObject unit)
        {

            if (unit.GetComponent<PlayerController>()) {
                respawnUI.SetActive(true);
                m_Player = unit;
            } else if (unit.GetComponent<SimpleShooter>()) {
                RespawnSimpleShooter(unit);
            } else {
                Debug.Log("Unit respawn denied");
            }
        }

        public void RespawnPlayerAtRandomPoint()
        {
            Transform point = m_SpawnPoints[Random.Range(0, m_SpawnPoints.Length - 1)].transform;
            m_Player.GetComponent<ActorController>().stats.health = 100;
            m_Player.transform.position = point.position;
            m_Player.SetActive(true);
            respawnUI.SetActive(false);
        }

        void RespawnSimpleShooter(GameObject simpleShooter)
        {
            StartCoroutine(SimpleShooterResurectionProcessStart(5f, simpleShooter));
        }

        IEnumerator SimpleShooterResurectionProcessStart(float delay, GameObject simpleShooter)
        {
            yield return new WaitForSeconds(delay);
            simpleShooter.GetComponent<ActorController>().stats.health = 100;
            simpleShooter.SetActive(true);
        }

    }
}