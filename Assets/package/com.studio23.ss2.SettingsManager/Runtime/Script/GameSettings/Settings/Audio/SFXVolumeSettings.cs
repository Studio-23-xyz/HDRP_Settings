using Studio23.SS2.SettingsManager.Core;
using Studio23.SS2.SettingsManager.Core.Component;
using Studio23.SS2.SettingsManager.Extensions;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

namespace Studio23.SS2.SettingsManager.Video
{
	[RequireComponent(typeof(Slider))]
	public class SFXVolumeSettings : Settings
	{
		[SerializeField] private Slider uiItem;

		[Range(0, 1)]
		[SerializeField] private float defaultVal = .75f;
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
			if (!isLive) Apply();  // if Live then already applied this
		}

		public void Apply()
		{
			_audioMixerGroup.audioMixer.SetFloat("SFXVol", CurrentValue.ToFloat().GetAttenuation());
		}
	}
}