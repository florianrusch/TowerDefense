using UnityEngine;

namespace Level
{
    public class Turret : MonoBehaviour
    {
        [Header("Upgradables")]
        public float fireRate = 0.5f;
        public float range = 15f;
        public float rotationSpeed = 10f;
    
        [Header("Unity Setup Fields")]
        public GameObject bulletPrefab;
        public Transform partToRotate;
        public Transform firePoint;
        public string enemyTag = "Enemy";

        [Header("Debugging")]
        public float fireCountdown = 0f;

        private Transform _target;

        private void Start()
        {
            InvokeRepeating(nameof(UpdateTarget), 0f, 0.5f);
        }

        private void UpdateTarget()
        {
            GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
            float shortestDistance = Mathf.Infinity;
            GameObject nearestEnemy = null;

            foreach (GameObject enemy in enemies)
            {
                float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
                if (distanceToEnemy < shortestDistance)
                {
                    shortestDistance = distanceToEnemy;
                    nearestEnemy = enemy;
                }
            }

            if (nearestEnemy != null && shortestDistance <= range)
            {
                _target = nearestEnemy.transform;
            }
            else
            {
                _target = null;
            }
        }
    
        // Update is called once per frame
        private void Update()
        {
            fireCountdown -= Time.deltaTime;
        
            if (_target == null)
                return;

            Vector3 dir = _target.position - transform.position;
            Quaternion lookRotation = Quaternion.LookRotation(dir);
            Vector3 rotation = Quaternion.Lerp(partToRotate.rotation, lookRotation, Time.deltaTime * rotationSpeed).eulerAngles;
            partToRotate.rotation = Quaternion.Euler(0f, rotation.y, 0f);

            if (fireCountdown <= 0f)
            {
                Shoot();
                fireCountdown = 1f / fireRate;
            }
        }

        private void Shoot()
        {
            GameObject bulletGo = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            Bullet bullet = bulletGo.GetComponent<Bullet>();

            if (bullet != null)
                bullet.Seek(_target);
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, range);
        }
    }
}
