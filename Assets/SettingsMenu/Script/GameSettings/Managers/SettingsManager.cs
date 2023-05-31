using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;



namespace GameSettings
{
    public class SettingsManager : MonoBehaviour
    {
        [SerializeField] private AudioSettingsController audioSettingsController;
        [SerializeField] private VideoSettingsController videoSettingsController;
        [SerializeField] private OtherSettingsController otherSettingsController;
        
        [SerializeField] private TMP_Text statusText;
       

        private void Awake()
        {
            audioSettingsController.Initialized();
            videoSettingsController.Initialized();
            otherSettingsController.Initialized();
        }
        private string DeviceSettings()
        {
            string output = null;
            output += $"fullScreenMode: {Screen.fullScreenMode} \n";
            output += $"currentResolution: {Screen.currentResolution} \n";
            output += $"vSyncCount: {QualitySettings.vSyncCount} \n";
            // output += $"brightness: {_autoExposure.keyValue.value/4.0f} \n";
            // output += $"fov: {virtualCamera.m_Lens.FieldOfView} \n";
            output += $"dpi: {QualitySettings.resolutionScalingFixedDPIFactor} \n";
            output += $"GetQualityLevel: {QualitySettings.GetQualityLevel()} \n";
            output += $"masterTextureLimit: {QualitySettings.masterTextureLimit.ToString()} \n";
            output += $"Shadow: {QualitySettings.shadows} \n";
            return output;
        }

        
        
    }
}
