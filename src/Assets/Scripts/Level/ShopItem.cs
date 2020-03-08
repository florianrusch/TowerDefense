using UnityEngine;
using UnityEngine.UI;

namespace Level
{
    public class ShopItem : MonoBehaviour
    {
        public TurretBlueprint turret;
    
        private BuildManager _buildManager;
        private Transform _purchasablePanel;
    
        private PlayerStats _playerStats;

        private void Awake()
        {
            _purchasablePanel = transform.Find("Purchasable");
        
            transform.Find("PricePanel").Find("Price").GetComponent<Text>().text = "$" + turret.cost;
        
            Image icon = transform.Find("Image").GetComponent<Image>();
            icon.sprite = turret.shopIcon;
            icon.enabled = true;
        }

        private void Start()
        {
            _buildManager = BuildManager.instance;
            _playerStats = PlayerStats.instance;
        }

        private void Update()
        {
            _purchasablePanel.gameObject.SetActive(_playerStats.Money < turret.cost);
        }
    
        public void SelectTurret()
        {
            _buildManager.TurretToBuild = turret;
        }
    }
}
