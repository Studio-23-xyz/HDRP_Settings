﻿using Studio23.SS2.SettingsManager.Core.Component;
using Studio23.SS2.SettingsManager.Data;
using System.Linq;
using Studio23.SS2.SettingsManager.Extensions;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.HighDefinition;
using UnityEngine.UI;

namespace Studio23.SS2.SettingsManager.Video
{
	[RequireComponent(typeof(Toggle))]
	public class AmbientOcclusionSettings : Settings
	{
		[SerializeField] private Toggle uiItem;

		[SerializeField] private bool defaultVal = true;
		private VolumeProfile data;
		private ScreenSpaceAmbientOcclusion component;

		protected override void OnQualityChanged(QualityName qualityName)
		{
			var setting = VideoSettingsController.QualitySettingsPreset.FirstOrDefault(x => x.names == qualityName);
			if (setting != null)
			{
				uiItem.isOn = setting.ambientOcclusion; ;
			}
		}

		public override void Setup()
		{
			data = FindObjectsOfType<Volume>().OrderBy(m => m.transform.GetSiblingIndex()).ToArray()[0].sharedProfile;
			data.TryGet(typeof(ScreenSpaceAmbientOcclusion), out component);

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

		public override void RestoreAction()
		{
			uiItem.isOn = defaultVal; // on change CurrentValue will be changed
			base.Save();
			if (!isLive) Apply(); // if Live then already applied this
		}
		public override void ApplyAction()
		{
			base.Save();
			if (!isLive) Apply();  // if Live then already applied this
		}

		public void Apply()
		{
			component.active = CurrentValue.ToBool();
		}
	}
}