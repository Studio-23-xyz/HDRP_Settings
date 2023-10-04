using Studio23.SS2.SettingsManager.Core;
using Studio23.SS2.SettingsManager.Data;
using Studio23.SS2.SettingsManager.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering.HighDefinition;
using ShadowQuality = Studio23.SS2.SettingsManager.Data.ShadowQuality;


namespace Studio23.SS2.SettingsManager.Video
{
	[RequireComponent(typeof(TMP_Dropdown))]
	public class ShadowQualitySettings : Settings
	{
		private HDAdditionalLightData _data;

		public string[] Settings { get; private set; }
		[SerializeField] private TMP_Dropdown _uiItem;
		[SerializeField] private ShadowQuality _defaultVal = ShadowQuality.Medium;

		protected override void OnQualityChanged(QualityName qualityName)
		{
			var setting = VideoSettingsController.QualitySettingsPreset.FirstOrDefault(x => x.Names == qualityName);
			if (setting != null)
			{
				int value = (int)setting.ShadowQuality;
				_uiItem.value = value;
			}
		}

		public override void Setup()
		{
			_data = FindObjectOfType<HDAdditionalLightData>();
			if (_data) _data.SetShadowResolutionOverride(false);
			base.Initialized((int)_defaultVal, GetType().Name);
			Apply();
		}

		private void Start()
		{
			_uiItem.AddOptionNew(GetOptions());
			_uiItem.value = CurrentValue.ToInt();

			_uiItem.onValueChanged.AddListener((value) =>
			{
				CurrentValue = value;
				if (IsLive) Apply();
			});
		}

		public override void RestoreAction()
		{
			_uiItem.value = (int)_defaultVal; // on change CurrentValue will be changed
			base.Save();
			if (!IsLive) Apply(); // if Live then already applied this
		}

		public override void ApplyAction()
		{
			base.Save();
			if (!IsLive) Apply(); // if Live then already applied this
		}

		public void Apply()
		{
			_data.SetShadowResolutionLevel(CurrentValue.ToInt());
		}

		private List<TMP_Dropdown.OptionData> GetOptions()
		{
			// Settings  = new [] {"Low", "Medium", "High", "Ultra"};/*Def 0, low  || Light-> Low Medium High Ultra*/
			List<TMP_Dropdown.OptionData> optionData = new List<TMP_Dropdown.OptionData>();
			foreach (var item in Enum.GetValues(typeof(ShadowQuality)))
			{
				optionData.Add(new TMP_Dropdown.OptionData(item.ToString()));
			}

			return optionData;
		}
	}
}