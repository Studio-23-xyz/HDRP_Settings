using TMPro;

namespace Studio23.SS2.SettingsManager.Utilities
{
	public static class TmpTextExtensions
	{
		public static void ShowText(this TMP_Text textComponent, string text)
		{
			if (textComponent != null)
			{
				textComponent.text = text;
			}
		}
	}
}