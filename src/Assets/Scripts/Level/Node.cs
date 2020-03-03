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
            if (EventSystem.current.IsPointerOverGameObject() || !(_buildManager.CanBuild && _buildManager.HasMoney))
                return;
        
            _rend.material.color = hoverColor;
        }

        private void OnMouseExit()
        {
            _rend.material.color = _startColor;
        }

        private void OnMouseUpAsButton()
        {
            if (EventSystem.current.IsPointerOverGameObject() || !_buildManager.CanBuild)
                return;
        
            if (turret != null)
            {
                Debug.LogError("Can't build there! - TODO: Display on screen.");
                return;
            }
        
            _buildManager.BuildTurretOn(this);
        }
    }
}
