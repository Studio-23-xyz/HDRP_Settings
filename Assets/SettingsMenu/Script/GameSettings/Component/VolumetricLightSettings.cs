using Cinemachine;
using UnityEngine;
using UnityEngine.Rendering.HighDefinition;
using UnityEngine.UI;

namespace GameSettings
{
    [RequireComponent(typeof(Toggle))]
    public class VolumetricLightSettings : Settings, ISettings
    {
       
        private SettingsUIManager _settingsUIManager;
        private Toggle uiItem;
        
        [SerializeField] private bool defaultVal = true;

       public HDAdditionalLightData data;
       
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
            
            
            data = FindObjectOfType<HDAdditionalLightData>();
            
            
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
            data.affectsVolumetric = currentValue.ToBool();
        }

        
    }
    
  
    
}