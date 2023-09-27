using Studio23.SS2.SettingsManager.Core;
using Studio23.SS2.SettingsManager.Core.Component;
using Studio23.SS2.SettingsManager.Data;
using Studio23.SS2.SettingsManager.Extensions;
using System.Linq;
using UnityEngine;
using UnityEngine.Rendering.HighDefinition;
using UnityEngine.UI;

namespace Studio23.SS2.SettingsManager.Video
{
	[RequireComponent(typeof(Toggle))]
	public class VolumetricLightSettings : Settings
	{
		[SerializeField] private Toggle uiItem;
		[SerializeField] private bool defaultVal = true;
		private HDAdditionalLightData data;

		protected override void OnQualityChanged(QualityName qualityName)
		{
			var setting = VideoSettingsController.QualitySettingsPreset.FirstOrDefault(x => x.names == qualityName);
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
			data.affectsVolumetric = CurrentValue.ToBool();
		}
	}
}