using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace ShooterFeatures
{
    public class ShootLogic
    {
        public Transform shootPoint;
        [HideInInspector] public ActorController shooter;
        [HideInInspector] public Weapon weapon;
        private DirectionRandomizator directionRandomizator = new DirectionRandomizator();

        public void ShootBullets(WeaponStatsController weaponData)
        {
            int howmany = weaponData.bulletsPerShoot;
            while (howmany != 0) {
                Vector3 direction = directionRandomizator.GetRandomDirection(shootPoint,new Vector2(weaponData.spreadingDegree, weaponData.shootingRange));
                BulletInstantiation(direction);
                SingleBulletEffect(direction, weaponData);
                howmany--;
            }
        }

        void BulletInstantiation(Vector3 direction)
        {
            GameObject newBullet = ObjectPooler.SharedInstance.GetPooledObject();
            if (newBullet != null) {
                newBullet.transform.position = shootPoint.transform.position;
                newBullet.transform.rotation = shootPoint.transform.rotation;
                newBullet.SetActive(true);
            }
            newBullet.GetComponent<Bullet>().Direction = direction;
            newBullet.GetComponent<Renderer>().material.color = Color.blue;
        }

        void SingleBulletEffect(Vector3 direction, WeaponStatsController weaponData)
        {
            RaycastHit hit;
            if (Physics.Raycast(shootPoint.position, direction, out hit)) {

                if (hit.transform.GetComponent<ActorController>()) {

                    ActorController enemy = hit.transform.GetComponent<ActorController>();
                    if (enemy.stats.health - weaponData.damage <= 0) {
                        enemy.TakeDamage(weaponData.damage);
                        BattleGrounObserver.instance.AddKill(new KillList { Killer = shooter.nickname, Weapon = weaponData.icon, Victum = enemy.nickname });
                        enemy.Death();
                    } else {
                        enemy.stats.health -= weaponData.damage;
                    }
                }
            }
        }
    }
}