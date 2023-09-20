using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace com.studio23.ss2.Core.Component
{
    public abstract class Settings : MonoBehaviour
    {
        protected object currentValue { get; set; }
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
                currentValue = defaultValue;
                Save();
            }
            else Select();
            
        }

        public void Select()
        {
            currentValue = LoadValue();
        }
       

        public virtual void Save()
        {
            var contents = JsonConvert.SerializeObject(currentValue);
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