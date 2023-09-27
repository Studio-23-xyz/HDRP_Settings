using Studio23.SS2.SettingsManager.Core.Component;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Studio23.SS2.SettingsManager.Core
{
	public class SettingsController : MonoBehaviour
	{
		public delegate void DecisionAction();

		public DecisionAction ApplyAction;
		public DecisionAction RestoreAction;

		[SerializeField] private Button _applyButton;
		[SerializeField] private Button _restoreButton;

		public List<Settings> Settings;

		private void OnEnable()
		{
			Debug.Log($"GameObject Name {name}");
			_applyButton.onClick.AddListener(() =>
			{
				ApplyAction?.Invoke();
			});
			_restoreButton.onClick.AddListener(()=>
			{
				RestoreAction?.Invoke();
			});
		}

		public virtual void Initialize()
		{
			Settings = GetComponentsInChildren<Settings>(true).ToList();
			Settings.ForEach(setting => setting.Setup());
		}
	}
}