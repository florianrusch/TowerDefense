using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Settings
{
    public class Settings : MonoBehaviour
    {
        private Resolution[] _resolutions;

        public Toggle fullscreenToggle;
        public Dropdown resolutionDropdown;
        public Dropdown qualityDropdown;
        public Slider volumeSlider;
        
        private void Start()
        {
            _resolutions = Screen.resolutions;
            List<string> options = new List<string>();
            int currentResolutionIndex = 0;
            
            resolutionDropdown.ClearOptions();

            for (int i = 0; i < _resolutions.Length; i++)
            {
                string option = _resolutions[i].width + "x" + _resolutions[i].height + " (" + _resolutions[i].refreshRate +" FPS)";
                options.Add(option);

                if (Screen.currentResolution.Equals(_resolutions[i]))
                {
                    currentResolutionIndex = i;
                }
            }
            
            resolutionDropdown.AddOptions(options);
            resolutionDropdown.value = currentResolutionIndex;
            resolutionDropdown.RefreshShownValue();

            fullscreenToggle.isOn = Screen.fullScreen;
            
            qualityDropdown.value = QualitySettings.GetQualityLevel();
        }

        public void ApplySettings()
        {
            Screen.fullScreen = fullscreenToggle.isOn;
            
            QualitySettings.SetQualityLevel(qualityDropdown.value);
            
            Resolution resolution = _resolutions[resolutionDropdown.value];
            Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
        }

        public void GoBackToMenu()
        {
            SceneManager.LoadScene("Menu");
        }
    }
}