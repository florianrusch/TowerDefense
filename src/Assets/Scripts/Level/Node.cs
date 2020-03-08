using UnityEngine;
using UnityEngine.EventSystems;

namespace Level
{
    public class Node : MonoBehaviour
    {
        public Vector3 turretOffset; 
        public Color hoverColor = Color.red;
    
        private Color _startColor;
    
        [Header("Optional")]
        public GameObject turret;

        private GameObject _previewTurret;

        private Renderer _rend;

        private BuildManager _buildManager;
    
        private void Start()
        {
            _rend = GetComponent<Renderer>();
            _startColor = _rend.material.color;
        
            // Needs to be instanciate in Start() else it will be null
            _buildManager = BuildManager.instance;
        }

        public Vector3 GetBuildPosition()
        {
            return transform.position + turretOffset;
        }

        private void OnMouseEnter()
        {
            if (turret) return;
            
            if (EventSystem.current.IsPointerOverGameObject() || !(_buildManager.CanBuild && _buildManager.HasMoney))
                return;
        
            _rend.material.color = hoverColor;
            
            _previewTurret = Instantiate(_buildManager.TurretToBuild.prefab, GetBuildPosition(), Quaternion.identity);
            _previewTurret.GetComponent<Turret>().allowedToShoot = false;
        }

        private void OnMouseExit()
        {
            _rend.material.color = _startColor;
            
            RemovePreviewTurret();
        }

        private void OnMouseUpAsButton()
        {
            if (EventSystem.current.IsPointerOverGameObject() || !_buildManager.CanBuild)
                return;
        
            if (turret != null)
            {
                return;
            }

            _rend.material.color = _startColor;
            
            RemovePreviewTurret();
            _buildManager.BuildTurretOn(this);
        }

        private void RemovePreviewTurret()
        {
            if (_previewTurret)
            {
                Destroy(_previewTurret.gameObject);
                _previewTurret = null;
            }
        }
    }
}
