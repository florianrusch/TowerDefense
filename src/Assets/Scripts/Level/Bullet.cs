using UnityEngine;

namespace Level
{
    public class Bullet : MonoBehaviour
    {
        [Header("Bullet config")]
        public float speed = 70f;
        public int damage = 10;

        public float explosionRadius = 0f;
        public GameObject impactEffect;

        [Header("Unity Setup Fields")]
        public string enemyTag = "Enemy";

        private Transform _target;

        public void Seek(Transform target)
        {
            _target = target;
        }

        // Update is called once per frame
        private void Update()
        {
            if (_target == null)
            {
                Destroy(gameObject);
                return;
            }

            Vector3 dir = _target.position - transform.position;
            float distanceThisFrame = speed * Time.deltaTime;

            if (dir.magnitude <= distanceThisFrame)
            {
                HitTarget();
                return;
            }

            transform.Translate(dir.normalized * distanceThisFrame, Space.World);
            transform.LookAt(_target);
        }

        private void HitTarget()
        {
            if (impactEffect != null)
            {
                GameObject effectInstance = Instantiate(impactEffect, transform.position, transform.rotation);
                Destroy(effectInstance, 2f);
            }

            if (explosionRadius > 0f)
            {
                Explode();
            }
            else
            {
                Damage(_target);
            }

            Destroy(gameObject);
        }

        private void Damage(Component enemyTransform)
        {
            Enemy enemy = enemyTransform.GetComponent<Enemy>();

            if (enemy != null)
            {
                enemy.ApplyDamage(damage);
            }
        }
    
        private void Explode()
        {
            Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);
            foreach (Collider explosionTarget in colliders)
            {
                if (explosionTarget.CompareTag(enemyTag))
                {
                    Damage(explosionTarget.transform);
                }
            }
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, explosionRadius);
        }
    }
}
