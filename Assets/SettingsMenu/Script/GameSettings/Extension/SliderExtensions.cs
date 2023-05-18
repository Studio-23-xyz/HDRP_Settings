using UnityEngine.UI;

namespace GameSettings
{
    public static class SliderExtensions
    {
        public static void Init(this Slider slider, float minValue, float maxValue, float value)
        {
            slider.minValue = minValue;
            slider.maxValue = maxValue;
            slider.value = value;
        }
    }
}