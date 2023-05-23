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

namespace GameSettings
{
    
   
    public abstract class Settings : MonoBehaviour
    {
         
        protected object defaultValue = 0 ;
        [SerializeField] protected bool isLive;
        protected object currentValue ;
        private string settingsPath;

      
        public abstract void Setup();
        public virtual void Initialized()
        {
            
            settingsPath =  Path.Combine(Application.persistentDataPath, $"{gameObject.name}.config");
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
        
        protected string FloatToText(float value)
        {
            return $"{gameObject.name} ({Math.Round(value * 100)}%)";
        }

        
    }

}