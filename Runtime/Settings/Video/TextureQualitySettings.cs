using Studio23.SS2.SettingsManager.Core;
using Studio23.SS2.SettingsManager.Data;
using Studio23.SS2.SettingsManager.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

namespace Studio23.SS2.SettingsManager.Video
{
	[RequireComponent(typeof(TMP_Dropdown))]
	public class TextureQualitySettings : Settings
	{
		[SerializeField] private TMP_Dropdown _uiItem;
		[SerializeField] private TextureQuality _defaultVal = TextureQuality.Medium;

		protected override void OnQualityChanged(QualityName qualityName)
		{
			var setting = VideoSettingsController.QualitySettingsPreset.FirstOrDefault(x => x.Names == qualityName);
			if (setting != null)
			{
				int value = (int)setting.TextureQuality;
				_uiItem.value = value;
			}
		}

		public override void Setup()
		{
			base.Initialized((int)_defaultVal, GetType().Name);
			Apply();
		}

		private void Start()
		{
			_uiItem.AddOptionNew(GenerateOptions());
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
			QualitySettings.globalTextureMipmapLimit = CurrentValue.ToInt(); // 0 - fullRes, limit 0-3
		}

		private List<TMP_Dropdown.OptionData> GenerateOptions()
		{
			// Settings  = new [] {"High", "Medium", "Low", "Very Low"};/*Full ,Half, Quarter, Eighth*/
			List<TMP_Dropdown.OptionData> optionData = new List<TMP_Dropdown.OptionData>();
			foreach (var item in Enum.GetValues(typeof(TextureQuality)))
			{
				optionData.Add(new TMP_Dropdown.OptionData(item.ToString()));
			}
			return optionData;
		}
	}
}