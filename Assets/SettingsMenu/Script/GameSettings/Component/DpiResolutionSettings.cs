using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using Cinemachine;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering.HighDefinition;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace GameSettings
{
    [RequireComponent(typeof(Slider))]
    public class DpiResolutionSettings : Settings, ISettings
    {
       
        private SettingsUIManager _settingsUIManager;
        private Slider uiItem;
        [SerializeField] private float minValue = 0; 
        [SerializeField] private float maxValue = 1; 
        [SerializeField] private float defaultVal = 1; 
        
        private CinemachineVirtualCamera virtualCamera;
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
            uiItem = GetComponent<Slider>();
            virtualCamera = FindObjectOfType<CinemachineVirtualCamera>();

            defaultValue = defaultVal;
            
            base.Awake();
            
            uiItem.Init(minValue, maxValue, currentValue.ToFloat());
            
           
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
            QualitySettings.resolutionScalingFixedDPIFactor = Mathf.Clamp01(currentValue.ToFloat()); // default 1, 0-1
        }

        
    }
    
  
    
}