using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace GameSettings
{
    public class OtherSettingsController : MonoBehaviour
    {
        public List<Settings> settings;

        public void Initialized()
        {
            settings = GetComponentsInChildren<Settings>(true).ToList();
            settings.ForEach(setting => setting.Setup());
        }
    }
}