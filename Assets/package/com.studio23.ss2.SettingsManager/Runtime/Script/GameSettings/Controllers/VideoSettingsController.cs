using Studio23.SS2.SettingsManager.Data;
using System;
using System.Collections.Generic;

namespace Studio23.SS2.SettingsManager.Core
{
	public class VideoSettingsController : SettingsController
	{
		public List<QualitySetting> QualitySettingsPreset = new List<QualitySetting>();

		public Action<QualityName> QualityChangedAction;
	}
}