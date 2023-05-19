using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace Studio23.Input.Rebinding
{
    public class RebindUI : MonoBehaviour
    {
        [SerializeField]
        private InputActionReference _inputActionReference;
        [SerializeField]
        private bool _excludeMouse = true;
        [Range(0, 10)]
        [SerializeField]
        private int _selectedBindingIndex;

        [SerializeField]
        private InputBinding.DisplayStringOptions _displayStringOptions;

        [Header("Binding Info")]
        [SerializeField] private InputBinding _inputBinding;
        private int _bindingIndex;

        [SerializeField] private string _actionName;

        [Header("UI Fields")]
        [SerializeField]
        private TextMeshProUGUI _actionText;
        [SerializeField]
        private TextMeshProUGUI _rebindText;
        [SerializeField]
        private Button _rebindButton;
        [SerializeField]
        private Button _resetButton;

        private void OnEnable()
        {
            _rebindButton.onClick.AddListener(() => DoRebind());
            _resetButton.onClick.AddListener(() => ResetBinding());

            if (_inputActionReference != null)
            {
                GetBindingInfo();
                UpdateUI();
            }

            InputManager.OnRebindComplete += UpdateUI;
            InputManager.OnRebindCanceled += UpdateUI;
        }

        public void SetInputActionReference(InputActionReference targetAction) => _inputActionReference = targetAction;

        private void OnDisable()
        {
            InputManager.OnRebindComplete -= UpdateUI;
            InputManager.OnRebindCanceled -= UpdateUI;
        }

        private void ResetBinding()
        {
            InputManager.ResetBinding(_actionName, _bindingIndex);
            UpdateUI();
        }

        private void DoRebind()
        {
            InputManager.StartRebinding(_actionName, _bindingIndex, _rebindText, _excludeMouse);
        }

        private void OnValidate()
        {
            if (_inputActionReference == null)
                return;
            GetBindingInfo();
            UpdateUI();
        }

        private void GetBindingInfo()
        {
            if (_inputActionReference.action != null)
                _actionName = _inputActionReference.action.name;

            if (_inputActionReference.action.bindings.Count > _selectedBindingIndex)
            {
                _inputBinding = _inputActionReference.action.bindings[_selectedBindingIndex];
                _bindingIndex = _selectedBindingIndex;
            }
        }

        private void UpdateUI()
        {
            if (_actionText != null)
                _actionText.text = _actionName;

            if (_rebindText != null)
            {
                if (Application.isPlaying)
                {
                    _rebindText.text = InputManager.GetBindingName(_actionName, _bindingIndex);
                }
                else
                {
                    _rebindText.text = _inputActionReference.action.GetBindingDisplayString(_bindingIndex);
                }
            }
        }
    }
}
