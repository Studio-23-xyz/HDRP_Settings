﻿using Cinemachine;
using Studio23.SS2.SettingsManager.Core;
using Studio23.SS2.SettingsManager.Core.Component;
using Studio23.SS2.SettingsManager.Extension;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace GameSettings
{
	[RequireComponent(typeof(Slider))]
	public class FovSettings : Settings
	{
		private VideoSettingsController _videoSettingsController;
		[SerializeField] private Slider uiItem;

		[Range(0, 1)]
		[SerializeField] private float defaultVal = 0;
		[SerializeField] private TMP_Text label;
		private CinemachineVirtualCamera virtualCamera;
		private void OnEnable()
		{
			_videoSettingsController = FindObjectOfType<VideoSettingsController>();
			_videoSettingsController.ApplyAction += ApplyAction;
			_videoSettingsController.RestoreAction += RestoreAction;
		}

		private void OnDisable()
		{
			_videoSettingsController.ApplyAction -= ApplyAction;
			_videoSettingsController.RestoreAction -= RestoreAction;
		}

		public override void Setup()
		{



			virtualCamera = FindObjectOfType<CinemachineVirtualCamera>();



			base.Initialized(defaultVal, GetType().Name, true);



			Apply();

		}

		private void Start()
		{

			uiItem.Init(CurrentValue.ToFloat());

			label.text = FloatToText(defaultVal, gameObject.name);

			uiItem.onValueChanged.AddListener((value) =>
			{
				CurrentValue = value;
				if (isLive) Apply();
				label.text = FloatToText(value, gameObject.name);
			});
		}

		private void RestoreAction()
		{
			uiItem.value = defaultVal; // on change CurrentValue will be changed
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
			virtualCamera.m_Lens.FieldOfView = 60f + Mathf.Clamp01(CurrentValue.ToFloat()) * 60f;

			// float : 0 - 1, 60-120

		}


	}



}