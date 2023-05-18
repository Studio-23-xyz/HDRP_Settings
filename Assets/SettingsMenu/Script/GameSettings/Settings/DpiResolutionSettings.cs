﻿using UnityEngine;
using UnityEngine.UI;

namespace GameSettings
{
    [RequireComponent(typeof(Slider))]
    public class DpiResolutionSettings : Settings, ISettings
    {
       
        private VideoSettingsController _videoSettingsController;
        private Slider uiItem;
        [SerializeField] private float minValue = 0; 
        [SerializeField] private float maxValue = 1; 
        [SerializeField] private float defaultVal = 1; 
        
        
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

        public override void Awake()
        {
            uiItem = GetComponent<Slider>();
           

            defaultValue = defaultVal;
            base.Awake();
            
            uiItem.Init(minValue, maxValue, currentValue.ToFloat());
            
           
            uiItem.onValueChanged.AddListener((value) =>
            {
                currentValue = value;
                if(isLive) Apply();
            });
            Apply();
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