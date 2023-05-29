using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Assets.Tools_23___Dynamic_Input_Switching.Scripts
{
    public class BindingDisplayHandler : MonoBehaviour
    {
        public static BindingDisplayHandler Instance;


        [Header("Setup for sprites")]
        [SerializeField] private ListOfTmpSpriteAssetsSO _listOfTmpSpriteAssets;
        [SerializeField] private DeviceType _deviceType;

        private PlayerInputActions _playerInputActions;
        public RebindAction RebindElement;


        private void Awake()
        {
            Instance = this;
            _playerInputActions = new PlayerInputActions();
        }

        public string GetBindingSprite(string bindingPath, InputAction action)
        {
            return ActionToSpriteConverter.ReplaceBindingToSpriteText(bindingPath,
                action.bindings[(int)_deviceType],
                _listOfTmpSpriteAssets.SpriteAssets[(int)_deviceType],
                action.name);
        }

        public string GetBindingSprite(string bindingPath, InputType inputType)
        {
            string spriteText = String.Empty;
            _deviceType = (DeviceType)GameInput.Instance.GetInputDeviceIndex();
            switch (inputType)
            {
                case InputType.Action1:
                    return ActionToSpriteConverter.ReplaceBindingToSpriteText(bindingPath,
                        _playerInputActions.Ground.Action1.bindings[(int)_deviceType],
                        _listOfTmpSpriteAssets.SpriteAssets[(int)_deviceType], _playerInputActions.Ground.Action1.name);
                case InputType.Action2:
                    return  ActionToSpriteConverter.ReplaceBindingToSpriteText(bindingPath,
                        _playerInputActions.Ground.Action2.bindings[(int)_deviceType],
                        _listOfTmpSpriteAssets.SpriteAssets[(int)_deviceType], _playerInputActions.Ground.Action2.name);
                case InputType.Action3:
                    return  ActionToSpriteConverter.ReplaceBindingToSpriteText(bindingPath,
                        _playerInputActions.Ground.Action3.bindings[(int)_deviceType],
                        _listOfTmpSpriteAssets.SpriteAssets[(int)_deviceType], _playerInputActions.Ground.Action3.name);
                default:
                    return string.Empty;
            }

            return string.Empty;
        }
    }
}