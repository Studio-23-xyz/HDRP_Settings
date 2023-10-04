using System.Collections.Generic;
using TMPro;

namespace Studio23.SS2.SettingsManager.Utilities
{
	public static class TmpDropdownExtensions
	{
		public static void AddOptionNew(this TMP_Dropdown dropdown, List<TMP_Dropdown.OptionData> options)
		{
			dropdown.ClearOptions();
			dropdown.AddOptions(options);
			dropdown.RefreshShownValue();
		}
	}
}