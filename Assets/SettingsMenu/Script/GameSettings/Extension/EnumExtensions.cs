using System;
using TMPro;
using UnityEngine;

public static class EnumExtensions
{
    public static TMP_Dropdown.OptionData ToOptionData(this Enum enumValue)
    {
        return new TMP_Dropdown.OptionData(enumValue.ToString());
    }
}