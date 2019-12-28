using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace ShooterFeatures
{
    public class Bullet: MonoBehaviour
    {
        public Vector3 Direction;

        private void FixedUpdate()
        {
            transform.position += Direction * Time.deltaTime * 3;            
        }

        private void OnEnable()
        {
            StartCoroutine(SelfDestruction(1.5f));
        }

        private IEnumerator SelfDestruction(float value)
        {
            yield return new WaitForSeconds(value);
            gameObject.SetActive(false);
        }

        private void OnCollisionStay(Collision collision)
        {
            if (!collision.gameObject.GetComponent<Bullet>())
                gameObject.SetActive(false);
        }
    }
}
