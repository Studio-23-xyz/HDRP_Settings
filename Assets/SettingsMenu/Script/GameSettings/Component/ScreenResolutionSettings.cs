using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering.HighDefinition;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace GameSettings
{
    [RequireComponent(typeof(TMP_Dropdown))]
    public class ScreenResolutionSettings : Settings, ISettings
    {
        private List<Resolution> settings { get; set; }
        private SettingsUIManager _settingsUIManager;
        private TMP_Dropdown uiItem;
        
        private FullScreenModeSettings _fullScreenModeSettings;
        
        [SerializeField] private int defaultVal = 0;
        private void OnEnable()
        {
            _settingsUIManager = FindObjectOfType<SettingsUIManager>();
            _settingsUIManager.ApplyAction += ApplyAction;
            _settingsUIManager.RestoreAction += RestoreAction;
        }
        private void OnDisable()
        {
            _settingsUIManager.RestoreAction -= RestoreAction;
            _settingsUIManager.ApplyAction -= ApplyAction;
           
        }
        public override void Awake()
        {
            _fullScreenModeSettings = FindObjectOfType<FullScreenModeSettings>();
            uiItem = GetComponent<TMP_Dropdown>();
            uiItem.AddOptionNew(GenerateOptions());
           
            defaultValue = defaultVal;
            base.Awake();
           
          
            uiItem.value =  currentValue.ToInt();
            
            uiItem.onValueChanged.AddListener((value) =>
            {
                currentValue = value;
                if(isLive) Apply();
            });
        }
        private void RestoreAction()
        {
            uiItem.value = defaultValue.ToInt(); // on change currentValue will be changed
            base.Save();
            if(!isLive) Apply(); // if Live then already applied this
        }
        private void ApplyAction()
        {
            base.Save();
            if(!isLive) Apply();  // if Live then already applied this
        }
        public void Apply()
       {
           var setting = settings[currentValue.ToInt()];
           Screen.SetResolution(setting.width, setting.height, _fullScreenModeSettings.Get());
       }
       private  List<TMP_Dropdown.OptionData> GenerateOptions()
       {
           settings = new List<Resolution>();
        
           settings = new List<Resolution>();
           settings.AddRange(Screen.resolutions.ToList());
           settings.Reverse();
          

           return settings.Select(x => Regex.Replace(x.ToString(), "([a-z])([A-Z])", "$1 $2")).Select(newVal => new TMP_Dropdown.OptionData(newVal)).ToList();
       }
       
       public Resolution Get()
       {
           return settings[currentValue.ToInt()];
       }
       
    }
       
    

}