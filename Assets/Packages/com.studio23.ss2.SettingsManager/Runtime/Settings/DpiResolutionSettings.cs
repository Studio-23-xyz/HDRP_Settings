using Studio23.SS2.SettingsManager.Core;
using Studio23.SS2.SettingsManager.Utilities;
using UnityEngine;
using UnityEngine.UI;

namespace Studio23.SS2.SettingsManager.Video
{
	[RequireComponent(typeof(Slider))]
	public class DpiResolutionSettings : Settings
	{
		[SerializeField] private Slider _uiItem;
		[SerializeField] private float _defaultVal = 1;

		public override void Setup()
		{
			base.Initialized(_defaultVal, GetType().Name);
			Apply();
		}

		private void Start()
		{
			_uiItem.Init(CurrentValue.ToFloat());
			_uiItem.onValueChanged.AddListener((value) =>
			{
				CurrentValue = value;
				if (IsLive) Apply();
			});
		}

		public override void RestoreAction()
		{
			_uiItem.value = _defaultVal; // on change CurrentValue will be changed
			base.Save();
			if (!IsLive) Apply(); // if Live then already applied this
		}

		public override void ApplyAction()
		{
			base.Save();
			if (!IsLive) Apply();  // if Live then already applied this
		}

		public void Apply()
		{
			QualitySettings.resolutionScalingFixedDPIFactor = Mathf.Clamp01(CurrentValue.ToFloat()); // default 1, 0-1
		}
	}
}