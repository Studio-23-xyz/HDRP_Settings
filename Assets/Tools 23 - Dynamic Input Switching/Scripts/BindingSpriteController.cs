using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Assets.Tools_23_Dynamic_Input_Switching.Scripts
{
    public class BindingSpriteController : MonoBehaviour
    {
        #region Properties

        public InputActionReference actionReference
        {
            get => m_Action;
            set { m_Action = value; }
        }

        public string bindingId
        {
            get => m_BindingId;
            set { m_BindingId = value; }
        }

        public InputBinding.DisplayStringOptions displayStringOptions
        {
            get => m_DisplayStringOptions;
            set { m_DisplayStringOptions = value; }
        }

        public TextMeshProUGUI actionLabel
        {
            get => m_ActionLabel;
            set { m_ActionLabel = value; }
        }

        public ListOfTmpSpriteAssetsSO ListofTmpSpriteAssets
        {
            get => m_ListOfTmpSpriteAssets;
            set { m_ListOfTmpSpriteAssets = value; }
        }

        #endregion

        #region Variables

        [Tooltip("Reference to action that is to be displayed.")]
        [SerializeField]
        private InputActionReference m_Action;

        [SerializeField] private string m_BindingId;

        [SerializeField] private InputBinding.DisplayStringOptions m_DisplayStringOptions;

        [Tooltip("Text label that will receive the name of the action. Optional. Set to None to have the "
                 + "rebind UI not show a label for the action.")]
        [SerializeField]
        private TextMeshProUGUI m_ActionLabel;

        [Tooltip("Scriptable Object container that holds the sprite sheet references for easier access.")]
        [SerializeField]
        private ListOfTmpSpriteAssetsSO m_ListOfTmpSpriteAssets;

        #endregion

        /// <summary>
        /// Fetches current binding index and input device index to get TMP formatted sprite text and assigns the action name to the TMP field if set in inspector.
        /// </summary>
        [ContextMenu("Update Binding Sprite")]
        public void FetchBindingSprite()
        {
            var action = actionReference.action;
            int deviceIndex = GameInput.Instance.GetInputDeviceIndex();
            //string bindingText = FetchSprite(action, deviceIndex, action.bindings[0].isComposite);
            //Debug.Log($"bindingText = {bindingText}");
			GetComponent<TextMeshProUGUI>().text = FetchSprite(action, deviceIndex, action.bindings[0].isComposite);
            if (m_ActionLabel != null)
            {
                m_ActionLabel.text = action.name;
            }
        }

        /// <summary>
        /// Used to communicate with ActionToSpriteConverter static class to fetch TMPSpriteAsset text based upon current input device and the input action desired.
        /// </summary>
        /// <param name="targetAction">Input action to fetch.</param>
        /// <param name="inputDeviceIndex">Index of the active input device.</param>
        /// <param name="isComposite">Is the InputAction part of a composite or not.</param>
        /// <returns>Returns the TMP Sprite asset formatted string that will fetch the sprite from the desired sprite sheet.</returns>
        private string FetchSprite(InputAction targetAction, int inputDeviceIndex, bool isComposite)
        {
            int compositeIndex = inputDeviceIndex;
            int targetBinding = 0;
            string dispString = "";
            targetBinding = targetAction.bindings.IndexOf(x => x.id.ToString() == m_BindingId);
            if (isComposite)
            {
                if (inputDeviceIndex > 0)
                {
                    compositeIndex = targetBinding + (inputDeviceIndex * 5);
                    dispString =
                        targetAction.GetBindingDisplayString(compositeIndex, out _, out _, displayStringOptions);
                }
                else
                {
                    dispString =
                        targetAction.GetBindingDisplayString(targetBinding, out _, out _, displayStringOptions);
                }
            }
            else
            {
                dispString = targetAction.GetBindingDisplayString(inputDeviceIndex, displayStringOptions);
            }

            return ActionToSpriteConverter.ReplaceBindingToSpriteText(dispString,
                ListofTmpSpriteAssets.SpriteAssets[inputDeviceIndex],
                targetAction.ToString());
        }
    }
}