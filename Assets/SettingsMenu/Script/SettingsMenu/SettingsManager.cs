using System;
using UnityEngine;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using Cinemachine;
using UnityEngine.Serialization;
 

namespace SettingMenu
{public class SettingsManager : MonoBehaviour
    {
        public static SettingsManager Instance;
        /*public PostProcessProfile postProcessProfile;
        private AutoExposure _autoExposure;*/
        public List<FullScreenMode> listOfFullScreenMode { get; private set; }
        public List<Resolution> listOfResolutions { get; private set; }
        private CinemachineVirtualCamera virtualCamera;
        private string _path;
        private const string FileName = "settings.config";
        public string[] TextureQualities { get; private set; }

       
        private void Awake()
        {
            if (Instance == null)
                Instance = this;
            else
                Destroy(this);

 
            
            Initialized();
            if (!File.Exists(_path)) SaveDefaultSettings();
            if (Settings()?.Count > 0) Apply(Settings());
            
            // Debug.Log($"  QualitySettings.resolutionScalingFixedDPIFactor {  QualitySettings.resolutionScalingFixedDPIFactor }");
            
        }
      
        public void SaveDefaultSettings()
        {
            var defaultSettings = new Dictionary<SettingsName, dynamic>();
            defaultSettings[SettingsName.FullScreenMode] = 0;// Screen.fullScreenMode;
            defaultSettings[SettingsName.ScreenResolution] = 0;//Screen.currentResolution;
            defaultSettings[SettingsName.vSync] = 1;//QualitySettings.vSyncCount;
            defaultSettings[SettingsName.Brightness] = 0.5f; //default 2;
            defaultSettings[SettingsName.FOV] = 0f; //float60-120;  60 +(0-1)*60;
            defaultSettings[SettingsName.DPI] =  1; //float .25-1;
            defaultSettings[SettingsName.Quality] =  2; // int, 0-2, default 2;
            defaultSettings[SettingsName.TextureQuality] =  0; // int, 0-FullRes 1-4, default 3;
            defaultSettings[SettingsName.ShadowQuality] =  0; // 0 Disable Shadow;
            defaultSettings[SettingsName.VolumetricLight] = true;
            defaultSettings[SettingsName.BloomEffect] = true;
            defaultSettings[SettingsName.VignetteEffect] = true;
            defaultSettings[SettingsName.AmbientOcclusion] = true;
            // var screenRes = Screen.resolutions.Where(x => x.ToString() == Screen.currentResolution.ToString());
            // var screenRes =  new ScreenResolution(Screen.currentResolution.width,Screen.currentResolution.height,Screen.currentResolution.refreshRate);
            /*ScreenResolution s = new ScreenResolution();
            s.Height = Screen.currentResolution.height;
            s.Width = Screen.currentResolution.width;
            s.RefreshRate = Screen.currentResolution.refreshRate;*/
            // defaultSettings[SettingsName.AntiAliasing] = QualitySettings.antiAliasing;
           // defaultSettings[SettingsName.FullScreen] = Screen.fullScreen;
            Save(defaultSettings);
        }
        private void Initialized()
        {
            _path = Path.Combine(Application.persistentDataPath, FileName);// FileName;
            // load all screen mode in array
            listOfFullScreenMode = new List<FullScreenMode>();
            int x = 0;
            var refScreenMode = Enum.GetValues(typeof(FullScreenMode));
            foreach (FullScreenMode fullScreenMode in refScreenMode )
            {
                if( x!= 2)  listOfFullScreenMode.Add(fullScreenMode);
                x++;
            }
            
            /*LOAD RESOLUTION TYPE IN DROPDOWN*/
            listOfResolutions = new List<Resolution>();
            listOfResolutions.AddRange(Screen.resolutions.ToList());
            listOfResolutions.Reverse();
            
            virtualCamera = FindObjectOfType<CinemachineVirtualCamera>();
            TextureQualities  = new [] {"High", "Medium", "Low", "Very Low"};/*Full ,Half, Quarter, Eighth*/
            /*postProcessProfile = FindObjectOfType<PostProcessVolume>().profile;
            postProcessProfile.TryGetSettings(out _autoExposure);*/
        }
        public Dictionary<SettingsName, dynamic> Settings()
        {
            // LOAD SETTINGS
            Dictionary<SettingsName, dynamic> Settings = new Dictionary<SettingsName, dynamic>();
            var json = File.ReadAllText(_path);
            Settings = JsonConvert.DeserializeObject<Dictionary<SettingsName, dynamic>>(json);
            return Settings;
        }
        public void Save( Dictionary<SettingsName, dynamic> settings)
        {
            var contents = JsonConvert.SerializeObject(settings);
            File.WriteAllText(_path, contents);
        }
        public void Append(Dictionary<SettingsName, dynamic> settings)
        {
            Dictionary<SettingsName, dynamic> existingSettings = new Dictionary<SettingsName, dynamic>();

            // Read existing settings from the file
            if (File.Exists(_path))
            {
                string contents = File.ReadAllText(_path);
                existingSettings = JsonConvert.DeserializeObject<Dictionary<SettingsName, dynamic>>(contents);
            }

            // Merge new settings with existing settings
            foreach (var item in settings)
            {
                existingSettings[item.Key] = item.Value;
            }

            // Save the merged settings to the file
          Save(existingSettings);
        }
        public void Apply(Dictionary<SettingsName, dynamic> settings)
        {
            if (settings == null || settings.Count == 0)
            {
                return;
            }
             
            FullScreenMode fullScreenMode = Screen.fullScreenMode;
           // bool fullScreen = Screen.fullScreen;
            
            /*foreach (var setting in settings)
            {
                switch (setting.Key)
                {
                    case SettingsName.FullScreenMode:
                        int.TryParse(Convert.ToString(setting.Value),out int fullScreenModeIndex);
                        fullScreenMode = listOfFullScreenMode[fullScreenModeIndex];
                       // fullScreen =  fullScreenMode != FullScreenMode.Windowed;
                       //  Screen.fullScreen =  fullScreen;
                        Screen.fullScreenMode = fullScreenMode;
                        break;
                    case SettingsName.ScreenResolution:
                        int.TryParse(Convert.ToString(setting.Value), out int screenResolution);
                        Resolution selectedRes = listOfResolutions[screenResolution];
                        Screen.SetResolution(selectedRes.width, selectedRes.height, fullScreenMode);
                        break;
                    case SettingsName.vSync:
                        bool.TryParse(Convert.ToString(setting.Value), out bool vSync);
                        QualitySettings.vSyncCount = vSync ? 1 : 0; //? 0 dont, 1 every v blank;
                        break;
                    case SettingsName.Brightness:
                        //float.TryParse(Convert.ToString(setting.Value), out float brightness);
                       // _autoExposure.keyValue.value = Mathf.Clamp01(brightness) * 4f;// float : 0 - 1
                       // Screen.brightness = brightness * 4f;// float : 0 - 1
                        break;
                    case SettingsName.FOV:
                        float.TryParse(Convert.ToString(setting.Value), out float fov);
                        if(virtualCamera) virtualCamera.m_Lens.FieldOfView = 60f + Mathf.Clamp01(fov) * 60f; // float : 0 - 1, 60-120
                        break;
                    case SettingsName.DPI:
                        float.TryParse(Convert.ToString(setting.Value), out float dpi);
                        QualitySettings.resolutionScalingFixedDPIFactor = Mathf.Clamp01(dpi);
                        break;
                    case SettingsName.Quality:
                        int.TryParse(Convert.ToString(setting.Value), out int quality);
                        QualitySettings.SetQualityLevel(quality,true);
                        break;
                    case SettingsName.TextureQuality:
                        int.TryParse(Convert.ToString(setting.Value), out int textureQuality);
                        QualitySettings.masterTextureLimit = textureQuality; //0-3
                        break;
                    case SettingsName.ShadowQuality:
                        int.TryParse(Convert.ToString(setting.Value), out int shadowQuality);
                        QualitySettings.shadows = (ShadowQuality)shadowQuality;
                        break;
                    case SettingsName.VolumetricLight: 
                        bool.TryParse(Convert.ToString(setting.Value), out bool volumetricLight);
                        break;
                    case SettingsName.BloomEffect:
                        bool.TryParse(Convert.ToString(setting.Value), out bool bloomEffect);
                        break;
                    case SettingsName.VignetteEffect:
                        bool.TryParse(Convert.ToString(setting.Value), out bool vignetteEffect);
                        break;
                    case SettingsName.AmbientOcclusion:
                        bool.TryParse(Convert.ToString(setting.Value), out bool ambientOcclusion);
                        break;
                    default:
                        //  Screen.fullScreen = isFullScreen;
                       // QualitySettings.antiAliasing = Mathf.Clamp(antiAliasing * 2, 0, 8) ; // 0, 2, 4, 8
                        Debug.LogWarning($"Unknown setting key: {setting.Key}");
                        break;
                }
            }*/
        }
        public string DeviceSettings()
        {
            string output = null;
            output += $"fullScreenMode: {Screen.fullScreenMode} \n";
            output += $"currentResolution: {Screen.currentResolution} \n";
            output += $"vSyncCount: {QualitySettings.vSyncCount} \n";
           // output += $"brightness: {_autoExposure.keyValue.value/4.0f} \n";
            output += $"fov: {virtualCamera.m_Lens.FieldOfView} \n";
            output += $"dpi: {QualitySettings.resolutionScalingFixedDPIFactor} \n";
            output += $"GetQualityLevel: {QualitySettings.GetQualityLevel()} \n";
            output += $"masterTextureLimit: {QualitySettings.masterTextureLimit.ToString()} \n";
            output += $"Shadow: {QualitySettings.shadows} \n";
            return output;
        }
    }
    public enum SettingsName
    {
        FullScreenMode, // int
        ScreenResolution, // int
        vSync, // int 0 off, 1 on, default 1
        Brightness, // float : 0 - 1
        FOV, // float 60 - 120
        DPI, // float .25 - 2
        Quality, // int, 0-2
        TextureQuality, // 0 - fullRes, limit 0-3
        ShadowQuality, // Disable Shadows(default), Hard Shadow Only, Hard and Soft Shadow // 0-2
        VolumetricLight,
        BloomEffect,
        VignetteEffect,
        AmbientOcclusion,
        AntiAliasing, // int
       
    }
}