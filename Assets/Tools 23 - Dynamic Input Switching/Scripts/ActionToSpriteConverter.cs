using TMPro;
using UnityEngine.InputSystem;

namespace Assets.Tools_23___Dynamic_Input_Switching.Scripts
{
    public static class ActionToSpriteConverter
    {
        public static string ReplaceBindingToSpriteText(string textToDisplay, InputBinding actionNeeded,
            TMP_SpriteAsset spriteAsset, string actionName, string deviceName = "")
        {
            string stringBtnName = actionNeeded.ToString();
            stringBtnName = RenameInput(stringBtnName, actionName, deviceName);

            textToDisplay = textToDisplay.Replace(textToDisplay, $"<sprite=\"{spriteAsset.name}\" name=\"{stringBtnName}\">");
            return textToDisplay;
        }

        public static int GetBindingIndex(string deviceName, bool isComposite)
        {
            if (string.Equals(deviceName, $"Keyboard"))
                return 0;
            else if (string.Equals(deviceName, $"XInputControllerWindows"))
                return 1;
            else if (string.Equals(deviceName, $"DualShockGamepad"))
                return 2;
            return 0;
        }

        private static string RenameInput(string stringBtnName, string actionName, string deviceName)
        {
            string inputDeviceName = deviceName;
            stringBtnName = stringBtnName.Replace($"[{inputDeviceName}]", string.Empty);
            stringBtnName = stringBtnName.Replace($"{actionName}:", string.Empty);
            stringBtnName = stringBtnName.Replace($"<Keyboard>/", $"Kb_");
            stringBtnName = stringBtnName.Replace($"<XInputController>/", $"Xbox_");
            stringBtnName = stringBtnName.Replace($"<DualShockGamepad>/", $"PS_");
            stringBtnName = stringBtnName.Replace($"[Xbox]", string.Empty);
            stringBtnName = stringBtnName.Replace($"[PS]", string.Empty);
            return stringBtnName;
        }
    }
}
