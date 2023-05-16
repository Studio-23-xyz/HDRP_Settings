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


        public SettingsUIManager _settingsUIManager;
        private TMP_Dropdown uiItem;

        [SerializeField] private QualityName defaultVal = QualityName.Medium ; //default 1; medium, 0 high, 2 low 

       
        
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
            uiItem = GetComponent<TMP_Dropdown>();

            uiItem.ClearOptions();
            uiItem.AddOptions(GenerateOptions());
          

            defaultValue = (int)defaultVal;
            base.Awake();


            uiItem.value = currentValue.ToInt();

            uiItem.onValueChanged.AddListener((value) =>
            {
                currentValue = value;
                if (isLive) Apply();
                SettingsUIManager.QualityChangedAction?.Invoke( (QualityName)currentValue.ToInt());
            });


        }

        private void RestoreAction()
        {
            uiItem.value = defaultValue.ToInt(); // on change currentValue will be changed
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

        private List<TMP_Dropdown.OptionData> GenerateOptions()
        {
            
        //uiItem.AddOptions(QualitySettings.names.ToList());
            List<TMP_Dropdown.OptionData> optionData = new List<TMP_Dropdown.OptionData>();
            foreach (var item in  Enum.GetValues(typeof(QualityName))  )
            {
                optionData.Add(new TMP_Dropdown.OptionData(item.ToString()));
            }
            return optionData;
        }
        
    }


}