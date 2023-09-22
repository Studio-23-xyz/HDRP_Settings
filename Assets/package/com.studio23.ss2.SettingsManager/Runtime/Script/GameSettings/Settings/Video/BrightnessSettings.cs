using Studio23.SS2.SettingsManager.Core;
using Studio23.SS2.SettingsManager.Core.Component;
using Studio23.SS2.SettingsManager.Extension;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.HighDefinition;
using UnityEngine.UI;

namespace GameSettings
{
	[RequireComponent(typeof(Slider))]
	public class BrightnessSettings : Settings
	{
		[SerializeField] private Slider uiItem;

		private VolumeProfile data;
		private ColorAdjustments component;

		[SerializeField] private float minVal = -3f;
		[SerializeField] private float maxVal = 1f;

		[SerializeField] private float defaultVal = -1f;
		[SerializeField] private TMP_Text label;

		public override void Setup()
		{
			data = FindObjectsOfType<Volume>().OrderBy(m => m.transform.GetSiblingIndex()).ToArray()[0]
				.sharedProfile; //FindObjectOfType<Volume>();
			data.TryGet(typeof(ColorAdjustments), out component);
			base.Initialized(defaultVal, GetType().Name, true);

			Apply();
		}

		private void Start()
		{
			uiItem.Init(minVal, maxVal, CurrentValue.ToFloat());

			label.text = FloatToText(defaultVal, gameObject.name);

			uiItem.onValueChanged.AddListener((value) =>
			{
				CurrentValue = value;
				if (isLive) Apply();
				label.text = FloatToText(value, gameObject.name);
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
			if (!isLive) Apply(); // if Live then already applied this
		}

		public void Apply()
		{
			component.postExposure.value = Mathf.Clamp(CurrentValue.ToFloat(), minVal, maxVal);
		}
	}
}