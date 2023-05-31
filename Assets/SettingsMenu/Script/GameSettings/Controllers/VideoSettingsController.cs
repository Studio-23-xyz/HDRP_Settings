using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace GameSettings
{
    public class VideoSettingsController : MonoBehaviour
    {

        public List<QualitySetting> QualitySettingsPreset;

        public Action<QualityName> QualityChangedAction;
        public Action ApplyAction;
        public Action RestoreAction;

        [SerializeField] private Button applyButton;
        [SerializeField] private Button restoreButton;
       
       
        public List<Settings> settings;

        public void Initialized()
        {
            settings = GetComponentsInChildren<Settings>(true).ToList();
            settings.ForEach(setting => setting.Setup());
        }

        private void Start()
        {
            applyButton.onClick.AddListener(ApplyAction.Invoke);
            restoreButton.onClick.AddListener(RestoreAction.Invoke);
            
        }
        
      
     
       
    }
    
    [Serializable]
    public class QualitySetting
    {
        public QualityName names;
        public TextureQuality textureQuality;
        public ShadowQuality shadowQuality;
        public bool vSyncCount;
        public bool volumetricLight;
        public bool bloom;
        public bool vignette;
        public bool ambientOcclusion;
    }

    [Serializable]
    public enum QualityName
    {
        High,
        Medium,
        Low,
    }

    [Serializable]
    public enum ShadowQuality
    {
        Low,
        Medium,
        High,
        Ultra
    }

    [Serializable]
    public enum TextureQuality
    {
        High,
        Medium,
        Low,
        Very_Low
    }
    
}
 
/*
High:
   Texture Full
   Shadow High
   vSync 1
Medium:
   Texture Half
   Shadow Medium
   vSync 0
 Low:
   Texture Quarter
   Shadow Low
   vSync 0
*/

    
 