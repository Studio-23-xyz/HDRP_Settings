using System.Linq;
using Cinemachine;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.HighDefinition;
using UnityEngine.UI;

namespace GameSettings
{
    [RequireComponent(typeof(Toggle))]
    public class VignetteSettings : Settings, ISettings
    {
       
        private SettingsUIManager _settingsUIManager;
        private Toggle uiItem;
        
        [SerializeField] private bool defaultVal = true; 
        
        private Volume data;
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
            
                
            uiItem = GetComponent<Toggle>();
            data = FindObjectsOfType<Volume>().OrderBy(m => m.transform.GetSiblingIndex()).ToArray()[0]; //FindObjectOfType<Volume>();
            
            defaultValue = defaultVal;
            
            base.Awake();
            
            uiItem.isOn = currentValue.ToBool();
           
            uiItem.onValueChanged.AddListener((value) =>
            {
                currentValue = value;
                if(isLive) Apply();
            });
        }
        private void RestoreAction()
        {
            uiItem.isOn = defaultValue.ToBool(); // on change currentValue will be changed
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
         // if(data)  data.GetComponent<Vignette>().active = currentValue.ToBool();
        }

        
    }
    
  
    
}