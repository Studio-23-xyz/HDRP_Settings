using Studio23.SS2.SettingsManager.Core;
using Studio23.SS2.SettingsManager.Utilities;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.HighDefinition;
using UnityEngine.UI;

namespace Studio23.SS2.SettingsManager.Video
{
	[RequireComponent(typeof(Slider))]
	public class BrightnessSettings : Settings
	{
		[SerializeField] private Slider _uiItem;

		private VolumeProfile _data;
		private ColorAdjustments _component;

		[SerializeField] private float _minVal = -3f;
		[SerializeField] private float _maxVal = 1f;

		[SerializeField] private float _defaultVal = -1f;
		[SerializeField] private TMP_Text _label;

		public override void Setup()
		{
			_data = FindObjectsOfType<Volume>().OrderBy(m => m.transform.GetSiblingIndex()).ToArray()[0]
				.sharedProfile; //FindObjectOfType<Volume>();
			_data.TryGet(typeof(ColorAdjustments), out _component);
			base.Initialized(_defaultVal, GetType().Name, true);

			Apply();
		}

		private void Start()
		{
			_uiItem.Init(_minVal, _maxVal, CurrentValue.ToFloat());

			_label.text = SliderExtensions.FloatToText(_defaultVal, gameObject.name);

			_uiItem.onValueChanged.AddListener((value) =>
			{
				CurrentValue = value;
				if (IsLive) Apply();
				_label.text = SliderExtensions.FloatToText(value, gameObject.name);
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
			if (!IsLive) Apply(); // if Live then already applied this
		}

		public void Apply()
		{
			_component.postExposure.value = Mathf.Clamp(CurrentValue.ToFloat(), _minVal, _maxVal);
		}
	}
}