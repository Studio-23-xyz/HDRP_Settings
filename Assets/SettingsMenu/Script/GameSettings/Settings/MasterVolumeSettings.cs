using Cinemachine;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace GameSettings
{
    [RequireComponent(typeof(Slider))]
    public class MasterVolumeSettings : Settings, ISettings
    {
       
        private AudioSettingsController _audioSettingsController;
        private Slider uiItem;
        
        [SerializeField] private float minValue = 0; 
        [SerializeField] private float maxValue = 1; 
        [SerializeField] private float defaultVal = 0.75f;
       
          
        [SerializeField] private AudioMixerGroup _audioMixerGroup;
        private void OnEnable()
        {
            _audioSettingsController = FindObjectOfType<AudioSettingsController>();
            _audioSettingsController.ApplyAction += ApplyAction;
            _audioSettingsController.RestoreAction += RestoreAction;
        }

        private void OnDisable()
        {
            _audioSettingsController.ApplyAction -= ApplyAction;
            _audioSettingsController.RestoreAction -= RestoreAction;
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
            _audioMixerGroup.audioMixer.SetFloat("MasterVol",currentValue.ToFloat().GetAttenuation());
           
        }
       
        
    }
    
  
    
}