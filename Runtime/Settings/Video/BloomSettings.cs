using Studio23.SS2.SettingsManager.Core;
using Studio23.SS2.SettingsManager.Data;
using System.Linq;
using Studio23.SS2.SettingsManager.Utilities;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.HighDefinition;
using UnityEngine.UI;

namespace Studio23.SS2.SettingsManager.Video
{
	[RequireComponent(typeof(Toggle))]
	public class BloomSettings : Settings
	{
		[SerializeField] private Toggle _uiItem;
		[SerializeField] private bool _defaultVal = true;

		private VolumeProfile _data;
		private Bloom _component;

		protected override void OnQualityChanged(QualityName qualityName)
		{
			var setting = VideoSettingsController.QualitySettingsPreset.FirstOrDefault(x => x.Names == qualityName);
			if (setting != null)
			{
				_uiItem.isOn = setting.Bloom; ;
			}
		}
		
		public override void Setup()
		{
			_data = FindObjectsOfType<Volume>().OrderBy(m => m.transform.GetSiblingIndex()).ToArray()[0].sharedProfile; //FindObjectOfType<Volume>();
			_data.TryGet(typeof(Bloom), out _component);
			base.Initialized(_defaultVal, GetType().Name);

			Apply();
		}

		private void Start()
		{
			_uiItem.isOn = CurrentValue.ToBool();
			_uiItem.onValueChanged.AddListener((value) =>
			{
				CurrentValue = value;
				if (IsLive) Apply();
			});
		}

		public override void RestoreAction()
		{
			_uiItem.isOn = _defaultVal; // on change CurrentValue will be changed
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
			_component.active = CurrentValue.ToBool();
		}
	}
}