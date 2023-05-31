using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

namespace GameSettings
{
    [Serializable]
    public class AudioSetting
    {
        public string settingsName= "Volume Settings";
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

        public List<Settings> settings;
        public void Initialized()
        {
            settings = GetComponentsInChildren<Settings>(true).ToList();
            settings.ForEach(setting => setting.Setup());

            InitializedOthersAudioSettings();
        }

        private void InitializedOthersAudioSettings()
        {
            var i = 0;
            foreach (var audioSetting in audioSettings)
            {
                var volumeController = Instantiate(volumeControllerTemplate,containerTransform);
                volumeController.name += i++;
                volumeController.Init(audioSetting);
            }
        }
        private void Start()
        {
                applyButton.onClick.AddListener(ApplyAction.Invoke);
                restoreButton.onClick.AddListener(RestoreAction.Invoke);
        }

       
    }
    
}