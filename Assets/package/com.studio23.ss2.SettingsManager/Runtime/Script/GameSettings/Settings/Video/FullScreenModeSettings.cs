using Studio23.SS2.SettingsManager.Core.Component;
using Studio23.SS2.SettingsManager.Extensions;
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
		private List<FullScreenMode> _fullscreenModes { get; set; } = new List<FullScreenMode>();
		[SerializeField] private TMP_Dropdown uiItem;
		[SerializeField] private FullScreenMode defaultVal;

		public override void Setup()
		{
			if (!_fullscreenModes.Any()) GenerateOptions();
			base.Initialized((int)defaultVal, GetType().Name);
			Apply();
		}

		private void Start()
		{
			if (!_fullscreenModes.Any()) return;
			uiItem.AddOptionNew(GetOptions());
			uiItem.value = CurrentValue.ToInt();

			uiItem.onValueChanged.AddListener((value) =>
			{
				CurrentValue = value;
				if (isLive) Apply();
			});
		}

		public override void RestoreAction()
		{
			uiItem.value = (int)defaultVal; // on change CurrentValue will be changed
			base.Save();
			if (!isLive) Apply(); // if Live then already applied this
		}

		public override void ApplyAction()
		{
			base.Save();
			if (!isLive) Apply();  // if Live then already applied this
		}

		public void Apply()
		{
			var setting = _fullscreenModes[CurrentValue.ToInt()];
			Screen.fullScreenMode = setting;
		}

		private void GenerateOptions()
		{
			_fullscreenModes = new List<FullScreenMode>();
			var x = 0;
			foreach (FullScreenMode fullScreenMode in Enum.GetValues(typeof(FullScreenMode)))
			{
				if (x != 2) _fullscreenModes.Add(fullScreenMode);
				x++;
			}
		}

		private List<TMP_Dropdown.OptionData> GetOptions()
		{
			return _fullscreenModes.Select(x => Regex.Replace(x.ToString(), "([a-z])([A-Z])", "$1 $2"))
				.Select(newVal => new TMP_Dropdown.OptionData(newVal)).ToList();
		}

		public FullScreenMode Get()
		{
			if (!_fullscreenModes.Any()) GenerateOptions();
			return _fullscreenModes[CurrentValue.ToInt()];
		}
	}
}