using System;
using UnityEngine.UI;

namespace Studio23.SS2.SettingsManager.Utilities
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
			slider.minValue = 0.001f;
			slider.maxValue = 1f;
			slider.value = value;
		}

		public static string FloatToText(float value, string label)
		{
			return $"{label} ({Math.Round(value * 100)}%)";
		}
	}
}