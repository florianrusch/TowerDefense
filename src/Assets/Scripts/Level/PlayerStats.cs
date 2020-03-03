using UnityEngine;
using UnityEngine.UI;

namespace Level
{
    public class PlayerStats : MonoBehaviour
    {
        public static PlayerStats instance;
    
        public int startMoney = 400;

        public Text statsMoneyText;
        private const string StatsMoneyTextFormat = "$ {0}";
    
        public Text statsKilledEnemiesText;
        private int _killedEnemies = 0;
        private const string StatsKilledEnemiesTextFormat = "Kills: {0}";
    
        private void Awake()
        {
            if (instance == null)
                instance = this;
        }

        private void Start()
        {
            Money = startMoney;
            UpdateStats();
        }

        public int Money { get; private set; }

        public void PurchasedTurret(int money)
        {
            Money -= money;
        
            UpdateStats();
        }

        public void KilledEnemy(int money)
        {
            Money += money;
            _killedEnemies++;
        
            UpdateStats();
        }

        private void UpdateStats()
        {
            statsMoneyText.text = string.Format(StatsMoneyTextFormat, Money);
            statsKilledEnemiesText.text = string.Format(StatsKilledEnemiesTextFormat, _killedEnemies);
        }
    }
}
