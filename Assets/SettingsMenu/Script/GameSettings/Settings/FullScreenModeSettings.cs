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
        private List<FullScreenMode> options { get; set; } = new();
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

        public override void Setup(string dbName)
        {
            if (!options.Any()) GenerateOptions();
            base.Initialized((int)defaultVal, dbName);
            Apply();
        }

        private void Start()
        {
            if (!options.Any()) return;
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
            var setting = options[currentValue.ToInt()];
            Screen.fullScreenMode = setting;
        }

        private void GenerateOptions()
        {
            options = new List<FullScreenMode>();
            var x = 0;
            foreach (FullScreenMode fullScreenMode in Enum.GetValues(typeof(FullScreenMode)))
            {
                if (x != 2) options.Add(fullScreenMode);
                x++;
            }

           
        }
        private List<TMP_Dropdown.OptionData> GetOptions()
        {
            

            return options.Select(x => Regex.Replace(x.ToString(), "([a-z])([A-Z])", "$1 $2"))
                .Select(newVal => new TMP_Dropdown.OptionData(newVal)).ToList();
        }
        
        public FullScreenMode Get()
        {
            if (!options.Any()) GenerateOptions();
            return options[currentValue.ToInt()];
        }
    }
    
  
    
}