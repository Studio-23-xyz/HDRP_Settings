﻿using Cinemachine;
using UnityEngine;
using UnityEngine.UI;

namespace GameSettings
{
    [RequireComponent(typeof(Slider))]
    public class FovSettings : Settings, ISettings
    {
       
        private SettingsUIManager _settingsUIManager;
        private Slider uiItem;
        
        [SerializeField] private float minValue = 0; 
        [SerializeField] private float maxValue = 1; 
        [SerializeField] private float defaultVal = 0;
        private CinemachineVirtualCamera virtualCamera;
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
            uiItem = GetComponent<Slider>();
            virtualCamera = FindObjectOfType<CinemachineVirtualCamera>();

            defaultValue = defaultVal;
            
            base.Awake();
            
            uiItem.Init(minValue, maxValue, currentValue.ToFloat());
            
           
            uiItem.onValueChanged.AddListener((value) =>
            {
                currentValue = value;
                if(isLive) Apply();
            });
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
            if(virtualCamera) virtualCamera.m_Lens.FieldOfView = 60f + Mathf.Clamp01(currentValue.ToFloat()) * 60f; // float : 0 - 1, 60-120
        }

        
    }
    
  
    
}