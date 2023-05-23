using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
 
using System.Reflection;

namespace GameSettings
{
    public class SettingsManager : MonoBehaviour
    {
       
        
        [SerializeField] private TMP_Text statusText;


        
       
          public static SettingsManager InstanceX;
          [Header("Audio Settings")]
        [SerializeField] private MasterVolumeSettings masterVolumeSettings;
        [SerializeField] private SFXVolumeSettings sfxVolumeSettings;
         [SerializeField] private MusicVolumeSettings musicVolumeSettings;
        [Header("Video Settings")]
        [SerializeField] private FullScreenModeSettings fullScreenModeSettings;
        [SerializeField] private ScreenResolutionSettings screenResolutionSettings;
        [SerializeField] private FovSettings fovSettings;
       
      
        [SerializeField] private VSyncSettings vSyncSettings;
        [SerializeField] private VolumetricLightSettings bVolumetricLightSettings;
        [SerializeField] private BloomSettings bloomSettings;
        [SerializeField] private VignetteSettings vignetteSettings;
        [SerializeField] private AmbientOcclusionSettings ambientOcclusionSettings;
        
       [SerializeField] private GameQualitySettings gameQualitySettings;
       [SerializeField] private TextureQualitySettings textureQualitySettings;
       [SerializeField] private ShadowQualitySettings shadowQualitySettings;
       [SerializeField] private BrightnessSettings brightnessSettings;

       private void Awake()=>Initialized();

       private void Initialized()
        {
            
            
            masterVolumeSettings.Setup();
            sfxVolumeSettings.Setup();
            musicVolumeSettings.Setup();
            fullScreenModeSettings.Setup();
            screenResolutionSettings.Setup();
            fovSettings.Setup();
            gameQualitySettings.Setup();
            textureQualitySettings.Setup();
            shadowQualitySettings.Setup();
            vSyncSettings.Setup();
            bVolumetricLightSettings.Setup();
            bloomSettings.Setup();
            vignetteSettings.Setup();
            ambientOcclusionSettings.Setup();
            brightnessSettings.Setup(); 
        }
        
        /*private string DeviceSettings()
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
        }*/

        
        
    }
}
