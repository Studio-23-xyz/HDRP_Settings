using System;
using System.IO;
using Newtonsoft.Json;
using UnityEngine;

namespace Studio23.SS2.SettingsManager.Core.Component
{
    public abstract class Settings : MonoBehaviour
    {
        protected object CurrentValue { get; set; }
        protected bool isLive { get; private set; }
        private object defaultValue = 0 ;
        private string settingsPath;

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