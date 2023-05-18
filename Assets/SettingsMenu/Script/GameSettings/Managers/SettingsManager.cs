using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace GameSettings
{
    public class SettingsManager : MonoBehaviour
    {
        public static SettingsManager Instance;
        
        [SerializeField] private TMP_Text statusText;
          [SerializeField] private bool InitOnStart;
        [FormerlySerializedAs("masterVolumeController")]
        [Header("Audio Settings")]
        [SerializeField] private MasterVolumeSettings masterVolumeSettings;
        [SerializeField] private SFXVolumeSettings sfxVolumeSettings;
        [FormerlySerializedAs("musicVolumeController")] [SerializeField] private MusicVolumeSettings musicVolumeSettings;
        [Header("Video Settings")]
        [SerializeField] private FullScreenModeSettings fullScreenModeSettings;
        [SerializeField] private ScreenResolutionSettings screenResolutionSettings;
        [SerializeField] private FovSettings fovSettings;
        [SerializeField] private DpiResolutionSettings dpiResolutionSettings;
        [SerializeField] private GameQualitySettings gameQualitySettings;
        [SerializeField] private TextureQualitySettings textureQualitySettings;
        [SerializeField] private ShadowQualitySettings shadowQualitySettings;
        [SerializeField] private VSyncSettings vSyncSettings;
        [SerializeField] private VolumetricLightSettings bVolumetricLightSettings;
        [SerializeField] private BloomSettings bloomSettings;
        [SerializeField] private VignetteSettings vignetteSettings;
        [SerializeField] private AmbientOcclusionSettings ambientOcclusionSettings;
       
       
        private void Awake()
        {
            if (Instance == null) Instance = this;
            else Destroy(gameObject);
        }

        public void ShowStatus(string status)
        {
            statusText.text = status;
        }

        private void Start()
        {
           if(InitOnStart) Initialized();
        }

        private void Initialized()
        {
            masterVolumeSettings.Awake();
            sfxVolumeSettings.Awake();
            musicVolumeSettings.Awake();
            fullScreenModeSettings.Awake();
            screenResolutionSettings.Awake();
            fovSettings.Awake();
            dpiResolutionSettings.Awake();
            gameQualitySettings.Awake();
            textureQualitySettings.Awake();
            shadowQualitySettings.Awake();
            vSyncSettings.Awake();
            bVolumetricLightSettings.Awake();
            bloomSettings.Awake();
            vignetteSettings.Awake();
            ambientOcclusionSettings.Awake();
        }
    }
}
