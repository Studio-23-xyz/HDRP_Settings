using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;



namespace GameSettings
{
    public class SettingsManager : MonoBehaviour
    {

        [SerializeField] private MasterVolumeSettings masterVolumeSettings;
        [SerializeField] private SFXVolumeSettings sfxVolumeSettings;
        [SerializeField] private MusicVolumeSettings musicVolumeSettings;
        [SerializeField] private FullScreenModeSettings fullScreenModeSettings;
        [SerializeField] private ScreenResolutionSettings screenResolutionSettings;
        [SerializeField] private FovSettings fovSettings;
        [SerializeField] private GameQualitySettings gameQualitySettings;
        [SerializeField] private TextureQualitySettings textureQualitySettings;
        [SerializeField] private ShadowQualitySettings shadowQualitySettings;
        [SerializeField] private VSyncSettings vSyncSettings;
        [SerializeField] private VolumetricLightSettings volumetricLightSettings;
        [SerializeField] private BloomSettings bloomSettings;
        [SerializeField] private VignetteSettings vignetteSettings;
        [SerializeField] private AmbientOcclusionSettings ambientOcclusionSettings;
        [SerializeField] private BrightnessSettings brightnessSettings;
        
        [SerializeField] private TMP_Text statusText;
        public List<Settings> allSettings;
       private void Awake()=>Initialized();

       private void Initialized()
       {
          
           /*Type[] types = System.Reflection.Assembly.GetExecutingAssembly().GetTypes();
          _allSettings = (from Type type in types where type.IsSubclassOf(typeof(Settings)) select type).ToList();*/
           
           /*foreach (Type tp in _allSettings)
           {
               Debug.Log(tp);
               MethodInfo method = tp.GetMethod("Setup");
               if (method != null)
               {
                   object instance = Activator.CreateInstance(tp);
                   method.Invoke(instance, null);
               }
              
           }*/
           
           
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
            volumetricLightSettings.Setup();
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
