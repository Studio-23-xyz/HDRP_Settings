using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


namespace SettingMenu
{
    public class SettingsMenuUIController : MonoBehaviour
    {
        private SettingsManager _settingsManager;


        [SerializeField] private Dropdown fullScreenModeDropdown;
        [SerializeField] private Dropdown screenResolutionDropdown;
        [SerializeField] private Toggle vSyncToggle;
        [SerializeField] private Slider brightnessSlider;
        [SerializeField] private Slider fovSlider;
        [SerializeField] private Slider dpiSlider;
        [SerializeField] private Dropdown qualityDropdown;
        [SerializeField] private Dropdown textureQualityDropdown;
        [SerializeField] private Slider overviewSlider;
        [SerializeField] private Dropdown shadowQualityDropdown;

        [SerializeField] private Toggle volumetricLightToggle;
        [SerializeField] private Toggle bloomEffectToggle;
        [SerializeField] private Toggle vignetteEffectToggle;
        [SerializeField] private Toggle ambientOcclusion;


        [SerializeField] private Button refreshButton;
        [SerializeField] private Button restoreDefaultButton;
        [SerializeField] private Button applyButton;

        [SerializeField] private TMP_InputField status;

        private Dictionary<SettingsName, dynamic> Settings;

        private void OnEnable()
        {
            Settings = new Dictionary<SettingsName, dynamic>();
        }

        void Start()
        {
            _settingsManager = SettingsManager.Instance;
            Initialized();
            LoadSettingsSaveValue();

            fullScreenModeDropdown.onValueChanged.AddListener((value) => SetSettings(SettingsName.FullScreenMode, value));
            screenResolutionDropdown.onValueChanged.AddListener((value) => SetSettings(SettingsName.ScreenResolution, value));

            brightnessSlider.onValueChanged.AddListener((value) => SetSettings(SettingsName.Brightness, value));
            fovSlider.onValueChanged.AddListener((value) => SetSettings(SettingsName.FOV, value));
            dpiSlider.onValueChanged.AddListener((value) => SetSettings(SettingsName.DPI, value));
            qualityDropdown.onValueChanged.AddListener((value) => SetSettings(SettingsName.Quality, value));
            textureQualityDropdown.onValueChanged.AddListener((value) => SetSettings(SettingsName.TextureQuality, value));
            shadowQualityDropdown.onValueChanged.AddListener((value) => SetSettings(SettingsName.ShadowQuality, value));


            vSyncToggle.onValueChanged.AddListener((value) => SetSettings(SettingsName.vSync, value));
            volumetricLightToggle.onValueChanged.AddListener((value) => SetSettings(SettingsName.VolumetricLight, value));
            bloomEffectToggle.onValueChanged.AddListener((value) => SetSettings(SettingsName.BloomEffect, value));
            vignetteEffectToggle.onValueChanged.AddListener((value) => SetSettings(SettingsName.VignetteEffect, value));
            ambientOcclusion.onValueChanged.AddListener((value) => SetSettings(SettingsName.AmbientOcclusion, value));

            applyButton.onClick.AddListener(AppendSettings);
            refreshButton.onClick.AddListener(RefreshSettings);
            restoreDefaultButton.onClick.AddListener(RestoreDefaultSettings);

        }
/*
        private void SetToggleValue(SettingsName settingsName, bool value)
        {
            if (settingsName == SettingsName.vSync) SetSettings(SettingsName.vSync, value ? 1 : 0);
            else SetSettings(settingsName, value);
        }

        private void SetShadowQuality(int value)
        {
            SetSettings(SettingsName.ShadowQuality, value);
        }

        private void SetTextureQuality(int value)
        {
            SetSettings(SettingsName.TextureQuality, value);
        }

        private void SetQuality(int value) => SetSettings(SettingsName.Quality, value);

        private void SetDPI(float value) => SetSettings(SettingsName.DPI, value);

       

        private void SetBrightness(float value) => SetSettings(SettingsName.Brightness, value);
        private void SetVSync(bool value) => SetSettings(SettingsName.vSync, value ? 1 : 0);
        private void SetScreenResolution(int value) => SetSettings(SettingsName.ScreenResolution, value);
        private void SetFullScreenMode(int value) => SetSettings(SettingsName.FullScreenMode, value);
 */
        public void SetSettings(SettingsName settingsName, dynamic obj)
        {

            /*if (settingsName == SettingsName.FOV)
            {
                _settingsManager.Apply(new Dictionary<SettingsName, dynamic> {{SettingsName.FOV, obj}});
                float.TryParse(Convert.ToString(obj), out float val);
                overviewSlider.value = val;
            }
            else if (settingsName == SettingsName.DPI)
            {
                _settingsManager.Apply(new Dictionary<SettingsName, dynamic> {{SettingsName.DPI, obj}});
                float.TryParse(Convert.ToString(obj), out float val);
                //overviewSlider.value =  val;
            }
            else if (settingsName == SettingsName.Brightness)
            {
                _settingsManager.Apply(new Dictionary<SettingsName, dynamic> {{SettingsName.Brightness, obj}});
                float.TryParse(Convert.ToString(obj), out float val);
                //overviewSlider.value =  val;
            }*/

            Settings[settingsName] = obj;
            
           // if (settingsName == SettingsName.vSync) SetSettings(SettingsName.vSync, value ? 1 : 0);
           // else SetSettings(settingsName, value);
        }

        #region CRUD_SETTINGS

        private void AppendSettings()
        {
            _settingsManager.Append(Settings);
            _settingsManager.Apply(Settings);

            LoadSettingsSaveValue();

            string newText = "\n ** <b>Appended Data </b> ** \n";
            foreach (var pair in Settings) newText += $"{pair.Key}: {pair.Value} \n";
            newText += "\n ** <b>Device</b> ** \n";
            newText += _settingsManager.DeviceSettings();
            ShowStatus(newText);
            Settings.Clear();
        }

        private void RefreshSettings()
        {
            _settingsManager.Apply(_settingsManager.Settings());
            LoadSettingsSaveValue();

            string newText = "\n ** <b>Refresh Database</b> ** \n";
            foreach (var pair in _settingsManager.Settings()) newText += $"{pair.Key}: {pair.Value} \n";
            newText += "\n ** <b>Device</b> ** \n";
            newText += _settingsManager.DeviceSettings();
            ShowStatus(newText);

        }

        private void RestoreDefaultSettings()
        {
            _settingsManager.SaveDefaultSettings();
            _settingsManager.Apply(_settingsManager.Settings());
            LoadSettingsSaveValue();

            string newText = "\n ** <b>Restored Data</b> ** \n";
            foreach (var pair in _settingsManager.Settings()) newText += $"{pair.Key}: {pair.Value} \n";
            newText += "\n ** <b>Device</b> ** \n";
            newText += _settingsManager.DeviceSettings();
            ShowStatus(newText);

        }

        private void ShowStatus(string newText)
        {
            var oldText = $"<color=grey>{status.text}</color>";
            status.text = "";
            status.text = $"{newText} \n {oldText}";
        }

        #endregion


        private void Initialized()
        {
            /*LOAD SCREEN TYPE IN DROPDOWN Fullscreen Window, Exclusive Fullscreen, Windowed*/
            fullScreenModeDropdown.ClearOptions();
            foreach (FullScreenMode fullScreenMode in _settingsManager.listOfFullScreenMode)
            {
                string newVal = Regex.Replace(fullScreenMode.ToString(), "([a-z])([A-Z])", "$1 $2");
                Dropdown.OptionData option = new Dropdown.OptionData(newVal);
                fullScreenModeDropdown.options.Add(option);
                /*fullScreenModeDropdown.value = fullScreenMode;
                fullScreenModeDropdown.RefreshShownValue();*/
            }

            /*LOAD SCREEN RESOLUTION*/
            screenResolutionDropdown.ClearOptions();
            foreach (var resolution in _settingsManager.listOfResolutions)
            {
                screenResolutionDropdown.options.Add(new Dropdown.OptionData(resolution.ToString()));
            }

            qualityDropdown.ClearOptions();
            qualityDropdown.AddOptions(QualitySettings.names.ToList());

            textureQualityDropdown.ClearOptions();
            textureQualityDropdown.AddOptions(_settingsManager.TextureQualities.ToList());

            shadowQualityDropdown.ClearOptions();
            shadowQualityDropdown.AddOptions(Enum.GetNames(typeof(ShadowQuality)).ToList());


        }

        private void LoadSettingsSaveValue()
        {
            /*SELECT CURRENT SELECTION*/
            Dictionary<SettingsName, dynamic> settings = _settingsManager.Settings();
            if (settings == null || settings.Count == 0)
            {
                return;
            }
           
            /*fullScreenModeDropdown.value = settings.FirstOrDefault(n=>n.Key == SettingsName.FullScreenMode).Value; 
            fullScreenModeDropdown.RefreshShownValue();*/
             
            
            
            
            foreach (var setting in settings)
            {
                GetValue(setting);
            }
        }

        
        private void GetValue(KeyValuePair<SettingsName, dynamic> setting)
        {
            /*switch (setting.Key)
                {
                    case SettingsName.FullScreenMode:
                        int.TryParse(Convert.ToString(setting.Value), out int fullScreenMode);
                        fullScreenModeDropdown.value = fullScreenMode;
                        fullScreenModeDropdown.RefreshShownValue();
                        break;
                    case SettingsName.ScreenResolution:
                        // Resolution resolution = JsonConvert.DeserializeObject<Resolution>(setting.Value.ToString());
                        //Debug.Log($"ScreenResolution: {setting.Key} with value: {ScreenResolution.height}");
                        // List<string> listAvailableStrings = screenResolutionDropdown.options.Select(option => option.text).ToList();
                        // screenResolutionDropdown.value = listOfResolutions.IndexOf(resolution);
                        int.TryParse(Convert.ToString(setting.Value), out int screenResolution);
                        screenResolutionDropdown.value = screenResolution;
                        screenResolutionDropdown.RefreshShownValue();
                        break;
                    case SettingsName.vSync:
                        bool.TryParse(Convert.ToString(setting.Value), out bool vSync);
                        vSyncToggle.isOn = vSync; //? true: false;
                        break;
                    case SettingsName.Brightness:
                        float.TryParse(Convert.ToString(setting.Value), out float brightness);
                        brightnessSlider.value = Mathf.Clamp01(brightness); // float : 0 - 1
                        break;
                    case SettingsName.FOV:
                        float.TryParse(Convert.ToString(setting.Value), out float fov);
                        if (fovSlider) fovSlider.value = Mathf.Clamp01(fov); // float : 0 - 1, 
                        break;
                    case SettingsName.DPI:
                        float.TryParse(Convert.ToString(setting.Value), out float dpi);
                        if (fovSlider) dpiSlider.value = Mathf.Clamp01(dpi);
                        break;
                    case SettingsName.Quality:
                        int.TryParse(Convert.ToString(setting.Value), out int quality);
                        qualityDropdown.value = quality;
                        break;
                    case SettingsName.TextureQuality:
                        int.TryParse(Convert.ToString(setting.Value), out int textureQuality);
                        QualitySettings.masterTextureLimit = textureQuality; //0-3
                        break;
                    case SettingsName.ShadowQuality:
                        int.TryParse(Convert.ToString(setting.Value), out int shadowQuality);
                        QualitySettings.masterTextureLimit = shadowQuality; //0-3
                        break;
                    case SettingsName.VolumetricLight:
                        bool.TryParse(Convert.ToString(setting.Value), out bool volumetricLight);
                        volumetricLightToggle.isOn = volumetricLight; //? true: false;
                        break;
                    case SettingsName.BloomEffect:
                        bool.TryParse(Convert.ToString(setting.Value), out bool bloomEffect);
                        volumetricLightToggle.isOn = bloomEffect; //? true: false;
                        break;
                    case SettingsName.VignetteEffect:
                        bool.TryParse(Convert.ToString(setting.Value), out bool vignetteEffect);
                        volumetricLightToggle.isOn = vignetteEffect; //? true: false;
                        break;
                    case SettingsName.AmbientOcclusion:
                        bool.TryParse(Convert.ToString(setting.Value), out bool ambientOcclusion);
                        volumetricLightToggle.isOn = ambientOcclusion; //? true: false;
                        break;
                        
                    default:
                        Debug.LogError(
                            $"Unknown setting key: {setting.Key} with value: {setting.Value}/{setting.GetType()}");
                        break;
                }*/
            
        }

    }

   
}