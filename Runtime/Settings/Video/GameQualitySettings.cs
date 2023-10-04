using Studio23.SS2.SettingsManager.Core;
using Studio23.SS2.SettingsManager.Data;
using Studio23.SS2.SettingsManager.Utilities;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


namespace Studio23.SS2.SettingsManager.Video
{
	[RequireComponent(typeof(TMP_Dropdown))]
	public class GameQualitySettings : Settings
	{
		[SerializeField] private TMP_Dropdown _uiItem;
		[SerializeField] private QualityName _defaultVal = QualityName.Medium; //default 1; medium, 0 high, 2 low 

		public override void Setup()
		{
			base.Initialized((int)_defaultVal, GetType().Name);
			Apply();
		}

		private void Start()
		{
			_uiItem.AddOptionNew(GetOptions());
			_uiItem.value = CurrentValue.ToInt();

			_uiItem.onValueChanged.AddListener((value) =>
			{
				CurrentValue = value;
				if (IsLive) Apply();
				VideoSettingsController.QualityChangedAction?.Invoke((QualityName)CurrentValue.ToInt());
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
			if (!IsLive) Apply(); // if Live then already applied this
		}

		public void Apply()
		{
			QualitySettings.SetQualityLevel(CurrentValue.ToInt(), true);
		}

		private List<TMP_Dropdown.OptionData> GetOptions()
		{
			//uiItem.AddOptions(QualitySettings.names.ToList());
			List<TMP_Dropdown.OptionData> optionData = new List<TMP_Dropdown.OptionData>();
			foreach (var item in Enum.GetValues(typeof(QualityName)))
			{
				optionData.Add(new TMP_Dropdown.OptionData(item.ToString()));
			}
			return optionData;
		}
	}
}