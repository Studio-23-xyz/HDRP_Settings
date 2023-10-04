using Newtonsoft.Json;
using Studio23.SS2.SettingsManager.Data;
using System;
using System.IO;
using UnityEngine;

namespace Studio23.SS2.SettingsManager.Core.Component
{
	public abstract class Settings : MonoBehaviour
	{
		protected VideoSettingsController VideoSettingsController;
		protected AudioSettingsController AudioSettingsController;

		protected object CurrentValue { get; set; }
		protected bool isLive { get; private set; }
		private object defaultValue = 0;
		private string settingsPath;

		public virtual void OnEnable()
		{
			VideoSettingsController = FindObjectOfType<VideoSettingsController>();
			if (VideoSettingsController != null)
			{
				VideoSettingsController.ApplyAction += ApplyAction;
				VideoSettingsController.RestoreAction += RestoreAction;
				VideoSettingsController.QualityChangedAction += OnQualityChanged;
			}

			AudioSettingsController = FindObjectOfType<AudioSettingsController>();
			if (AudioSettingsController != null)
			{
				AudioSettingsController.ApplyAction += ApplyAction;
				AudioSettingsController.RestoreAction += RestoreAction;
			}
		}

		protected virtual void OnQualityChanged(QualityName obj)
		{

		}

		public virtual void OnDisable()
		{
			if (VideoSettingsController != null)
			{
				VideoSettingsController.ApplyAction -= ApplyAction;
				VideoSettingsController.RestoreAction -= RestoreAction;
				VideoSettingsController.QualityChangedAction -= OnQualityChanged;
			}

			if (AudioSettingsController != null)
			{
				AudioSettingsController.ApplyAction -= ApplyAction;
				AudioSettingsController.RestoreAction -= RestoreAction;
			}
		}

		public abstract void Setup();
		public virtual void Initialized(object defVal, string dbName, bool isLiveValue = false)
		{
			defaultValue = defVal;
			isLive = isLiveValue;
			settingsPath = Path.Combine(Application.persistentDataPath, $"{dbName}.config");
			if (!File.Exists(settingsPath))
			{
				CurrentValue = defaultValue;
				Save();
			}
			else Select();
		}

		public abstract void ApplyAction();
		public abstract void RestoreAction();

		public void Select()
		{
			CurrentValue = LoadValue();
		}

		public virtual void Save()
		{
			var contents = JsonConvert.SerializeObject(CurrentValue);
			File.WriteAllText(settingsPath, contents);
		}

		private object LoadValue()
		{
			var json = File.ReadAllText(settingsPath);
			return JsonConvert.DeserializeObject<object>(json);
		}
	}
}