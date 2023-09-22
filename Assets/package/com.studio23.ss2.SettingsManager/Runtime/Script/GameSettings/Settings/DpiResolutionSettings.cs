using Studio23.SS2.SettingsManager.Core;
using Studio23.SS2.SettingsManager.Core.Component;
using Studio23.SS2.SettingsManager.Extension;
using UnityEngine;
using UnityEngine.UI;

namespace GameSettings
{
	[RequireComponent(typeof(Slider))]
	public class DpiResolutionSettings : Settings
	{

		private SettingsController _videoSettingsController;
		[SerializeField] private Slider uiItem;

		[SerializeField] private float defaultVal = 1;


		//private void OnEnable()
		//{
		//	_videoSettingsController = FindObjectOfType<SettingsController>();
		//	_videoSettingsController.ApplyAction += ApplyAction;
		//	_videoSettingsController.RestoreAction += RestoreAction;
		//}

		//private void OnDisable()
		//{
		//	_videoSettingsController.ApplyAction -= ApplyAction;
		//	_videoSettingsController.RestoreAction -= RestoreAction;
		//}

		public override void Setup()
		{
			base.Initialized(defaultVal, GetType().Name);

			Apply();
		}

		private void Start()
		{

			uiItem.Init(CurrentValue.ToFloat());

			uiItem.onValueChanged.AddListener((value) =>
			{
				CurrentValue = value;
				if (isLive) Apply();
			});
		}

		public override void RestoreAction()
		{
			uiItem.value = defaultVal; // on change CurrentValue will be changed
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
			QualitySettings.resolutionScalingFixedDPIFactor = Mathf.Clamp01(CurrentValue.ToFloat()); // default 1, 0-1

		}


	}



}