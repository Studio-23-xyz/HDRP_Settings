using System;
using TMPro;

namespace Studio23.SS2.SettingsManager.Utilities
{
	public static class EnumExtensions
	{
		public static TMP_Dropdown.OptionData ToOptionData(this Enum enumValue)
		{
			return new TMP_Dropdown.OptionData(enumValue.ToString());
		}
	}
}