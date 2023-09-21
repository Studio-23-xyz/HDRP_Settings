using Studio23.SS2.SettingsManager.Core;
using Studio23.SS2.SettingsManager.Core.Component;
using Studio23.SS2.SettingsManager.Data;
using Studio23.SS2.SettingsManager.Extension;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace GameSettings
{
	[RequireComponent(typeof(Toggle))]
	public class VSyncSettings : Settings
	{

		private VideoSettingsController _videoSettingsController;
		[SerializeField] private Toggle _uiItem;

		[SerializeField] private bool _defaultVal = true;


		private void OnEnable()
		{
			_videoSettingsController = FindObjectOfType<VideoSettingsController>();
			_videoSettingsController.ApplyAction += ApplyAction;
			_videoSettingsController.RestoreAction += RestoreAction;

			_videoSettingsController.QualityChangedAction += QualityChangedAction;
		}

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

		private void QualityChangedAction(QualityName qualityName)
		{
			var setting = _videoSettingsController.QualitySettingsPreset.FirstOrDefault(x => x.names == qualityName);
			if (setting != null)
			{

				_uiItem.isOn = setting.vSyncCount; ;
			}
		}

		private void RestoreAction()
		{
			_uiItem.isOn = _defaultVal; // on change CurrentValue will be changed
			base.Save();
			if (!isLive) Apply(); // if Live then already applied this
		}
		private void ApplyAction()
		{
			base.Save();
			if (!isLive) Apply();  // if Live then already applied this
		}

		public void Apply() => QualitySettings.vSyncCount = CurrentValue.ToBool() ? 1 : 0; //? 0 dont, 1 every v blank;

		private void OnDisable()
		{
			_videoSettingsController.ApplyAction -= ApplyAction;
			_videoSettingsController.RestoreAction -= RestoreAction;

			_videoSettingsController.QualityChangedAction -= QualityChangedAction;
		}
	}



}