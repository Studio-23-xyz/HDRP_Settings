using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Assets.Tools_23___Dynamic_Input_Switching.Scripts
{
    public class GameInput : MonoBehaviour
    {
        public static GameInput Instance { get; private set; }

        public event EventHandler<CustomEventHandler> OnAction1;
        public event EventHandler<CustomEventHandler> OnAction2;
        public event EventHandler<CustomEventHandler> OnAction3;

        [SerializeField] private PlayerInput _playerInput;
        private PlayerInputActions _playerInputActions;

        public class CustomEventHandler
        {
            public string DisplayString;
        }

        private void Awake()
        {
            Instance = this;

            _playerInputActions = new PlayerInputActions();
            _playerInputActions.Ground.Enable();
            SubscribeToEvents();
        }

        private void Start()
        {
            _playerInput = FindObjectOfType<PlayerInput>();
        }

        private void SubscribeToEvents()
        {
        
            _playerInputActions.Ground.Action1.performed += Action1Performed;
            _playerInputActions.Ground.Action2.performed += Action2Performed;
            _playerInputActions.Ground.Action3.performed += Action3Performed;
        }

        private void OnDestroy()
        {
        
            _playerInputActions.Ground.Action1.performed -= Action1Performed;
            _playerInputActions.Ground.Action2.performed -= Action2Performed;
            _playerInputActions.Ground.Action3.performed -= Action3Performed;
        }

        private void Action2Performed(InputAction.CallbackContext obj)
        {
            OnAction2?.Invoke(this, new CustomEventHandler
            {
                DisplayString = GetBindingText(InputType.Action2)
            });
            EventActionHandler eventActionHandler = GetComponent<EventActionHandler>();
            if (eventActionHandler != null)
            {
                eventActionHandler.HandleInputAction(obj.action.name);

            }
        }
        private void Action3Performed(InputAction.CallbackContext obj)
        {
            OnAction3?.Invoke(this, new CustomEventHandler
            {
                DisplayString = GetBindingText(InputType.Action2)
            });
            EventActionHandler eventActionHandler = GetComponent<EventActionHandler>();
            if (eventActionHandler != null)
            {
                eventActionHandler.HandleInputAction(obj.action.name);

            }
        }

        private void Action1Performed(InputAction.CallbackContext obj)
        {
            OnAction1?.Invoke(this, new CustomEventHandler
            {
                DisplayString = GetBindingText(InputType.Action1)
            });

            EventActionHandler eventActionHandler = GetComponent<EventActionHandler>();
            if (eventActionHandler != null)
            {
                eventActionHandler.HandleInputAction(obj.action.name);

            }
        }

        public string GetBindingText(InputType inputType)
        {
            switch (inputType)
            {
                case InputType.Action1:
                    return GetDeviceBoundInputControl(_playerInputActions.Ground.Action1);
                case InputType.Action2:
                    return GetDeviceBoundInputControl(_playerInputActions.Ground.Action2);
                case InputType.Action3:
                    return GetDeviceBoundInputControl(_playerInputActions.Ground.Action3);
            }

            return $"Null";
        }

        private string GetDeviceBoundInputControl(InputAction action)
        {
            int deviceIndex = GetInputDeviceIndex();
            return action.bindings[deviceIndex].ToString();
        }

        //TODO remove string dependency & compare active device in a better way
        public int GetInputDeviceIndex()
        {
            string inputDeviceName = GetInputDeviceName();

            if (inputDeviceName.Contains("Keyboard"))
            {
                return 0;
            }
            else if (inputDeviceName.Contains("XInput"))
            {
                return 1;
            }
            else
            {
                return 1;
            }

            return 0;
        }

        public string GetInputDeviceName()
        {
            return _playerInput.GetDevice<InputDevice>().name;
        }

  
    }
}