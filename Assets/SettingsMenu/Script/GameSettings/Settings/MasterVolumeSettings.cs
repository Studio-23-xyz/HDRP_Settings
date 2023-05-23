using System;
using System.Text.RegularExpressions;
using Cinemachine;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Serialization;
using UnityEngine.UIElements;
using Slider = UnityEngine.UI.Slider;

namespace GameSettings
{
    [RequireComponent(typeof(Slider))]
    public class MasterVolumeSettings : Settings
    {
       
        private AudioSettingsController _audioSettingsController;
        [SerializeField] private Slider uiItem;
        
       
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

        public override void Setup()
        {
            defaultValue = defaultVal;
            base.Initialized();
            uiItem.Init(currentValue.ToFloat());
            Apply();
        }

        private void Start()
        {
            
            label.text = FloatToText(defaultValue.ToFloat());
           
            uiItem.onValueChanged.AddListener((value) =>
            {
                currentValue = value;
                if(isLive) Apply();
                label.text = FloatToText(value);
            });
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
            _audioMixerGroup.audioMixer.SetFloat("MasterVol",currentValue.ToFloat().GetAttenuation());
           
        }

       
    }
    
  
    
}