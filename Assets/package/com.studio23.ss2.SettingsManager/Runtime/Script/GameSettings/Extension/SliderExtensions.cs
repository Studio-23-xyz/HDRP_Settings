using UnityEngine.UI;

namespace Studio23.SS2.SettingsManager.Extensions
{
    public static class SliderExtensions
    {
        public static void Init(this Slider slider, float minValue, float maxValue, float value)
        {
            slider.minValue = minValue;
            slider.maxValue = maxValue;
            slider.value = value;
        }
        public static void Init(this Slider slider, float value)
        {
            slider.minValue = 0;
            slider.maxValue = 1;
            slider.value = value;
        }
    }
}