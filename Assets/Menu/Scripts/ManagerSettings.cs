using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

class ManagerSettings : MonoBehaviour
{
    [SerializeField]
    TMP_Dropdown difficultyLevel;
    [SerializeField]
    TMP_Dropdown qualityLevelDropdown;
    [SerializeField]
    TMP_Dropdown resolutionDropdown;
    [SerializeField]
    Toggle fullScreen;
    [SerializeField]
    Slider slider;

    [SerializeField]
    AudioMixer audioMixer;

    Resolution[] resolutions;

    void OnEnable()
    {
        DirectoryInfo dir = new DirectoryInfo(Application.persistentDataPath);
        FileInfo[] files = dir.GetFiles("settings.survive");

        if (files.Length == 1)
        {
            DataSettings data = SaveSettings.LoadSetting();

            difficultyLevel.value = data.difficultyLevel;
            slider.value = data.volume;

            Manager.SetDiffcultyLevel(data.difficultyLevel);
            audioMixer.SetFloat("sound", data.volume);
        }
    }

    void OnDisable()
    {
        SaveSettings.SaveSetting(difficultyLevel.value, slider.value);
    }

    void Start()
    {
        QualityLevel();
        Resolution();
        FullScreen();
    }

    public void SetDifficultyLevel(int difficultyIndex)
    {
        Manager.SetDiffcultyLevel(difficultyIndex);
    }

    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }

    public void SetResolution (int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    public void SetFullScreen(bool isFullScreen)
    {
        Screen.fullScreen = isFullScreen;
    }

    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("sound", volume);
    }

    void QualityLevel()
    {
        qualityLevelDropdown.value = QualitySettings.GetQualityLevel();
    }

    void Resolution()
    {
        resolutions = Screen.resolutions;

        resolutionDropdown.ClearOptions();

        List<string> options = new List<string>();

        int currentResolutionIndex = 0;

        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(option);

            if (Screen.fullScreen)
            {
                if (resolutions[i].width == Screen.currentResolution.width
                    && resolutions[i].height == Screen.currentResolution.height)
                {
                    currentResolutionIndex = i;
                }
            }
            else
            {
                if (resolutions[i].width == Screen.width
                    && resolutions[i].height == Screen.height)
                {
                    currentResolutionIndex = i;
                }

            }
        }

        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();
    }

    void FullScreen()
    {
        fullScreen.isOn = Screen.fullScreen;
    }
}
