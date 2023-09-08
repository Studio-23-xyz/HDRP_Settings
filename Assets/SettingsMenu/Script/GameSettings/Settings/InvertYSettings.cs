﻿using System;
using System.Linq;
using Cinemachine;
using UnityEngine;
using UnityEngine.UI;

namespace GameSettings
{
    [RequireComponent(typeof(Toggle))]
    public class InvertYSettings : Settings
    {
       
        private VideoSettingsController _videoSettingsController;
        [SerializeField] private Toggle uiItem;
        
        [SerializeField] private bool defaultVal = true; 
        
      
        private void OnEnable()
        {
            _videoSettingsController = FindObjectOfType<VideoSettingsController>();
            _videoSettingsController.ApplyAction += ApplyAction;
            _videoSettingsController.RestoreAction += RestoreAction;

            _videoSettingsController.QualityChangedAction += QualityChangedAction;
        }

        private void QualityChangedAction(QualityName qualityName)
        {
            var setting = _videoSettingsController.QualitySettingsPreset.FirstOrDefault(x => x.names == qualityName);
            if (setting != null)
            {
                
                uiItem.isOn = setting.vSyncCount;;
            }
        }

        private void OnDisable()
        {
            _videoSettingsController.ApplyAction -= ApplyAction;
            _videoSettingsController.RestoreAction -= RestoreAction;

            _videoSettingsController.QualityChangedAction -= QualityChangedAction;
        }

        public override void Setup()
        {
            
            base.Initialized(defaultVal,GetType().Name);
           
            Apply();
        }

        private void Start()
        {
          
            uiItem.isOn = currentValue.ToBool();
            
            uiItem.onValueChanged.AddListener((value) =>
            {
                currentValue = value;
                if(isLive) Apply();
            });
        }

        private void RestoreAction()
        {
            uiItem.isOn = defaultVal; // on change currentValue will be changed
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
           var r = currentValue.ToBool() ? 1 : 0;  
           Debug.Log($"{GetType().Name}: {r}");
        } 

        
    }
    
  
    
}