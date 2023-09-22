using System;
using System.IO;
using Newtonsoft.Json;
using Studio23.SS2.SettingsManager.Data;
using UnityEngine;

namespace Studio23.SS2.SettingsManager.Core.Component
{
    public abstract class Settings : MonoBehaviour
    {
	    protected VideoSettingsController VideoSettingsController;
		protected object CurrentValue { get; set; }
        protected bool isLive { get; private set; }
        private object defaultValue = 0 ;
        private string settingsPath;

        public virtual void OnEnable()
        {
            VideoSettingsController = FindObjectOfType<VideoSettingsController>();
            VideoSettingsController.ApplyAction += ApplyAction;
            VideoSettingsController.RestoreAction += RestoreAction;
            VideoSettingsController.QualityChangedAction += OnQualityChanged;
        }

        protected virtual void OnQualityChanged(QualityName obj)
        {

        }

        public virtual void OnDisable()
        {
	        VideoSettingsController.ApplyAction -= ApplyAction;
	        VideoSettingsController.RestoreAction -= RestoreAction;
	        VideoSettingsController.QualityChangedAction -= OnQualityChanged;
		}

		public abstract void Setup();
        public virtual void Initialized(object defVal, string dbName, bool isLiveValue = false)
        {
            defaultValue = defVal;
            isLive = isLiveValue;
            settingsPath =  Path.Combine(Application.persistentDataPath, $"{dbName}.config");
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
        
        protected string FloatToText(float value, string label)
        {
            return $"{label} ({Math.Round(value * 100)}%)";
        }
    }
}