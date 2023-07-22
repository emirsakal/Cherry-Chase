using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class optionsMenu : MonoBehaviour
{   
    public Dropdown resolutionDropDown;
    Resolution[] resolutions;
    public Toggle trailEffectToggle;
    public Dropdown languageDropdown;
    
    void Start() {
        resolutions = Screen.resolutions;

        resolutionDropDown.ClearOptions();

        List<string> options = new List<string>(); 

        int currentResolutionIndex = 0;
        for (int i = 0; i < resolutions.Length; i++){
            string option = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(option);

            if(resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height) {
                currentResolutionIndex = i;
            }
        } 

        resolutionDropDown.AddOptions(options);
        resolutionDropDown.value = currentResolutionIndex;
        resolutionDropDown.RefreshShownValue();

        languageDropdown.value = PlayerPrefs.GetInt("Language", 0);
        languageDropdown.RefreshShownValue();

        bool isTrailOn = (PlayerPrefs.GetInt("isTrailEffectOn") == 1) ? true : false;
        trailEffectToggle.isOn = isTrailOn;
    }

    public void SetFullScreen(bool isFullscreen) {
        Screen.fullScreen = isFullscreen;
    }

    public void SetResolution (int resolutionIndex) {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution (resolution.width, resolution.height, Screen.fullScreen);
    }

    public void SetTrailEffect (bool isTrailEffect) {
        if(isTrailEffect){
            PlayerPrefs.SetInt("isTrailEffectOn", 1);   
        } else {
            PlayerPrefs.SetInt("isTrailEffectOn", 0);
        }
    }

    public void SetLanguage(int index) {
        switch (index) {
            case 0: PlayerPrefs.SetInt("Language", 0); break;
            case 1: PlayerPrefs.SetInt("Language", 1); break;
        }
    }
}
