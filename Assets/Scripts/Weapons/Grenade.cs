using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ShooterFeatures
{
    public class Grenade: MonoBehaviour
    {
        public Sprite grenadeIcon;
        public Vector3[] trajectory;
        public GameObject mesh;
        public ParticleSystem explotionEffect;
        public float explotionDamage;
        public ActorController owner;
        private ActorController m_Owner;
        private int m_Index;

        void Start()
        {
            m_Index = 0;
            StartCoroutine(Detonate(1f));
            m_Owner = owner as ActorController;
        }

        void Update()
        {
            if (trajectory != null) {
                if (m_Index <= trajectory.Length - 1) {
                    Debug.DrawLine(gameObject.transform.position, trajectory[m_Index], Color.yellow, 1f);
                    transform.position = trajectory[m_Index];
                    m_Index += 2;
                }
            }
        }

        private IEnumerator Detonate(float value)
        {
            yield return new WaitForSeconds(value);
            explotionEffect.Play();
            mesh.SetActive(false);
            List<GameObject> units = UnitHolder.instance.units;
            for (int i = 0; i < units.Count; i++) {
                if (units[i] != null) {
                    if (Vector3.Distance(transform.position, units[i].transform.position) <= 5) {
                        Debug.DrawLine(transform.position, units[i].transform.position, Color.red, 2f);
                        Debug.DrawRay(gameObject.transform.position, units[i].transform.position - transform.position, Color.green, 2f);

                        RaycastHit hit;
                        if (Physics.Raycast(transform.position, units[i].transform.position - transform.position, out hit)) {

                            if (hit.transform.GetComponent<ActorController>()) {
                                ActorController enemy = hit.transform.GetComponent<ActorController>();

                                if (enemy.stats.health -  explotionDamage*m_Owner.stats.damageCaused  <= 0) {
                                    enemy.TakeDamage(explotionDamage * m_Owner.stats.damageCaused);
                                    BattleGrounObserver.instance.AddKill(new KillList { Killer = owner.nickname,Weapon = grenadeIcon, Victum = enemy.nickname });
                                    enemy.Death();
                                } else {
                                    enemy.TakeDamage(explotionDamage * m_Owner.stats.damageCaused);
                                }                                
                            }
                        }
                    }
                }
            }
            Invoke(nameof(Destroy), 1f);
        }

        void Destroy()
        {
            Destroy(gameObject);
        }
    }
}