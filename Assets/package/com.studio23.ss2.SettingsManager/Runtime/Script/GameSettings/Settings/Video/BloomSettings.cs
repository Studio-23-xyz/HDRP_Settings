using Studio23.SS2.SettingsManager.Core;
using Studio23.SS2.SettingsManager.Core.Component;
using Studio23.SS2.SettingsManager.Data;
using Studio23.SS2.SettingsManager.Extension;
using System.Linq;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.HighDefinition;
using UnityEngine.UI;

namespace GameSettings
{
	[RequireComponent(typeof(Toggle))]
	public class BloomSettings : Settings
	{

		private VideoSettingsController _videoSettingsController;
		[SerializeField] private Toggle uiItem;

		[SerializeField] private bool defaultVal = true;

		private VolumeProfile data;
		private Bloom component;

		//private void OnEnable()
		//{
		//	_videoSettingsController = FindObjectOfType<VideoSettingsController>();
		//	_videoSettingsController.ApplyAction += ApplyAction;
		//	_videoSettingsController.RestoreAction += RestoreAction;

		//	_videoSettingsController.QualityChangedAction += QualityChangedAction;
		//}

		//private void OnDisable()
		//{
		//	_videoSettingsController.ApplyAction -= ApplyAction;
		//	_videoSettingsController.RestoreAction -= RestoreAction;

		//	_videoSettingsController.QualityChangedAction += QualityChangedAction;
		//}

		protected override void OnQualityChanged(QualityName qualityName)
		{
			var setting = VideoSettingsController.QualitySettingsPreset.FirstOrDefault(x => x.names == qualityName);
			if (setting != null)
			{
				uiItem.isOn = setting.bloom; ;
			}
		}
		

		public override void Setup()
		{
			data = FindObjectsOfType<Volume>().OrderBy(m => m.transform.GetSiblingIndex()).ToArray()[0].sharedProfile; //FindObjectOfType<Volume>();
			data.TryGet(typeof(Bloom), out component);



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