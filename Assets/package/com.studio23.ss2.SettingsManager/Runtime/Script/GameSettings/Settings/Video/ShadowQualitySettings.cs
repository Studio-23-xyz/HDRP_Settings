using Studio23.SS2.SettingsManager.Core;
using Studio23.SS2.SettingsManager.Core.Component;
using Studio23.SS2.SettingsManager.Data;
using Studio23.SS2.SettingsManager.Extension;
using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering.HighDefinition;
using ShadowQuality = Studio23.SS2.SettingsManager.Data.ShadowQuality;


namespace GameSettings
{
	[RequireComponent(typeof(TMP_Dropdown))]
	public class ShadowQualitySettings : Settings
	{

		private HDAdditionalLightData data;

		public string[] settings { get; private set; }
		[SerializeField] private TMP_Dropdown uiItem;
		[SerializeField] private ShadowQuality defaultVal = ShadowQuality.Medium;

		protected override void OnQualityChanged(QualityName qualityName)
		{
			var setting = VideoSettingsController.QualitySettingsPreset.FirstOrDefault(x => x.names == qualityName);
			if (setting != null)
			{
				int value = (int)setting.shadowQuality;
				uiItem.value = value;
			}
		}

		public override void Setup()
		{
			data = FindObjectOfType<HDAdditionalLightData>();
			if (data) data.SetShadowResolutionOverride(false);


			base.Initialized((int)defaultVal, GetType().Name);

			Apply();
		}

		private void Start()
		{
			uiItem.AddOptionNew(GetOptions());
			uiItem.value = CurrentValue.ToInt();

			uiItem.onValueChanged.AddListener((value) =>
			{
				CurrentValue = value;
				if (isLive) Apply();
			});
		}

		public override void RestoreAction()
		{
			uiItem.value = (int)defaultVal; // on change CurrentValue will be changed
			base.Save();
			if (!isLive) Apply(); // if Live then already applied this
		}

		public override void ApplyAction()
		{
			base.Save();
			if (!isLive) Apply(); // if Live then already applied this
		}

		public void Apply()
		{

			data.SetShadowResolutionLevel(CurrentValue.ToInt());

			/*var hdRenderPipelineAsset = GetRpQualityAsset();
            GraphicsSettings.renderPipelineAsset = null;
            GraphicsSettings.renderPipelineAsset = hdRenderPipelineAsset;*/

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