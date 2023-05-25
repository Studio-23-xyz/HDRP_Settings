using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

namespace GameSettings
{
    [Serializable]
    public class AudioSetting
    {
        public bool isLive = true;
        public AudioMixerGroup audioMixerGroup;
        [Range(0, 1)] public float defaultValue = 0.75f;
        public string exposedParameter;
    }

   public class AudioSettingsController : MonoBehaviour
   {
       [SerializeField] private List<AudioSetting> audioSettings;
       [SerializeField] private Transform containerTransform;
       [SerializeField] private VolumeController volumeControllerTemplate;
        public Action ApplyAction;
        public Action RestoreAction;

        [SerializeField] private Button applyButton;
        [SerializeField] private Button restoreButton;

        private void Start()
        {
           
                applyButton.onClick.AddListener(ApplyAction.Invoke);
                restoreButton.onClick.AddListener(RestoreAction.Invoke);

              //  GenerateOtherAudioSettings();

        }

        private void GenerateOtherAudioSettings()
        {
            foreach (AudioSetting audioSetting in audioSettings)
            {
                VolumeController volumeController = Instantiate(volumeControllerTemplate,containerTransform);
                volumeController.Init(audioSetting);
            }
        }
    }
    
}