using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Level
{
    public class PauseController : MonoBehaviour
    {
        [SerializeField] private GameObject pauseMenu;
        private bool _isPaused;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
                TogglePauseMenu();

            if (_isPaused && pauseMenu.activeInHierarchy || !_isPaused && !pauseMenu.activeInHierarchy) return;
            
            Time.timeScale = Convert.ToSingle(!_isPaused);
            AudioListener.pause = _isPaused;
            pauseMenu.SetActive(_isPaused);
        }
        
        public void TogglePauseMenu()
        {
            _isPaused = !_isPaused;
        }

        public void RestartLevel()
        {
            TogglePauseMenu();
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        
        public void BackToMenu()
        {
            TogglePauseMenu();
            SceneManager.LoadScene("Menu");
        }
        
        public void QuietGame()
        {
            Application.Quit();
        }
    }
}