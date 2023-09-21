using Studio23.SS2.SettingsManager.Core;
using Studio23.SS2.SettingsManager.Core.Component;
using Studio23.SS2.SettingsManager.Extension;
using TMPro;
using UnityEngine;
using Slider = UnityEngine.UI.Slider;

namespace GameSettings
{
	[RequireComponent(typeof(Slider))]
	public class VolumeController : Settings
	{
		[SerializeField] private Slider uiItem;
		[SerializeField] private TMP_Text label;

		private AudioSettingsController _audioSettingsController;
		private AudioSetting audioSetting;
		private void OnEnable()
		{
			_audioSettingsController ??= FindObjectOfType<AudioSettingsController>();
			_audioSettingsController.ApplyAction += ApplyAction;
			_audioSettingsController.RestoreAction += RestoreAction;
		}

		private void OnDisable()
		{
			_audioSettingsController.ApplyAction -= ApplyAction;
			_audioSettingsController.RestoreAction -= RestoreAction;
		}

		public void Init(AudioSetting adoSetting)
		{
			audioSetting = adoSetting;
			Setup();
		}

		public override void Setup()
		{
			if (audioSetting == null) return;

			base.Initialized(audioSetting.DefaultValue, name, audioSetting.IsLive);
			uiItem.Init(CurrentValue.ToFloat());
			Apply();
		}

		private void Start()
		{

			label.text = FloatToText(audioSetting.DefaultValue, audioSetting.SettingsName);

			uiItem.onValueChanged.AddListener((value) =>
			{
				CurrentValue = value;
				if (isLive) Apply();
				label.text = FloatToText(value, audioSetting.SettingsName);
			});
		}

		private void RestoreAction()
		{
			uiItem.value = audioSetting.DefaultValue; // on change CurrentValue will be changed
			base.Save();
			if (!isLive) Apply(); // if Live then already applied this
		}
		private void ApplyAction()
		{
			base.Save();
			if (!isLive) Apply();  // if Live then already applied this
		}

		public void Apply()
		{
			audioSetting.AudioMixerGroup.audioMixer.SetFloat(audioSetting.ExposedParameter, CurrentValue.ToFloat().GetAttenuation());

		}


	}



}