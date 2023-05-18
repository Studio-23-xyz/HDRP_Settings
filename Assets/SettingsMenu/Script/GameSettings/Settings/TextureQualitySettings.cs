using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;


namespace GameSettings
{
    [RequireComponent(typeof(TMP_Dropdown))]
    public class TextureQualitySettings : Settings, ISettings
    {
     //   public string[] settings { get; private set; }
    private VideoSettingsController _videoSettingsController;
    private TMP_Dropdown dropdown;

    [SerializeField] private TextureQuality defaultVal = TextureQuality.Medium;
    private void OnEnable()
    {
        _videoSettingsController = FindObjectOfType<VideoSettingsController>();
        _videoSettingsController.ApplyAction += ApplyAction;
        _videoSettingsController.RestoreAction += RestoreAction;
        
        VideoSettingsController.QualityChangedAction += QualityChangedAction;
    }

    private void OnDisable()
    {
        _videoSettingsController.ApplyAction -= ApplyAction;
        _videoSettingsController.RestoreAction -= RestoreAction;
        
       VideoSettingsController.QualityChangedAction -= QualityChangedAction;
    }

    private void QualityChangedAction(QualityName qualityName)
    {

        var setting = _videoSettingsController.QualitySettingsPreset.FirstOrDefault(x => x.names == qualityName);
        if (setting != null)
        {
            int value = (int)setting.textureQuality;
            dropdown.value = value;
        }
    }
    
    public override void Awake()
    {
        dropdown = GetComponent<TMP_Dropdown>();
        dropdown.AddOptionNew(GenerateOptions());

        defaultValue = (int)defaultVal;
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
       
       QualitySettings.masterTextureLimit = currentValue.ToInt(); // 0 - fullRes, limit 0-3
    }

    private List<TMP_Dropdown.OptionData> GenerateOptions()
    {
       // settings  = new [] {"High", "Medium", "Low", "Very Low"};/*Full ,Half, Quarter, Eighth*/
        
        List<TMP_Dropdown.OptionData> optionData = new List<TMP_Dropdown.OptionData>();
        foreach (var item in  Enum.GetValues(typeof(TextureQuality))  )
        {
         optionData.Add(new TMP_Dropdown.OptionData(item.ToString()));
        }
        return optionData;
    }
    }
}