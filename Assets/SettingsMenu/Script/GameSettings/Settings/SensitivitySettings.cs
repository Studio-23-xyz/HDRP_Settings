using System;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.HighDefinition;
using UnityEngine.UI;

namespace GameSettings
{
    [RequireComponent(typeof(Slider))]
    public class SensitivitySettings : Settings
    {
       
        private VideoSettingsController _videoSettingsController;
        [SerializeField] private Slider uiItem;
        
      
        
        [SerializeField] private float minVal = 0.01f; 
        [SerializeField] private float maxVal =  5f; 
         
        [SerializeField] private float defaultVal = 0.1f; 
        [SerializeField] private TMP_Text label;
        
        /*private VolumeProfile data;
        private ColorAdjustments component;*/
        
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
            
            
           
           
            base.Initialized(defaultVal, GetType().Name);
            
           
            
            Apply();
            
        }

        private void Start()
        {
           
            uiItem.Init(minVal, maxVal, currentValue.ToFloat());
            
            label.text = FloatToText(defaultVal, gameObject.name);
           
            uiItem.onValueChanged.AddListener((value) =>
            {
                currentValue = value;
                if(isLive) Apply();
                label.text = FloatToText(value, gameObject.name);
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
            
            
            var r = currentValue.ToFloat();  
            Debug.Log($"{GetType().Name}: {r}");
            
            
        }

        
    }
    
  
    
}