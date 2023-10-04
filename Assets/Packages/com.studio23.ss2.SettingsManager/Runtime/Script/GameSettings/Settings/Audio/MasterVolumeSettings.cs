using Studio23.SS2.SettingsManager.Core.Component;
using Studio23.SS2.SettingsManager.Extensions;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using Slider = UnityEngine.UI.Slider;

namespace Studio23.SS2.SettingsManager.Audio
{
	[RequireComponent(typeof(Slider))]
	public class MasterVolumeSettings : Settings
	{
		[SerializeField] private Slider uiItem;

		[Range(0, 1)]
		[SerializeField] private float defaultVal = 0.75f;
		[SerializeField] private TMP_Text label;
		[SerializeField] private AudioMixerGroup _audioMixerGroup;

		public override void Setup()
		{
			base.Initialized(defaultVal, GetType().Name);
			Apply();
		}

		private void Start()
		{
			uiItem.Init(CurrentValue.ToFloat());
			label.text = SliderExtensions.FloatToText(defaultVal, gameObject.name);
			uiItem.onValueChanged.AddListener((value) =>
			{
				CurrentValue = value;
				if (isLive) Apply();
				label.text = SliderExtensions.FloatToText(value, gameObject.name);
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
			_audioMixerGroup.audioMixer.SetFloat("MasterVol", CurrentValue.ToFloat().GetAttenuation());
		}
	}
}