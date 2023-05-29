using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Tools_23___Dynamic_Input_Switching.Scripts
{
    [CreateAssetMenu(fileName = "New Combo"), Serializable]
    public class ComboStringSO : ScriptableObject
    {
        public string ComboName;
        public List<InputType> InputTypes = new List<InputType>();
    }
}