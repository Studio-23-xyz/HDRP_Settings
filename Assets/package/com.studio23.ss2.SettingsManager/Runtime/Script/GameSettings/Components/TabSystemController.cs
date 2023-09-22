using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Studio23.SS2.SettingsManager.UI
{
	public class TabSystemController : MonoBehaviour
	{
		public List<GameObject> SettingsTabs;
		public List<Button> TabButtons;

		public Color SelectedTabColor;
		public Color DeselectedTabColor;

		private void Start()
		{
			Initialize();
		}

		private void Initialize()
		{
			for (int i = 0; i < TabButtons.Count; i++)
			{
				var temp = i;
				TabButtons[temp].onClick.AddListener(() =>
				{
					CleanupTabs();
					UpdateUi(temp);
				});
			}
		}

		private void UpdateUi(int i)
		{
			var tab = SettingsTabs[i];
			tab.SetActive(true);
			var tabButton = TabButtons[i];
			tabButton.image.color = SelectedTabColor;
		}

		private void CleanupTabs()
		{
			for (int i = 0; i < TabButtons.Count; i++)
			{
				SettingsTabs[i].SetActive(false);
				TabButtons[i].image.color = DeselectedTabColor;
			}
		}
	}
}