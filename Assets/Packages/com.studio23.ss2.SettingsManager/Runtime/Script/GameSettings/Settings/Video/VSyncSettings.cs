using Studio23.SS2.SettingsManager.Core;
using Studio23.SS2.SettingsManager.Core.Component;
using Studio23.SS2.SettingsManager.Data;
using System.Linq;
using Studio23.SS2.SettingsManager.Extensions;
using UnityEngine;
using UnityEngine.UI;

namespace Studio23.SS2.SettingsManager.Video
{
	[RequireComponent(typeof(Toggle))]
	public class VSyncSettings : Settings
	{
		[SerializeField] private Toggle _uiItem;
		[SerializeField] private bool _defaultVal = true;

		private void Start()
		{
			_uiItem.isOn = CurrentValue.ToBool();
			_uiItem.onValueChanged.AddListener((value) =>
			{
				CurrentValue = value;
				if (isLive) Apply();
			});
		}

		public override void Setup()
		{
			base.Initialized(_defaultVal, GetType().Name);
			Apply();
		}

		protected override void OnQualityChanged(QualityName qualityName)
		{
			var setting = VideoSettingsController.QualitySettingsPreset.FirstOrDefault(x => x.names == qualityName);
			if (setting != null)
			{
				_uiItem.isOn = setting.vSyncCount; ;
			}
		}

		public override void RestoreAction()
		{
			_uiItem.isOn = _defaultVal; // on change CurrentValue will be changed
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
			QualitySettings.vSyncCount = CurrentValue.ToBool() ? 1 : 0;
			//? 0 dont, 1 every v blank;0
		}
	}
}