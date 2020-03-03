using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Level
{
    public class WaveSpawner : MonoBehaviour
    {
        public Transform enemyPrefab;
        public Transform spawnPoint;

        public Text waveCountdownText;
        private const string WaveCountdownTextFormat = "{0} until incoming...";
        private const string WaveIncomingTextFormat = "!! Incoming !!";
        private float _countdown = 5f;
        private bool _isCountdownEnabled = true;

        public Text statsWaveNumberText;
        private const string StatsWaveNumberTextFormat = "Wave: {0}";
        private int _waveNumber = 0;

        private PlayerStats _playerStats;

        private void Start()
        {
            _playerStats = PlayerStats.instance;

            waveCountdownText.text = string.Format(WaveCountdownTextFormat, $"{_countdown:00.00}");
            statsWaveNumberText.text = string.Format(StatsWaveNumberTextFormat, _waveNumber);
        }
    
        private void Update()
        {
            if (!_isCountdownEnabled) return;
            
            if (_countdown <= 0f)
            {
                _isCountdownEnabled = false;
                waveCountdownText.text = WaveIncomingTextFormat;
                
                StartCoroutine(SpawnWave());
                
                return;
            }
        
            _countdown -= Time.deltaTime;
        
            float timeUntilNextWave = Mathf.Clamp(_countdown, 0f, max: Mathf.Infinity);
            waveCountdownText.text = timeUntilNextWave > 0 ? string.Format(WaveCountdownTextFormat, $"{timeUntilNextWave:00.00}") : WaveIncomingTextFormat;
        }

        public void StartNewWave()
        {
            if (!_isCountdownEnabled) return;
            
            _countdown = -1f;
        }

        private IEnumerator SpawnWave()
        {
            IncreaseWaveNumber();

            for (int i = 0; i < _waveNumber; i++)
            {
                SpawnEnemy();
                yield return new WaitForSeconds(GetTimeBetweenEnemySpawn());
            }
            
            _isCountdownEnabled = true;
            _countdown = GetInitialCountdownForWave(_waveNumber);
        }

        private void SpawnEnemy()
        {
            Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);
            _playerStats.IncreaseLivingEnemies();
        }
        
        private float GetTimeBetweenEnemySpawn()
        {
            return 0.8f;
        }

        private float GetInitialCountdownForWave(int waveNumber)
        {
            return 10f;
        }

        private void IncreaseWaveNumber()
        {
            _waveNumber++;
            statsWaveNumberText.text = string.Format(StatsWaveNumberTextFormat, _waveNumber);
        }
    }
}
