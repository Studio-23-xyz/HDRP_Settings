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
    public class ScreenResolutionSettings : Settings
    {
        private List<Resolution> settings { get; set; }
        private VideoSettingsController _videoSettingsController;
        [SerializeField] private TMP_Dropdown uiItem;
        
        [SerializeField] private FullScreenModeSettings _fullScreenModeSettings;
        
        [SerializeField] private int defaultVal = 0;
        private void OnEnable()
        {
            _videoSettingsController = FindObjectOfType<VideoSettingsController>();
            _videoSettingsController.ApplyAction += ApplyAction;
            _videoSettingsController.RestoreAction += RestoreAction;
        }
        private void OnDisable()
        {
            _videoSettingsController.RestoreAction -= RestoreAction;
            _videoSettingsController.ApplyAction -= ApplyAction;
           
        }
        public override void Setup()
        {
            GenerateOptions();
            base.Initialized(defaultVal);
            Apply();
        }

        private void Start()
        {
            uiItem.AddOptionNew(GetOptions());
            uiItem.value =  currentValue.ToInt();
            
            uiItem.onValueChanged.AddListener((value) =>
            {
                currentValue = value;
                if(isLive) Apply();
            });
        }

        private void RestoreAction()
        {
            uiItem.value = defaultVal; // on change currentValue will be changed
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
       private  void GenerateOptions()
       {
           settings = new List<Resolution>();
           
           settings.AddRange(Screen.resolutions.ToList());
           settings.Reverse();
          

           
       }
       private  List<TMP_Dropdown.OptionData> GetOptions()
       {
           return settings.Select(x => Regex.Replace(x.ToString(), "([a-z])([A-Z])", "$1 $2")).Select(newVal => new TMP_Dropdown.OptionData(newVal)).ToList();
       }

      
       
    }
       
    

}