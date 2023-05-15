using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;


namespace GameSettings
{
    [RequireComponent(typeof(TMP_Dropdown))]
    public class GameQualitySettings : Settings, ISettings
    {


        private SettingsUIManager _settingsUIManager;
        private TMP_Dropdown dropdown;

        [SerializeField] private int defaultVal = 1; //default 1; medium, 0 high, 2 low 


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
            dropdown = GetComponent<TMP_Dropdown>();

            dropdown.ClearOptions();
            dropdown.AddOptions(QualitySettings.names.ToList());

            defaultValue = defaultVal;
            base.Awake();


            dropdown.value = currentValue.ToInt();

            dropdown.onValueChanged.AddListener((value) =>
            {
                currentValue = value;
                if (isLive) Apply();
                _settingsUIManager.QualityChangedAction.Invoke( (QualityName)currentValue.ToInt());
            });


        }

        private void RestoreAction()
        {
            dropdown.value = defaultValue.ToInt(); // on change currentValue will be changed
            base.Save();
            if (!isLive) Apply(); // if Live then already applied this
        }

        private void ApplyAction()
        {
            base.Save();
            if (!isLive) Apply(); // if Live then already applied this
        }

        public void Apply()
        {
            QualitySettings.SetQualityLevel(currentValue.ToInt(), true);
        }

        
        
    }


}