using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class RebindMenu : MonoBehaviour
{
    public Transform UiElementParent;
    public RebindAction RebindActionPrefab;
    public InputActionAsset InputAsset;

    public GameObject RebindOverlay;
    public TextMeshProUGUI RebindOverlayText;

    [ContextMenu("Generate Rebinding UI")]
    public void GenerateRebindingElements()
    {
        var actionRef = new List<InputActionReference>();

        Object[] subAssets = AssetDatabase.LoadAllAssetsAtPath(AssetDatabase.GetAssetPath(InputAsset));
        List<InputActionReference> inputActionReferences = new List<InputActionReference>();
        foreach (Object obj in subAssets)
        {
            // there are 2 InputActionReference returned for each InputAction in the asset, need to filter to not had the hidden one generated for backward compatibility
            if (obj is InputActionReference inputActionReference && (inputActionReference.hideFlags & HideFlags.HideInHierarchy) == 0)
            {
                inputActionReferences.Add(inputActionReference);
            }
        }

        foreach (var inputActionReference in inputActionReferences)
        {
            var rebindAction = Instantiate(RebindActionPrefab, UiElementParent);
            rebindAction.actionReference = inputActionReference;
            rebindAction.gameObject.name = inputActionReference.name;
            rebindAction.rebindOverlay = RebindOverlay;
            rebindAction.rebindPrompt = RebindOverlayText;
        }
    }

    //private void OnEnable()
    //{
    //    LoadKeybinds();
    //    UpdateRebindElements();
    //}

    private void UpdateRebindElements()
    {
        foreach (Transform rebindElement in UiElementParent)
        {
            rebindElement.GetComponent<RebindAction>().UpdateBindingDisplay();
        }
    }

    [ContextMenu("Clear Rebind Elements")]
    public void DebugClearRebindElements()
    {
        foreach (Transform child in UiElementParent)
        {
            DestroyImmediate(child.gameObject);
        }
    }

    /// <summary>
    /// Used to store changed keybinds in PlayerPrefs under the key "rebinds" 
    /// </summary>
    public void SaveKeybinds()
    {
        var rebinds = InputAsset.SaveBindingOverridesAsJson();
        PlayerPrefs.SetString("rebinds", rebinds);
    }

    /// <summary>
    /// Reset all keybind overrides
    /// </summary>
    public void RestoreDefaultKeybinds()
    {
        foreach (InputAction inputAction in InputAsset)
        {
            inputAction.RemoveAllBindingOverrides();
        }
        UpdateRebindElements();
    }

    /// <summary>
    /// Responsible for loading keybinds from PlayerPrefs saved under the key "rebinds"
    /// </summary>
    public void LoadKeybinds()
    {
        var rebinds = PlayerPrefs.GetString("rebinds");
        if (!string.IsNullOrEmpty(rebinds))
            InputAsset.LoadBindingOverridesFromJson(rebinds);
    }
}
