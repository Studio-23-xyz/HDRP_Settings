using System;
using System.Linq;
using Cinemachine;
using com.studio23.ss2.Core;
using com.studio23.ss2.Core.Component;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.HighDefinition;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace GameSettings
{
    [RequireComponent(typeof(Toggle))]
    public class AmbientOcclusionSettings : Settings
    {
       
        private VideoSettingsController _videoSettingsController;
        [SerializeField] private Toggle uiItem;
        
        [SerializeField] private bool defaultVal = true; 
        private VolumeProfile data;
         private ScreenSpaceAmbientOcclusion component;
      
         private void OnEnable()
         {
             _videoSettingsController = FindObjectOfType<VideoSettingsController>();
             _videoSettingsController.ApplyAction += ApplyAction;
             _videoSettingsController.RestoreAction += RestoreAction;
            
             _videoSettingsController.QualityChangedAction += QualityChangedAction;
         }

         private void OnDisable()
         {
             _videoSettingsController.ApplyAction -= ApplyAction;
             _videoSettingsController.RestoreAction -= RestoreAction;
            
             _videoSettingsController.QualityChangedAction += QualityChangedAction;
         }

         private void QualityChangedAction(QualityName qualityName)
         {
             var setting = _videoSettingsController.QualitySettingsPreset.FirstOrDefault(x => x.names == qualityName);
             if (setting != null)
             {
                
                 uiItem.isOn = setting.ambientOcclusion;;
             }
         }
        public override void Setup()
        {
            data = FindObjectsOfType<Volume>().OrderBy(m => m.transform.GetSiblingIndex()).ToArray()[0].sharedProfile; 
            data.TryGet(typeof(ScreenSpaceAmbientOcclusion), out component);
             
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
           component.active = currentValue.ToBool();
        }

        
    }
    
  
    
}