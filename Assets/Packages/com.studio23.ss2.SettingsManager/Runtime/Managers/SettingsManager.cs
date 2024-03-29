﻿using UnityEngine;

namespace Studio23.SS2.SettingsManager.Core
{
	public class SettingsManager : MonoBehaviour
	{
		[SerializeField] private AudioSettingsController _audioSettingsController;
		[SerializeField] private VideoSettingsController _videoSettingsController;

		private void Awake()
		{
			_audioSettingsController.Initialize();
			_videoSettingsController.Initialize();
		}

		private string DeviceSettings()
		{
			string output = null;
			output += $"fullScreenMode: {Screen.fullScreenMode} \n";
			output += $"currentResolution: {Screen.currentResolution} \n";
			output += $"vSyncCount: {QualitySettings.vSyncCount} \n";
			output += $"dpi: {QualitySettings.resolutionScalingFixedDPIFactor} \n";
			output += $"GetQualityLevel: {QualitySettings.GetQualityLevel()} \n";
			output += $"masterTextureLimit: {QualitySettings.globalTextureMipmapLimit.ToString()} \n";
			output += $"Shadow: {QualitySettings.shadows} \n";
			return output;
		}
	}
}
