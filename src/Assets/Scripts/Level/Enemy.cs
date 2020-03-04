using UnityEngine;

namespace Level
{
    public class Enemy : MonoBehaviour
    {
        public float speed = 10f;
        public float health = 100f;
        public int earning = 5;

        private Transform _target;
        private int _waypointIndex = 0;
        private PlayerStats _playerStats;

        private void Start()
        {
            _target = Waypoints.points[0];
            _playerStats = PlayerStats.instance;
        }

        private void Update()
        {
            Vector3 dir = _target.position - transform.position;
            transform.Translate(dir.normalized * (speed * Time.deltaTime), Space.World);

            if (Vector3.Distance(transform.position, _target.position) <= 0.6f)
            {
                GetNextWaypoint();
            }
        }

        private void GetNextWaypoint()
        {
            if (_waypointIndex >= Waypoints.points.Length - 1)
            {
                Destroy(gameObject);
                return;
            }

            _waypointIndex++;
            _target = Waypoints.points[_waypointIndex];
        }

        public void ApplyDamage(int damage)
        {
            health -= damage;

            if (health > 0) return;

            _playerStats.KilledEnemy(earning);
            Destroy(gameObject);
        }
    }
}