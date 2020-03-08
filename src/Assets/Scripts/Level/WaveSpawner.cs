using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Level
{
    public class WaveSpawner : MonoBehaviour
    {
        public Transform spawnPoint;

        public Text countdownText;
        private const string CountdownTextFormat = "{0} until incoming...";
        private const string CountdownIncomingTextFormat = "!! Incoming !!";
        private float _countdown;
        private bool _isCountdownStarted = true;

        public Wave[] waves;
        public Text statsWaveNumberText;
        private const string StatsWaveNumberTextFormat = "Wave: {0}";
        private int _spawnedWaves;
        private int _spawnedEnemySeriesForWave;

        private bool _isIncoming;

        private PlayerStats _playerStats;

        private void Start()
        {
            _playerStats = PlayerStats.instance;

            // Init countdown for first wave
            _countdown = waves[0].initialDelay;
            _isCountdownStarted = true;
            
            UpdateCountdownText();
            UpdateStatsWaveNumberText();
        }
    
        private void Update()
        {
            if (_isCountdownStarted)
            {
                _countdown -= Time.deltaTime;
                
                UpdateCountdownText();

                if (_countdown > 0f) return;
                
                StartWave();
                return;
            }

            if (_isIncoming)
                return;

            if (WasLastWave())
                return;
            
            // Init countdown for next wave
            _countdown = waves[_spawnedWaves].initialDelay;
            _isCountdownStarted = true;
        }

        private bool WasLastWave()
        {
            return (_spawnedWaves == waves.Length);
        }

        public void ManualStartWave()
        {
            _countdown = 0;
        }
        
        private void StartWave()
        {
            _isCountdownStarted = false;
            _spawnedEnemySeriesForWave = 0;
            _isIncoming = true;
            
            Wave wave = waves[_spawnedWaves];
            
            foreach (var enemySeries in wave.enemies)
            {
                StartCoroutine(SpawnEnemySeries(enemySeries));
            }

            _spawnedWaves++;
            
            UpdateStatsWaveNumberText();
        }

        private IEnumerator SpawnEnemySeries(Wave.EnemySeries waveEnemies)
        {
            for (int i = 0; i < waveEnemies.amount; i++)
            {
                Instantiate(waveEnemies.enemy, spawnPoint.position, spawnPoint.rotation);
                _playerStats.IncreaseLivingEnemies();
                yield return new WaitForSeconds(1f / waveEnemies.rate);
            }

            _spawnedEnemySeriesForWave++;
            if (_spawnedEnemySeriesForWave == waves[_spawnedWaves-1].enemies.Length)
                _isIncoming = false;
        }

        private void UpdateCountdownText()
        {
            float timeUntilNextWave = Mathf.Clamp(_countdown, 0f, max: Mathf.Infinity);
            countdownText.text = timeUntilNextWave > 0 ? string.Format(CountdownTextFormat, $"{timeUntilNextWave:00.00}") : CountdownIncomingTextFormat;
        }

        private void UpdateStatsWaveNumberText()
        {
            statsWaveNumberText.text = string.Format(StatsWaveNumberTextFormat, _spawnedWaves);
        }

        [Serializable]
        public class Wave
        {
            public EnemySeries[] enemies;
            public float initialDelay = 10f;

            [Serializable]
            public class EnemySeries
            {
                public Transform enemy;
                public float rate = 1f;
                public int amount = 1;
            }
        }
    }
}
