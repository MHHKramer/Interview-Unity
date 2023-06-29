using System;
using UnityEngine;

namespace DefaultNamespace
{
    public class Bullet: MonoBehaviour
    {
        public float speed = 10f;
        private GameObject target;

        public void Start()
        {
            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
            float shortestDistance = Mathf.Infinity;
            foreach (GameObject enemy in enemies)
            {
                float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
                if (distanceToEnemy < shortestDistance)
                {
                    shortestDistance = distanceToEnemy;
                    target = enemy;
                }
            }
        }

        public void Update()
        {
            if (target == null)
            {
                Destroy(gameObject);
            }

            transform.position =
                Vector3.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            Debug.Log("Bullet Trigger Enter");
            if (other.gameObject.tag == "Enemy")
            {
                
                Destroy(other.gameObject);
                Destroy(gameObject);
            }
        }
    }
}