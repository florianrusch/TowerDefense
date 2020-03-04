using UnityEngine;
using UnityEngine.UI;

namespace Level
{
    public class Enemy : MonoBehaviour
    {
        public float startSpeed = 10f;
        public float startHealth = 100f;
        public int earning = 5;

        [Header("Unity Setup Fields")]
        public Transform healthBar;
        
        private float _speed;
        private float _health;

        private Transform _target;
        private int _waypointIndex = 0;
        private PlayerStats _playerStats;

        private void Start()
        {
            _speed = startSpeed;
            _health = startHealth;
            _target = Waypoints.points[0];
            _playerStats = PlayerStats.instance;
        }

        private void Update()
        {
            Vector3 dir = _target.position - transform.position;
            transform.Translate(dir.normalized * (_speed * Time.deltaTime), Space.World);

            if (Vector3.Distance(transform.position, _target.position) <= 0.6f)
            {
                GetNextWaypoint();
            }
        }

        public float Health => _health;
        
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
            _health -= damage;
            
            // Update healthBar
            healthBar.GetComponent<Image>().fillAmount = _health / startHealth;

            if (_health > 0) return;

            _playerStats.KilledEnemy(earning);
            Destroy(gameObject);
        }
    }
}