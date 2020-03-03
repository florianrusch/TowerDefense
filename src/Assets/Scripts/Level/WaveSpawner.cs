using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Level
{
    public class WaveSpawner : MonoBehaviour
    {
        public Transform enemyPrefab;
        public Transform spawnPoint;
    
        public float timeBetweenWaves = 10f;
        private float _countdown = 5f;
        private int _waveNumber = 0;

        public Text waveCountdownText;
        private const string WaveCountdownTextFormat = "{0} until incoming...";
        private const string WaveIncomingTextFormat = "!! Incoming !!";

        public Text statsWaveNumberText;
        private const string StatsWaveNumberTextFormat = "Wave: {0}";

        private void Start()
        {
            waveCountdownText.text = string.Format(WaveCountdownTextFormat, $"{_countdown:00.00}");
            statsWaveNumberText.text = string.Format(StatsWaveNumberTextFormat, _waveNumber);
        }
    
        private void Update()
        {
            if (_countdown < 0f)
            {
                StartCoroutine(SpawnWave());
                _countdown = timeBetweenWaves;
            }
        
            _countdown -= Time.deltaTime;
        
            float timeUntilNextWave = Mathf.Clamp(_countdown, 0f, max: Mathf.Infinity);
        
            waveCountdownText.text = timeUntilNextWave > 0 ? string.Format(WaveCountdownTextFormat, $"{timeUntilNextWave:00.00}") : WaveIncomingTextFormat;
        }

        private IEnumerator SpawnWave()
        {
            IncreaseWaveNumber();
        
            for (int i = 0; i < _waveNumber; i++)
            {
                SpawnEnemy();
                yield return new WaitForSeconds(0.8f);
            }
        }

        private void IncreaseWaveNumber()
        {
            _waveNumber++;
            statsWaveNumberText.text = string.Format(StatsWaveNumberTextFormat, _waveNumber);
        }

        private void SpawnEnemy()
        {
            Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);
        }
    }
}
