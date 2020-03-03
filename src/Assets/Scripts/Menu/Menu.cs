using UnityEngine;
using UnityEngine.SceneManagement;

namespace Menu
{
    public class Menu : MonoBehaviour
    {
        public void StartGame()
        {
            SceneManager.LoadScene("Level");
        }

        public void Settings()
        {
            SceneManager.LoadScene("Settings");
        }

        public void QuitGame()
        {
            Application.Quit();
        }
    }
}
