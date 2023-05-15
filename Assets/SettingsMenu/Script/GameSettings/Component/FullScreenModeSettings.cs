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
    public class FullScreenModeSettings : Settings, ISettings
    {
        private List<FullScreenMode> settings { get; set; }
        private SettingsUIManager _settingsUIManager;
        private TMP_Dropdown dropdown;
        private void OnEnable()
        {
            _settingsUIManager = FindObjectOfType<SettingsUIManager>();
            _settingsUIManager.ApplyAction += ApplyAction;
            _settingsUIManager.RestoreAction += RestoreAction;
        }

        private void OnDisable()
        {
            _settingsUIManager.ApplyAction -= ApplyAction;
            _settingsUIManager.RestoreAction -= RestoreAction;
        }

        public override void Awake()
        {
            dropdown = GetComponent<TMP_Dropdown>();
            dropdown.AddOptionNew(GenerateOptions());
            
            base.Awake();
            
            dropdown.value =  currentValue.ToInt();
           
            dropdown.onValueChanged.AddListener((value) =>
            {
                currentValue = value;
                if(isLive) Apply();
            });
        }
        private void RestoreAction()
        {
            dropdown.value = defaultValue.ToInt(); // on change currentValue will be changed
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