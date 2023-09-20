using UnityEngine;
using UnityEngine.UI;

namespace com.studio23.ss2.UI
{
    public class TabSystemController: MonoBehaviour
    {
        public GameObject SoundTab;
        public GameObject VideoTab;
        public GameObject ControlsTab;

        public Button SoundTabButton;
        public Button VideoTabButton;
        public Button ControlsTabButton;

        public Color SelectedTabColor;
        public Color DeselectedTabColor;

        private void Start()
        {
            // Set the initial active tab
            ActivateTab(SoundTab);
            UpdateTabButtonColors(SoundTabButton);

            // Assign tab buttons' onClick events
            SoundTabButton.onClick.AddListener(() =>
            {
                ActivateTab(SoundTab);
                UpdateTabButtonColors(SoundTabButton);
            });

            VideoTabButton.onClick.AddListener(() =>
            {
                ActivateTab(VideoTab);
                UpdateTabButtonColors(VideoTabButton);
            });

            ControlsTabButton.onClick.AddListener(() =>
            {
                ActivateTab(ControlsTab);
                UpdateTabButtonColors(ControlsTabButton);
            });
        }

        private void ActivateTab(GameObject tab)
        {
            // Activate the selected tab
            SoundTab.SetActive(tab == SoundTab);
            VideoTab.SetActive(tab == VideoTab);
            ControlsTab.SetActive(tab == ControlsTab);
        }

        private void UpdateTabButtonColors(Button selectedButton)
        {
            // Update tab button colors
            SoundTabButton.image.color = selectedButton == SoundTabButton ? SelectedTabColor : DeselectedTabColor;
            VideoTabButton.image.color = selectedButton == VideoTabButton ? SelectedTabColor : DeselectedTabColor;
            ControlsTabButton.image.color = selectedButton == ControlsTabButton ? SelectedTabColor : DeselectedTabColor;
        }
    }
}