using TMPro;

public static class TMP_TextExtensions
{
    public static void ShowText(this TMP_Text textComponent, string text)
    {
        if (textComponent != null)
        {
            textComponent.text = text;
        }
    }
}