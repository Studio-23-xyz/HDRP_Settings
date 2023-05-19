using System;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Studio23.Input
{
    public class InputManager : MonoBehaviour
    {
        public static PlayerControls InputControlAsset;

        public static event Action OnRebindComplete;
        public static event Action OnRebindCanceled;
        public static event Action<InputAction, int> OnRebindStart;

        private void Awake()
        {
            if (InputControlAsset == null)
            {
                InputControlAsset = new PlayerControls();
            }
        }

        public static void StartRebinding(string actionName, int bindingIndex, TextMeshProUGUI statusText)
        {
            InputAction actionToRebind = InputControlAsset.asset.FindAction(actionName);
            if (actionToRebind == null || actionToRebind.bindings.Count <= bindingIndex)
            {
                Debug.Log($"<color=ffd700>Action / binding not found.</color>");
                return;
            }

            if (actionToRebind.bindings[bindingIndex].isComposite)
            {
                var firstPartIndex = bindingIndex + 1;
                if (firstPartIndex < actionToRebind.bindings.Count &&
                    actionToRebind.bindings[firstPartIndex].isComposite)
                {
                    DoRebind(actionToRebind, firstPartIndex, statusText, true);
                }
            }
            else
            {
                DoRebind(actionToRebind, bindingIndex, statusText, false);
            }
        }

        private static void DoRebind(InputAction actionToRebind, int bindingIndex, TextMeshProUGUI statusText, bool isComposite)
        {
            if (actionToRebind == null || bindingIndex < 0)
                return;

            statusText.text = $"Press {actionToRebind.expectedControlType}";
            actionToRebind.Disable();

            var rebind = actionToRebind.PerformInteractiveRebinding(bindingIndex);

            rebind.OnComplete(operation =>
            {
                actionToRebind.Enable();
                operation.Dispose();

                if (isComposite)
                {
                    var nextBindingIndex = bindingIndex + 1;
                    if (nextBindingIndex < actionToRebind.bindings.Count && actionToRebind.bindings[nextBindingIndex].isComposite)
                        DoRebind(actionToRebind, nextBindingIndex, statusText, isComposite);
                }

                OnRebindComplete?.Invoke();
            });

            rebind.OnCancel(operation =>
            {
                actionToRebind.Enable();
                operation.Dispose();

                OnRebindCanceled?.Invoke();
            });

            OnRebindStart?.Invoke(actionToRebind, bindingIndex);
            rebind.Start();
        }

        public static string GetBindingName(string actionName, int bindingIndex)
        {
            if (InputControlAsset == null)
                InputControlAsset = new PlayerControls();

            InputAction action = InputControlAsset.asset.FindAction(actionName);
            return action.GetBindingDisplayString(bindingIndex);
        }
    }
}