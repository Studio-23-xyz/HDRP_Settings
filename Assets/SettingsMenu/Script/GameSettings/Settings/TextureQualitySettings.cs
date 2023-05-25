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
    public class TextureQualitySettings : Settings
    {
     //   public string[] settings { get; private set; }
    private VideoSettingsController _videoSettingsController;
    [SerializeField] private TMP_Dropdown uiItem;

    [SerializeField] private TextureQuality defaultVal = TextureQuality.Medium;
    private void OnEnable()
    {
        _videoSettingsController = FindObjectOfType<VideoSettingsController>();
        _videoSettingsController.ApplyAction += ApplyAction;
        _videoSettingsController.RestoreAction += RestoreAction;
        
        _videoSettingsController.QualityChangedAction += QualityChangedAction;
    }

    private void OnDisable()
    {
        _videoSettingsController.ApplyAction -= ApplyAction;
        _videoSettingsController.RestoreAction -= RestoreAction;
        
        _videoSettingsController.QualityChangedAction -= QualityChangedAction;
    }

    private void QualityChangedAction(QualityName qualityName)
    {

        var setting = _videoSettingsController.QualitySettingsPreset.FirstOrDefault(x => x.names == qualityName);
        if (setting != null)
        {
            int value = (int)setting.textureQuality;
            uiItem.value = value;
        }
    }
    
    public override void Setup()
    {
        
        
        
      

       
        base.Initialized((int)defaultVal);
        uiItem.value = currentValue.ToInt();
       
       
        Apply();
    }

    

    
    private void Start()
    {
      
        uiItem.AddOptionNew(GenerateOptions());
        
        uiItem.onValueChanged.AddListener((value) =>
        {
            currentValue = value;
            if (isLive) Apply();
        });
    }

    private void RestoreAction()
    {
        uiItem.value =(int)defaultVal; // on change currentValue will be changed
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