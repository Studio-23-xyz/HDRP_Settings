using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace GameSettings
{
    public class VideoSettingsController : MonoBehaviour
    {

        public List<QualitySetting> QualitySettingsPreset;

        public static Action<QualityName> QualityChangedAction;
        public Action ApplyAction;
        public Action RestoreAction;

        [SerializeField] private Button applyButton;
        [SerializeField] private Button restoreButton;
       
       

       
        private void Start()
        {
            applyButton.onClick.AddListener(ApplyAction.Invoke);
            restoreButton.onClick.AddListener(RestoreAction.Invoke);

          

         
            
           SettingsManager.Instance.ShowStatus( DeviceSettings());
        }
        
      
        public string DeviceSettings()
        {
            string output = null;
            output += $"fullScreenMode: {Screen.fullScreenMode} \n";
            output += $"currentResolution: {Screen.currentResolution} \n";
            output += $"vSyncCount: {QualitySettings.vSyncCount} \n";
            // output += $"brightness: {_autoExposure.keyValue.value/4.0f} \n";
           // output += $"fov: {virtualCamera.m_Lens.FieldOfView} \n";
            output += $"dpi: {QualitySettings.resolutionScalingFixedDPIFactor} \n";
            output += $"GetQualityLevel: {QualitySettings.GetQualityLevel()} \n";
            output += $"masterTextureLimit: {QualitySettings.masterTextureLimit.ToString()} \n";
            output += $"Shadow: {QualitySettings.shadows} \n";
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
        public bool volumetricLight;
        public bool bloom;
        public bool vignette;
        public bool ambientOcclusion;
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

    
 