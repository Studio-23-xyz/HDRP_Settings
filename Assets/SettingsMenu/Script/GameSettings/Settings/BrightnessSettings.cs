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
    public class BrightnessSettings : Settings
    {
       
        private VideoSettingsController _videoSettingsController;
        [SerializeField] private Slider uiItem;
        
        private VolumeProfile data;
        private ColorAdjustments component;
        
        [SerializeField] private float minVal = -3f; 
        [SerializeField] private float maxVal = 1f; 
         
        [SerializeField] private float defaultVal = -1f; 
        [SerializeField] private TMP_Text label;
        
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
            data = FindObjectsOfType<Volume>().OrderBy(m => m.transform.GetSiblingIndex()).ToArray()[0].sharedProfile; //FindObjectOfType<Volume>();
            data.TryGet(typeof(ColorAdjustments), out component);
            
           
           
            base.Initialized(defaultVal);
            
           
            
            Apply();
            
        }

        private void Start()
        {
           
            uiItem.Init(minVal, maxVal, currentValue.ToFloat());
            
            label.text = FloatToText(defaultVal);
           
            uiItem.onValueChanged.AddListener((value) =>
            {
                currentValue = value;
                if(isLive) Apply();
                label.text = FloatToText(value);
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

            component.postExposure.value = Mathf.Clamp(currentValue.ToFloat(),minVal,maxVal) ;
        }

        
    }
    
  
    
}