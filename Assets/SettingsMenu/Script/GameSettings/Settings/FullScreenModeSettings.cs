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
    public class FullScreenModeSettings : Settings
    {
        private List<FullScreenMode> settings { get; set; }
        private VideoSettingsController _videoSettingsController;
        [SerializeField] private TMP_Dropdown uiItem;
        
        [SerializeField] private FullScreenMode defaultVal;
        private void OnEnable()
        {
            _videoSettingsController = FindObjectOfType<VideoSettingsController>();
            _videoSettingsController.ApplyAction += ApplyAction;
            _videoSettingsController.RestoreAction += RestoreAction;
        }

        private void OnDisable()
        {
            _videoSettingsController.ApplyAction -= ApplyAction;
            _videoSettingsController.RestoreAction -= RestoreAction;
        }

        public override void Setup()
        {
            
             
           
            
            uiItem.AddOptionNew(GenerateOptions());
           
           
            base.Initialized((int)defaultVal);
            
            uiItem.value =  currentValue.ToInt();
            Apply();
        }

        private void Start()
        {
           
           
            uiItem.onValueChanged.AddListener((value) =>
            {
                currentValue = value;
                if(isLive) Apply();
            });
        }

        private void RestoreAction()
        {
            uiItem.value = (int)defaultVal; // on change currentValue will be changed
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
            Screen.fullScreenMode = setting;
        }

        private List<TMP_Dropdown.OptionData> GenerateOptions()
        {
            settings = new List<FullScreenMode>();
            var x = 0;
            foreach (FullScreenMode fullScreenMode in Enum.GetValues(typeof(FullScreenMode)))
            {
                if (x != 2) settings.Add(fullScreenMode);
                x++;
            }

            return settings.Select(x => Regex.Replace(x.ToString(), "([a-z])([A-Z])", "$1 $2"))
                .Select(newVal => new TMP_Dropdown.OptionData(newVal)).ToList();
        }

        public FullScreenMode Get()
        {
            return settings[currentValue.ToInt()];
        }
    }
    
  
    
}