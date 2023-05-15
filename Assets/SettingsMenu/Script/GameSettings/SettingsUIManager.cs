using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace GameSettings
{
    public class SettingsUIManager : MonoBehaviour
    {

       public List<QualitySetting> QualitySettings;

        public Action<QualityName> QualityChangedAction;

        public Action ApplyAction;
        public Action RestoreAction;

        [SerializeField] private Button applyButton;
        [SerializeField] private Button restoreButton;
        [SerializeField] private TMP_Text statusText;

        [SerializeField] private FullScreenModeSettings fullScreenModeSettings;
        [SerializeField] private ScreenResolutionSettings screenResolutionSettings;
        [SerializeField] private GameQualitySettings gameQualitySettings;
        [SerializeField] private TextureQualitySettings textureQualitySettings;
        [SerializeField] private ShadowQualitySettings shadowQualitySettings;
        [SerializeField] private FovSettings fovSettings;
        [SerializeField] private DpiResolutionSettings dpiResolutionSettings;


        private void Start()
        {

            applyButton.onClick.AddListener(ApplyAction.Invoke);
            restoreButton.onClick.AddListener(RestoreAction.Invoke);

            fullScreenModeSettings.Apply();
            screenResolutionSettings.Apply();
            gameQualitySettings.Apply();
            textureQualitySettings.Apply();
            shadowQualitySettings.Apply();
            fovSettings.Apply();
            dpiResolutionSettings.Apply();

            statusText.text = DeviceSettings();

        }

        public string DeviceSettings()
        {
            string output = null;
            output += $"GetQualityLevel: {UnityEngine.QualitySettings.GetQualityLevel()} \n";
            output += $"masterTextureLimit: {UnityEngine.QualitySettings.masterTextureLimit.ToString()} \n";
            output += $"Shadow: {UnityEngine.QualitySettings.shadows} \n";
            return output;
        }


       
    }
    
    [Serializable]
    public class QualitySetting
    {
        public QualityName names;
        public TextureQuality textureQuality;
        public ShadowQuality shadowQuality;
        public bool vSyncCount;
    }

    [Serializable]
    public enum QualityName
    {
        High,
        Medium,
        Low,
    }

    [Serializable]
    public enum ShadowQuality
    {
        Low,
        Medium,
        High,
        Ultra
    }

    [Serializable]
    public enum TextureQuality
    {
        High,
        Medium,
        Low,
        Very_Low
    }
    
}
/*
 High:
    Texture Full
    Shadow High
    vSync 1
 Medium:
    Texture Half
    Shadow Medium
    vSync 0
  Low:
    Texture Quarter
    Shadow Low
    vSync 0
 */

 

    
 