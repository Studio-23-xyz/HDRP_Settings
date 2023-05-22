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

        public static void StartRebinding(string actionName, int bindingIndex, TextMeshProUGUI statusText, bool excludeMouse)
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
                    actionToRebind.bindings[firstPartIndex].isPartOfComposite)
                {
                    DoRebind(actionToRebind, firstPartIndex, statusText, true, excludeMouse);
                }
            }
            else
            {
                DoRebind(actionToRebind, bindingIndex, statusText, false, excludeMouse);
            }
        }

        private static void DoRebind(InputAction actionToRebind, int bindingIndex, TextMeshProUGUI statusText, bool isComposite, bool excludeMouse)
        {
            if (actionToRebind == null || bindingIndex < 0)
                return;

            if (actionToRebind.bindings[bindingIndex].isPartOfComposite)
                statusText.text = $"Binding {actionToRebind.bindings[bindingIndex].name}";
            else
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
                    if (nextBindingIndex < actionToRebind.bindings.Count && actionToRebind.bindings[nextBindingIndex].isPartOfComposite)
                        DoRebind(actionToRebind, nextBindingIndex, statusText, isComposite, excludeMouse);
                }

                OnRebindComplete?.Invoke();
            });

            rebind.OnCancel(operation =>
            {
                actionToRebind.Enable();
                operation.Dispose();

                OnRebindCanceled?.Invoke();
            });

            rebind.WithCancelingThrough($"<Keyboard>/escape"); // -.-

            if (excludeMouse)
                rebind.WithControlsExcluding($"Mouse");


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

        private static void SaveBindingOverride(InputAction action)
        {
            for (var index = 0; index < action.bindings.Count; index++)
            {
                PlayerPrefs.SetString(action.actionMap + action.name + index, action.bindings[index].overridePath);
            }
        }

        public static void LoadBindingOverride(string actionName)
        {
            InputAction action = InputControlAsset.asset.FindAction(actionName);

            for (int i =0; i < action.bindings.Count; i++)
            {
                if (!string.IsNullOrEmpty(PlayerPrefs.GetString(action.actionMap + action.name + i)))
                    action.ApplyBindingOverride(i, PlayerPrefs.GetString(action.actionMap + action.name + i));
            }
        }

        public static void ResetBinding(string actionName, int bindingIndex)
        {
            InputAction action = InputControlAsset.asset.FindAction(actionName);

            if (action == null || action.bindings.Count <= bindingIndex)
            {
                Debug.Log($"<color=ffc55a>Desired binding or action not found.</color>");
                return;
            }

            if (action.bindings[bindingIndex].isComposite)
            {
                for (int i = bindingIndex; i < action.bindings.Count && action.bindings[i].isComposite; i++)
                    action.RemoveBindingOverride(i);
            }
            else 
                action.RemoveBindingOverride(bindingIndex);
        }

        public static void ResetAllBindings(string actionName)
        {
            InputAction action = InputControlAsset.asset.FindAction(actionName);

            action.RemoveAllBindingOverrides();
        }
    }
}