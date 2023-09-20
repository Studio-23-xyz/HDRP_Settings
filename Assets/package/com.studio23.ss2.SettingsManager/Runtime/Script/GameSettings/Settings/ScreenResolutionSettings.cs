using com.studio23.ss2.Core;
using com.studio23.ss2.Core.Component;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;

namespace GameSettings
{
	[RequireComponent(typeof(TMP_Dropdown))]
	public class ScreenResolutionSettings : Settings
	{
		private List<Resolution> options { get; set; } = new();
		private VideoSettingsController _videoSettingsController;
		[SerializeField] private TMP_Dropdown uiItem;

		[SerializeField] private FullScreenModeSettings _fullScreenModeSettings;

		[SerializeField] private int defaultVal = 0;
		private void OnEnable()
		{
			_videoSettingsController = FindObjectOfType<VideoSettingsController>();
			_videoSettingsController.ApplyAction += ApplyAction;
			_videoSettingsController.RestoreAction += RestoreAction;
		}
		private void OnDisable()
		{
			_videoSettingsController.RestoreAction -= RestoreAction;
			_videoSettingsController.ApplyAction -= ApplyAction;

		}
		public override void Setup()
		{
			if (!options.Any()) GenerateOptions();
			base.Initialized(defaultVal, GetType().Name);
			Apply();
		}

		private void Start()
		{
			if (!options.Any()) return;
			uiItem.AddOptionNew(GetOptions());
			uiItem.value = currentValue.ToInt();

			uiItem.onValueChanged.AddListener((value) =>
			{
				currentValue = value;
				if (isLive) Apply();
			});
		}

		private void RestoreAction()
		{
			uiItem.value = defaultVal; // on change currentValue will be changed
			base.Save();
			if (!isLive) Apply(); // if Live then already applied this
		}
		private void ApplyAction()
		{
			base.Save();
			if (!isLive) Apply();  // if Live then already applied this
		}
		public void Apply()
		{
			var setting = options[currentValue.ToInt()];
			Screen.SetResolution(setting.width, setting.height, _fullScreenModeSettings.Get());
		}
		private void GenerateOptions()
		{
			options = new List<Resolution>();

			options.AddRange(Screen.resolutions.ToList());
			options.Reverse();



		}
		private List<TMP_Dropdown.OptionData> GetOptions()
		{
			return options.Select(x => Regex.Replace(x.ToString(), "([a-z])([A-Z])", "$1 $2")).Select(newVal => new TMP_Dropdown.OptionData(newVal)).ToList();
		}



	}



}