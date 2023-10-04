using Studio23.SS2.SettingsManager.Core;
using Studio23.SS2.SettingsManager.Utilities;
using TMPro;
using UnityEngine;
using Slider = UnityEngine.UI.Slider;

namespace Studio23.SS2.SettingsManager.Audio
{
	[RequireComponent(typeof(Slider))]
	public class VolumeController : Settings
	{
		[SerializeField] private Slider _volumeSlider;
		[SerializeField] private TMP_Text _sliderLabel;
		private AudioSetting _audioSetting;

		public void Init(AudioSetting adoSetting)
		{
			_audioSetting = adoSetting;
			Setup();
		}

		public override void Setup()
		{
			if (_audioSetting == null) return;

			base.Initialized(_audioSetting.DefaultValue, name, _audioSetting.IsLive);
			_volumeSlider.Init(CurrentValue.ToFloat());
			Apply();
			string volumePercentage = SliderExtensions.FloatToText(CurrentValue.ToFloat(), _audioSetting.SettingsName);
			Debug.Log($"Float to text result {volumePercentage}");
			_sliderLabel.text = volumePercentage;
		}

		private void Start()
		{
			_volumeSlider.onValueChanged.AddListener((value) =>
			{
				CurrentValue = value;
				if (IsLive) Apply();
				_sliderLabel.text = SliderExtensions.FloatToText(value, _audioSetting.SettingsName);
			});
		}

		public override void RestoreAction()
		{
			_volumeSlider.value = _audioSetting.DefaultValue; // on change CurrentValue will be changed
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
			_audioSetting.AudioMixerGroup.audioMixer.SetFloat(_audioSetting.ExposedParameter, Mathf.Log10(CurrentValue.ToFloat())*20);
		}
	}
}