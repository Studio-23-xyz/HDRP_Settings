using System;
using UnityEngine;
using UnityEngine.UI;

namespace GameSettings
{
    public class AudioSettingsController : MonoBehaviour
    {
        public Action ApplyAction;
        public Action RestoreAction;

        [SerializeField] private Button applyButton;
        [SerializeField] private Button restoreButton;

        private void Start()
        {
           
                applyButton.onClick.AddListener(ApplyAction.Invoke);
                restoreButton.onClick.AddListener(RestoreAction.Invoke);
        }
    }
    
}