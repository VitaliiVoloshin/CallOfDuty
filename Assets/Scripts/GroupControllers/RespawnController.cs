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

        public void RespawnRequest(ActorController unit)
        {
            unit.SelfRespawn();
        }

        public void RespawnPlayer() {
            respawnUI.SetActive(true);
        }

        public void RespawnPlayerAtRandomPoint()
        {
            Transform point = m_SpawnPoints[Random.Range(0, m_SpawnPoints.Length - 1)].transform;
            m_Player.GetComponent<ActorController>().stats.health = 100;
            m_Player.transform.position = point.position;
            m_Player.SetActive(true);
            respawnUI.SetActive(false);
        }

        public void DefaultRespawn(ActorController unit)
        {
            StartCoroutine(SimpleShooterResurectionProcessStart(5f, unit));
        }

        IEnumerator SimpleShooterResurectionProcessStart(float delay, ActorController simpleShooter)
        {
            yield return new WaitForSeconds(delay);
            simpleShooter.stats.health = 100;
            simpleShooter.gameObject.SetActive(true);
        }

    }
}