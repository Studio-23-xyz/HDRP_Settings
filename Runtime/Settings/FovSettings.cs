using Cinemachine;
using Studio23.SS2.SettingsManager.Core;
using Studio23.SS2.SettingsManager.Utilities;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Studio23.SS2.SettingsManager.Video
{
	[RequireComponent(typeof(Slider))]
	public class FovSettings : Settings
	{
		[SerializeField] private Slider _uiItem;

		[Range(0, 1)]
		[SerializeField] private float _defaultVal = 0;
		[SerializeField] private TMP_Text _label;
		private CinemachineVirtualCamera _virtualCamera;

		public override void Setup()
		{
			_virtualCamera = FindObjectOfType<CinemachineVirtualCamera>();
			base.Initialized(_defaultVal, GetType().Name, true);
			Apply();
		}

		private void Start()
		{
			_uiItem.Init(CurrentValue.ToFloat());
			_label.text = SliderExtensions.FloatToText(_defaultVal, gameObject.name);
			_uiItem.onValueChanged.AddListener((value) =>
			{
				CurrentValue = value;
				if (IsLive) Apply();
				_label.text = SliderExtensions.FloatToText(value, gameObject.name);
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
			_virtualCamera.m_Lens.FieldOfView = 60f + Mathf.Clamp01(CurrentValue.ToFloat()) * 60f;
			// float : 0 - 1, 60-120
		}
	}
}