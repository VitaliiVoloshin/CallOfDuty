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

        public void ShootBullets(WeaponData weaponData)
        {
            int howmany = weaponData.BulletsPerShoot;
            while (howmany != 0) {
                Vector3 direction = GetRandomDirection(new Vector2(weaponData.spreadingDegree, weaponData.ShootingRange));
                BulletInstantiation(direction);
                SingleBulletEffect(direction, weaponData);
                howmany--;
            }
        }

        Vector3 GetRandomDirection(Vector2 direction)
        {
            return RandomRayPoint(direction.x, direction.y);
        }

        Vector3 RandomRayPoint(float spread, float range)
        {
            float degree = Random.Range(-spread / 2, spread / 2);
            Quaternion angle = Quaternion.AngleAxis(degree, new Vector3(0, 1, 0));
            return angle * shootPoint.forward * range;
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

        void SingleBulletEffect(Vector3 direction, WeaponData weaponData)
        {
            RaycastHit hit;
            if (Physics.Raycast(shootPoint.position, direction, out hit)) {

                if (hit.transform.GetComponent<ActorController>()) {

                    ActorController enemy = hit.transform.GetComponent<ActorController>();
                    if (enemy.stats.health - weaponData.Damage <= 0) {
                        enemy.stats.health -= weaponData.Damage;
                        BattleGrounObserver.instance.AddKill(new KillList { Killer = shooter.nickname, Weapon = weaponData.icon, Victum = enemy.nickname });
                        enemy.Death();
                    } else {
                        enemy.stats.health -= weaponData.Damage;
                    }
                }
            }
        }
    }
}