using TMPro;

namespace Assets.Tools_23_Dynamic_Input_Switching.Scripts
{
    public static class ActionToSpriteConverter
    {
        /// <summary>
        /// Public wrapper to return the desired formatted string which TextMeshPro will then convert to the desired sprite asset. 
        /// </summary>
        /// <param name="textToDisplay">Button that is bound to the InputAction we are currently dealing with.</param>
        /// <param name="spriteAsset">The sprite asset that we will use to find the sprite from.</param>
        /// <param name="deviceName">To compare and use the required suffix to get the sprite from the intended InputDevice.</param>
        /// <returns>Returns the TMP Sprite asset formatted string that will fetch the sprite from the desired sprite sheet.</returns>
        public static string ReplaceBindingToSpriteText(string textToDisplay,
            TMP_SpriteAsset spriteAsset, string deviceName)
        {
            string stringBtnName = textToDisplay;
            stringBtnName = RenameInput(stringBtnName, deviceName);

            textToDisplay = textToDisplay.Replace(textToDisplay, $"<sprite=\"{spriteAsset.name}\" name=\"{stringBtnName}\">");
            return textToDisplay;
        }

        /// <summary>
        /// Generates a string format for the desired sprite corresponding to the spritesheet
        /// </summary>
        /// <param name="stringBtnName">The binding button of the given InputAction</param>
        /// <param name="deviceName">Device name is derived from the InputAction name as the entire string contains required InputDevice when set to different control schemes from the InputAction asset</param>
        /// <returns>Renames the action string formatted for TMP Sprite Asset readability.</returns>
        private static string RenameInput(string stringBtnName, string deviceName)
        {
            string bindingDisplayString = "";
            if (deviceName.Contains($"Keyboard") || deviceName.Contains($"Mouse"))
                bindingDisplayString += $"Kb_";
            else if (deviceName.Contains($"XInput"))
                bindingDisplayString += $"Xbox_";
            else if (deviceName.Contains($"DualShock"))
                bindingDisplayString += $"PS_";
            bindingDisplayString += stringBtnName.ToLower();
            bindingDisplayString = bindingDisplayString.Replace($" ", "");
            return bindingDisplayString;
        }
    }
}
