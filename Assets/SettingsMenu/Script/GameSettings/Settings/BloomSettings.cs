using System;
using System.Linq;
using Cinemachine;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.HighDefinition;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace GameSettings
{
    [RequireComponent(typeof(Toggle))]
    public class BloomSettings : Settings
    {
       
        private VideoSettingsController _videoSettingsController;
        [SerializeField] private Toggle uiItem;
        
        [SerializeField] private bool defaultVal = true; 
        
         private VolumeProfile data;
        private Bloom component;
    
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
            data.TryGet(typeof(Bloom), out component);
            
           
            
            base.Initialized(defaultVal);
            uiItem.isOn = currentValue.ToBool();
           
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