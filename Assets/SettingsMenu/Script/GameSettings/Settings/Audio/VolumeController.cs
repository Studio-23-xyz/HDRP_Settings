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
    public class VolumeController : Settings
    {
        [SerializeField] private Slider uiItem;
        [SerializeField] private TMP_Text label;
        
        private AudioSettingsController _audioSettingsController;
        private AudioSetting audioSetting;
        private void OnEnable()
        {
            _audioSettingsController ??= FindObjectOfType<AudioSettingsController>();
            _audioSettingsController.ApplyAction += ApplyAction;
            _audioSettingsController.RestoreAction += RestoreAction;
        }

        private void OnDisable()
        {
            _audioSettingsController.ApplyAction -= ApplyAction;
            _audioSettingsController.RestoreAction -= RestoreAction;
        }

        public void Init(AudioSetting adoSetting)
        {
           audioSetting = adoSetting;
           Setup();
        }
        public override void Setup()
        {
            if(audioSetting == null) return;
            
            base.Initialized(audioSetting.defaultValue, name, audioSetting.isLive);
            uiItem.Init(currentValue.ToFloat());
            Apply();
        }

        private void Start()
        {
            
            label.text = FloatToText(audioSetting.defaultValue, audioSetting.settingsName);
           
            uiItem.onValueChanged.AddListener((value) =>
            {
                currentValue = value;
                if(isLive) Apply();
                label.text = FloatToText(value, audioSetting.settingsName);
            });
        }

        private void RestoreAction()
        {
            uiItem.value = audioSetting.defaultValue; // on change currentValue will be changed
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
            audioSetting.audioMixerGroup.audioMixer.SetFloat(audioSetting.exposedParameter,currentValue.ToFloat().GetAttenuation());
           
        }

       
    }
    
  
    
}