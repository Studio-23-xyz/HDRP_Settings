using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;

public static class TMP_DropdownExtensions
{
    public static void AddOptionNew(this TMP_Dropdown dropdown, List<TMP_Dropdown.OptionData> options)
    {
        dropdown.ClearOptions();
        dropdown.AddOptions(options);
         dropdown.RefreshShownValue();
    }
}