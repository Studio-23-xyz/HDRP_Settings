using GameSettings;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

namespace Studio23.SS2.SettingsManager.Core
{
	[Serializable]
	public class AudioSetting
	{
		public string SettingsName = "Volume Settings";
		public bool IsLive = true;
		public AudioMixerGroup AudioMixerGroup;
		[Range(0, 1)]
		public float DefaultValue = 0.75f;
		public string ExposedParameter;
	}

	public class AudioSettingsController : SettingsController
	{
		[SerializeField] private List<AudioSetting> _audioSettings;
		[SerializeField] private Transform _containerTransform;
		[SerializeField] private VolumeController _volumeControllerTemplate;

		public override void Initialize()
		{
			base.Initialize();
			InitializedSubAudioSettings();
		}

		private void InitializedSubAudioSettings()
		{
			var i = 0;
			foreach (var audioSetting in _audioSettings)
			{
				var volumeController = Instantiate(_volumeControllerTemplate, _containerTransform);
				volumeController.name += i++;
				volumeController.Init(audioSetting);
			}
		}
	}

}