using System.Collections.Generic;
using System.Linq;
using com.studio23.ss2.Core.Component;
using UnityEngine;

namespace com.studio23.ss2.Core
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