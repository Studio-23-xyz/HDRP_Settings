﻿using Studio23.SS2.SettingsManager.Core;
using Studio23.SS2.SettingsManager.Core.Component;
using Studio23.SS2.SettingsManager.Data;
using Studio23.SS2.SettingsManager.Extension;
using System.Linq;
using UnityEngine;
using UnityEngine.Rendering.HighDefinition;
using UnityEngine.UI;

namespace GameSettings
{
	[RequireComponent(typeof(Toggle))]
	public class VolumetricLightSettings : Settings
	{

		private VideoSettingsController _videoSettingsController;
		[SerializeField] private Toggle uiItem;

		[SerializeField] private bool defaultVal = true;

		private HDAdditionalLightData data;

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

			_videoSettingsController.QualityChangedAction += QualityChangedAction;
		}

		private void QualityChangedAction(QualityName qualityName)
		{
			var setting = _videoSettingsController.QualitySettingsPreset.FirstOrDefault(x => x.names == qualityName);
			if (setting != null)
			{

				uiItem.isOn = setting.volumetricLight; ;
			}
		}
		public override void Setup()
		{
			data = FindObjectsOfType<HDAdditionalLightData>().OrderBy(m => m.transform.GetSiblingIndex()).ToArray()[0];

			base.Initialized(defaultVal, GetType().Name);

			Apply();
		}

		private void Start()
		{
			uiItem.isOn = CurrentValue.ToBool();

			uiItem.onValueChanged.AddListener((value) =>
			{
				CurrentValue = value;
				if (isLive) Apply();
			});
		}

		private void RestoreAction()
		{
			uiItem.isOn = defaultVal; // on change CurrentValue will be changed
			base.Save();
			if (!isLive) Apply(); // if Live then already applied this
		}
		private void ApplyAction()
		{
			base.Save();
			if (!isLive) Apply();  // if Live then already applied this
		}

		public void Apply()
		{

			data.affectsVolumetric = CurrentValue.ToBool();
		}


	}



}