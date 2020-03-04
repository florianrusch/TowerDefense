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
        private Enemy _targetEnemy;

        public void Seek(Transform target)
        {
            _target = target;
            _targetEnemy = target.GetComponent<Enemy>();
        }

        private void Update()
        {
            if (_target == null || _targetEnemy.health <= 0)
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
                Damage(_targetEnemy);
            }

            Destroy(gameObject);
        }

        private void Damage(Enemy target)
        {
            if (target != null)
            {
                target.ApplyDamage(damage);
            }
        }

        private void Explode()
        {
            Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);
            foreach (Collider explosionTarget in colliders)
            {
                if (explosionTarget.CompareTag(enemyTag))
                {
                    Damage(explosionTarget.transform.GetComponent<Enemy>());
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