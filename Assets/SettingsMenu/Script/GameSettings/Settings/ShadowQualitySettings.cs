﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.HighDefinition;


namespace GameSettings
{
    [RequireComponent(typeof(TMP_Dropdown))]
    public class ShadowQualitySettings : Settings, ISettings
    {

       private HDAdditionalLightData data;

        public string[] settings { get; private set; }
        private VideoSettingsController _videoSettingsController;
        private TMP_Dropdown dropdown;
        [SerializeField] private ShadowQuality defaultVal = ShadowQuality.Medium;
        private void OnEnable()
        {
            _videoSettingsController = FindObjectOfType<VideoSettingsController>();
            _videoSettingsController.ApplyAction += ApplyAction;
            _videoSettingsController.RestoreAction += RestoreAction;

            VideoSettingsController.QualityChangedAction += QualityChangedAction;
        }

        private void QualityChangedAction(QualityName qualityName)
        {
            var setting = _videoSettingsController.QualitySettingsPreset.FirstOrDefault(x => x.names == qualityName);
            if (setting != null)
            {
                int value = (int)setting.shadowQuality;
                dropdown.value = value;
            }
        }

        private void OnDisable()
        {
            _videoSettingsController.ApplyAction -= ApplyAction;
            _videoSettingsController.RestoreAction -= RestoreAction;

           VideoSettingsController.QualityChangedAction -= QualityChangedAction;
        }

        public override void Awake()
        {
            data = FindObjectOfType<HDAdditionalLightData>();
            if(data)data.SetShadowResolutionOverride(false);
            // levels will only take effect if res is disabled

            dropdown = GetComponent<TMP_Dropdown>();
            dropdown.AddOptionNew(GenerateOptions());

            defaultValue = (int) defaultVal;
            base.Awake();


            dropdown.value = currentValue.ToInt();

            dropdown.onValueChanged.AddListener((value) =>
            {
                currentValue = value;
                if (isLive) Apply();
            });
            Apply();
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

            data.SetShadowResolutionLevel(currentValue.ToInt());

            /*var hdRenderPipelineAsset = GetRpQualityAsset();
            GraphicsSettings.renderPipelineAsset = null;
            GraphicsSettings.renderPipelineAsset = hdRenderPipelineAsset;*/

        }

        private List<TMP_Dropdown.OptionData> GenerateOptions()
        {
            // settings  = new [] {"Low", "Medium", "High", "Ultra"};/*Def 0, low  || Light-> Low Medium High Ultra*/



            List<TMP_Dropdown.OptionData> optionData = new List<TMP_Dropdown.OptionData>();
            foreach (var item in Enum.GetValues(typeof(ShadowQuality)))
            {
                optionData.Add(new TMP_Dropdown.OptionData(item.ToString()));
            }

            return optionData;

        }
    }
}