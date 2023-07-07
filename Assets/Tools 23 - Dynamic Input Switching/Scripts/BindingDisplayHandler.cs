using System.Collections.Generic;
using UnityEngine;

namespace Assets.Tools_23_Dynamic_Input_Switching.Scripts
{
    public class BindingDisplayHandler : MonoBehaviour
    {
        public static BindingDisplayHandler Instance;

        [Header("Setup for sprites")]
        [SerializeField] private ListOfTmpSpriteAssetsSO _listOfTmpSpriteAssets;

        public List<BindingSpriteController> ListOfBindingSpriteFields = new List<BindingSpriteController>();

        private void Awake()
        {
            if (Instance == null)
                Instance = this;
            else
                Destroy(gameObject);
        }

        /// <summary>
        /// Subscribing to InputDevice change event from PlayerInput component & Initial update sprite call. 
        /// </summary>
        private void Start()
        {
            GameInput.Instance.PlayerInput.controlsChangedEvent.AddListener(x =>
            {
                UpdateBindingSprites();
            });
            foreach (var bindingSpriteController in ListOfBindingSpriteFields)
            {
                bindingSpriteController.ListofTmpSpriteAssets = _listOfTmpSpriteAssets;
            }
            UpdateBindingSprites();
        }

        /// <summary>
        /// Called by PlayerInput component on InputDeviceChange
        /// </summary>
        public void UpdateBindingSprites()
        {
            foreach (var bindingSpriteController in ListOfBindingSpriteFields)
            {
                bindingSpriteController.FetchBindingSprite();
            }
        }
    }
}