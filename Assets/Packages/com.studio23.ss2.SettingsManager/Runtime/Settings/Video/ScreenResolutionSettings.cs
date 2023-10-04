using Studio23.SS2.SettingsManager.Core;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Studio23.SS2.SettingsManager.Utilities;
using TMPro;
using UnityEngine;

namespace Studio23.SS2.SettingsManager.Video
{
	[RequireComponent(typeof(TMP_Dropdown))]
	public class ScreenResolutionSettings : Settings
	{
		private List<Resolution> Options { get; set; } = new();
		[SerializeField] private TMP_Dropdown _uiItem;
		[SerializeField] private FullScreenModeSettings _fullScreenModeSettings;
		[SerializeField] private int _defaultVal = 0;

		public override void Setup()
		{
			if (!Options.Any()) GenerateOptions();
			base.Initialized(_defaultVal, GetType().Name);
			Apply();
		}

		private void Start()
		{
			if (!Options.Any()) return;
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
			_uiItem.value = _defaultVal; // on change CurrentValue will be changed
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
			var setting = Options[CurrentValue.ToInt()];
			Screen.SetResolution(setting.width, setting.height, _fullScreenModeSettings.Get());
		}

		private void GenerateOptions()
		{
			Options = new List<Resolution>();
			Options.AddRange(Screen.resolutions.ToList());
			Options.Reverse();
		}

		private List<TMP_Dropdown.OptionData> GetOptions()
		{
			return Options.Select(x => Regex.Replace(x.ToString(), "([a-z])([A-Z])", "$1 $2")).Select(newVal => new TMP_Dropdown.OptionData(newVal)).ToList();
		}
	}
}