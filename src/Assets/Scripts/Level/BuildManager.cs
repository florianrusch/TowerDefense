using UnityEngine;

namespace Level
{
    public class BuildManager : MonoBehaviour
    {
        public static BuildManager instance;

        public GameObject standardTurretPrefab;
        public GameObject missileLauncherPrefab;
    
        private TurretBlueprint _turretToBuild;

        private PlayerStats _playerStats;

        private void Awake()
        {
            if (instance == null)
                instance = this;
        }

        private void Start()
        {
            _playerStats = PlayerStats.instance;
        }
    
        public bool CanBuild => _turretToBuild != null;
        public bool HasMoney => _playerStats.Money >= _turretToBuild.cost;

        public void BuildTurretOn (Node node)
        {
            if (_playerStats.Money < _turretToBuild.cost)
            {
                Debug.Log("Not enough money to build that!");
                return;
            }
        
            _playerStats.PurchasedTurret(_turretToBuild.cost);
        
            GameObject turret = Instantiate(_turretToBuild.prefab, node.GetBuildPosition(), Quaternion.identity);
            node.turret = turret;

            Debug.Log("Turret build! Money left: " + _playerStats.Money);
        }

        public void SelectTurretToBuild (TurretBlueprint turret)
        {
            _turretToBuild = turret;
        }
    }
}
