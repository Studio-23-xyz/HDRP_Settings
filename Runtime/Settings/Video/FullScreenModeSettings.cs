using Studio23.SS2.SettingsManager.Core;
using Studio23.SS2.SettingsManager.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;

namespace Studio23.SS2.SettingsManager.Video
{
	[RequireComponent(typeof(TMP_Dropdown))]
	public class FullScreenModeSettings : Settings
	{
		private List<FullScreenMode> FullscreenModes { get; set; } = new List<FullScreenMode>();
		[SerializeField] private TMP_Dropdown _uiItem;
		[SerializeField] private FullScreenMode _defaultVal;

		public override void Setup()
		{
			if (!FullscreenModes.Any()) GenerateOptions();
			base.Initialized((int)_defaultVal, GetType().Name);
			Apply();
		}

		private void Start()
		{
			if (!FullscreenModes.Any()) return;
			_uiItem.AddOptionNew(GetOptions());
			_uiItem.value = CurrentValue.ToInt();

			_uiItem.onValueChanged.AddListener((value) =>
			{
				CurrentValue = value;
				if (IsLive) Apply();
			});
		}

		public override void RestoreAction()
		{
			_uiItem.value = (int)_defaultVal; // on change CurrentValue will be changed
			base.Save();
			if (!IsLive) Apply(); // if Live then already applied this
		}

		public override void ApplyAction()
		{
			base.Save();
			if (!IsLive) Apply();  // if Live then already applied this
		}

		public void Apply()
		{
			var setting = FullscreenModes[CurrentValue.ToInt()];
			Screen.fullScreenMode = setting;
		}

		private void GenerateOptions()
		{
			FullscreenModes = new List<FullScreenMode>();
			var x = 0;
			foreach (FullScreenMode fullScreenMode in Enum.GetValues(typeof(FullScreenMode)))
			{
				if (x != 2) FullscreenModes.Add(fullScreenMode);
				x++;
			}
		}

		private List<TMP_Dropdown.OptionData> GetOptions()
		{
			return FullscreenModes.Select(x => Regex.Replace(x.ToString(), "([a-z])([A-Z])", "$1 $2"))
				.Select(newVal => new TMP_Dropdown.OptionData(newVal)).ToList();
		}

		public FullScreenMode Get()
		{
			if (!FullscreenModes.Any()) GenerateOptions();
			return FullscreenModes[CurrentValue.ToInt()];
		}
	}
}