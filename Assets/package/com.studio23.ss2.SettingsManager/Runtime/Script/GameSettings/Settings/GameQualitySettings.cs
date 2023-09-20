using com.studio23.ss2.Core;
using com.studio23.ss2.Core.Component;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


namespace GameSettings
{
	[RequireComponent(typeof(TMP_Dropdown))]
	public class GameQualitySettings : Settings
	{


		private VideoSettingsController videoSettingsController;
		[SerializeField] private TMP_Dropdown uiItem;

		[SerializeField] private QualityName defaultVal = QualityName.Medium; //default 1; medium, 0 high, 2 low 



		private void OnEnable()
		{
			videoSettingsController = FindObjectOfType<VideoSettingsController>();
			videoSettingsController.ApplyAction += ApplyAction;
			videoSettingsController.RestoreAction += RestoreAction;
		}

		private void OnDisable()
		{
			videoSettingsController.ApplyAction -= ApplyAction;
			videoSettingsController.RestoreAction -= RestoreAction;
		}

		public override void Setup()
		{


			base.Initialized((int)defaultVal, GetType().Name);

			Apply();
		}



		private void Start()
		{
			uiItem.AddOptionNew(GetOptions());
			uiItem.value = currentValue.ToInt();

			uiItem.onValueChanged.AddListener((value) =>
			{
				currentValue = value;
				if (isLive) Apply();
				videoSettingsController.QualityChangedAction?.Invoke((QualityName)currentValue.ToInt());
			});
		}

		private void RestoreAction()
		{
			uiItem.value = (int)defaultVal; // on change currentValue will be changed
			base.Save();
			if (!isLive) Apply(); // if Live then already applied this
		}

		private void ApplyAction()
		{
			base.Save();
			if (!isLive) Apply(); // if Live then already applied this
		}

		public void Apply()
		{
			QualitySettings.SetQualityLevel(currentValue.ToInt(), true);
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