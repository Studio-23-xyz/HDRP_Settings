using UnityEngine;
using UnityEngine.UI;

namespace GameSettings
{
    public class TabSystemController: MonoBehaviour
    {
        public GameObject soundTab;
        public GameObject videoTab;
        public GameObject controlsTab;

        public Button soundTabButton;
        public Button videoTabButton;
        public Button controlsTabButton;

        public Color selectedTabColor;
        public Color deselectedTabColor;

        private void Start()
        {
            // Set the initial active tab
            ActivateTab(soundTab);
            UpdateTabButtonColors(soundTabButton);

            // Assign tab buttons' onClick events
            soundTabButton.onClick.AddListener(() =>
            {
                ActivateTab(soundTab);
                UpdateTabButtonColors(soundTabButton);
            });

            videoTabButton.onClick.AddListener(() =>
            {
                ActivateTab(videoTab);
                UpdateTabButtonColors(videoTabButton);
            });

            controlsTabButton.onClick.AddListener(() =>
            {
                ActivateTab(controlsTab);
                UpdateTabButtonColors(controlsTabButton);
            });
        }

        private void ActivateTab(GameObject tab)
        {
            // Activate the selected tab
            soundTab.SetActive(tab == soundTab);
            videoTab.SetActive(tab == videoTab);
            controlsTab.SetActive(tab == controlsTab);
        }

        private void UpdateTabButtonColors(Button selectedButton)
        {
            // Update tab button colors
            soundTabButton.image.color = selectedButton == soundTabButton ? selectedTabColor : deselectedTabColor;
            videoTabButton.image.color = selectedButton == videoTabButton ? selectedTabColor : deselectedTabColor;
            controlsTabButton.image.color = selectedButton == controlsTabButton ? selectedTabColor : deselectedTabColor;
        }
    }
}