using Cinemachine;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

namespace GameSettings
{
    [RequireComponent(typeof(Slider))]
    public class MusicVolumeSettings : Settings, ISettings
    {
       
        private AudioSettingsController _audioSettingsController;
        private Slider uiItem;
        
        
        [Range(0,1)]
        [SerializeField] private float defaultVal = .75f;
        [SerializeField] private TMP_Text label;
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
            
            uiItem.Init(currentValue.ToFloat());
            label.text = FloatToText(defaultValue.ToFloat());
           
            uiItem.onValueChanged.AddListener((value) =>
            {
                currentValue = value;
                if(isLive) Apply();
                label.text = FloatToText(value);
            });
            
            Apply();
            
        }
        private void RestoreAction()
        {
            uiItem.value = defaultValue.ToFloat(); // on change currentValue will be changed
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
            _audioMixerGroup.audioMixer.SetFloat("BGVol",currentValue.ToFloat().GetAttenuation());
        }
        
    }
    
  
    
}