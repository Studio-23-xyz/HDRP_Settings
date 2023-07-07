using UnityEngine;
using UnityEngine.InputSystem;

namespace Assets.Tools_23_Dynamic_Input_Switching.Scripts
{
    public class GameInput : MonoBehaviour
    {
        public static GameInput Instance { get; private set; }
        [SerializeField] private PlayerInput _playerInput;
        private PlayerInputActions _playerInputActions;
        public PlayerInput PlayerInput => _playerInput;

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

            _playerInputActions.Ground.SwordAttack.performed += Action1Performed;
            _playerInputActions.Ground.ShieldAttack.performed += Action2Performed;
            _playerInputActions.Ground.Action3.performed += Action3Performed;
        }

        private void OnDestroy()
        {

            _playerInputActions.Ground.SwordAttack.performed -= Action1Performed;
            _playerInputActions.Ground.ShieldAttack.performed -= Action2Performed;
            _playerInputActions.Ground.Action3.performed -= Action3Performed;
        }

        private void Action2Performed(InputAction.CallbackContext obj)
        {
            EventActionHandler eventActionHandler = GetComponent<EventActionHandler>();
            if (eventActionHandler != null)
            {
                eventActionHandler.HandleInputAction(obj.action.name);

            }
        }
        private void Action3Performed(InputAction.CallbackContext obj)
        {
            EventActionHandler eventActionHandler = GetComponent<EventActionHandler>();
            if (eventActionHandler != null)
            {
                eventActionHandler.HandleInputAction(obj.action.name);

            }
        }

        private void Action1Performed(InputAction.CallbackContext obj)
        {
            EventActionHandler eventActionHandler = GetComponent<EventActionHandler>();
            if (eventActionHandler != null)
            {
                eventActionHandler.HandleInputAction(obj.action.name);
            }
        }

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
            else if (inputDeviceName.Contains("Dual"))
            {
                return 2;
            }
            return 0;
        }

        public string GetInputDeviceName()
        {
            return _playerInput.GetDevice<InputDevice>().name;
        }
    }
}