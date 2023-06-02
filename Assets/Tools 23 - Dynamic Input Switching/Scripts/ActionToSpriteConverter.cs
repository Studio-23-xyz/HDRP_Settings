using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Assets.Tools_23___Dynamic_Input_Switching.Scripts
{
    public static class ActionToSpriteConverter
    {
        public static string ReplaceBindingToSpriteText(string textToDisplay, InputBinding actionNeeded,
            TMP_SpriteAsset spriteAsset, string actionName, string deviceName = "")
        {
            Debug.Log($"InputBinding Action {actionNeeded.ToString()} & text to display {textToDisplay}");
            string stringBtnName = textToDisplay;
            stringBtnName = RenameInput(stringBtnName, actionName, deviceName);

            textToDisplay = textToDisplay.Replace(textToDisplay, $"<sprite=\"{spriteAsset.name}\" name=\"{stringBtnName}\">");
            return textToDisplay;
        }

        public static int GetBindingIndex(string deviceName, bool isComposite)
        {
            Debug.Log($"Device name received {deviceName}");
            if (string.Equals(deviceName, $"Keyboard") || string.Equals(deviceName, $"Mouse"))
                return 0;
            else if (string.Equals(deviceName, $"XInputControllerWindows"))
                return 1;
            else if (string.Equals(deviceName, $"DualShockGamepad"))
                return 1;
            return 0;
        }

        private static string RenameInput(string stringBtnName, string actionName, string deviceName)
        {
            string bindingDisplayString = "";
            if (string.Equals("Keyboard", deviceName) || string.Equals(deviceName, $"Mouse"))
                bindingDisplayString += $"Kb_";
            else if (string.Equals(deviceName, $"XInputControllerWindows"))
                bindingDisplayString += $"Xbox_";
            else if (string.Equals(deviceName, $"DualShockGamepad"))
                bindingDisplayString += $"PS_";
            bindingDisplayString += stringBtnName.ToLower();
            Debug.Log($"BindingSprite identifier {bindingDisplayString}");
            //stringBtnName = stringBtnName.Replace($"[{inputDeviceName}]", string.Empty);
            //stringBtnName = stringBtnName.Replace($"{actionName}:", string.Empty);
            //stringBtnName = stringBtnName.Replace($"<Keyboard>/", $"Kb_");
            //stringBtnName = stringBtnName.Replace($"<XInputController>/", $"Xbox_");
            //stringBtnName = stringBtnName.Replace($"<DualShockGamepad>/", $"PS_");
            //stringBtnName = stringBtnName.Replace($"[Xbox]", string.Empty);
            //stringBtnName = stringBtnName.Replace($"[PS]", string.Empty);
            return bindingDisplayString;
        }
    }
}
