using Studio23.Input.Rebinding;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class RebindMenu : MonoBehaviour
{
    public Transform UiElementParent;
    public RebindUI RebindUiPrefab;
    public RebindAction RebindActionPrefab;

    public InputActionAsset InputAsset;
    public PlayerControls PlayerControlsAsset;

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
            rebindAction.bindingId = "1";
            //if (inputActionReference.action.bindings[0].isComposite)
            //{
            //    Debug.Log(
            //        $"Composite binding {inputActionReference.action.name} & number of bindings {inputActionReference.action.bindings.Count}");
            //    for (int i = 1; i < 5; i++)
            //    {
            //        var compositeBinding = Instantiate(RebindUiPrefab, UiElementParent);
            //        compositeBinding.SetInputActionReference(inputActionReference, i);
            //        compositeBinding.gameObject.name = inputActionReference.action.bindings[i].name;
            //    }

            //    continue;
            //}
            //var rebindElement = Instantiate(RebindUiPrefab, UiElementParent);
            //rebindElement.SetInputActionReference(inputActionReference, -1);
            //rebindElement.gameObject.name = inputActionReference.name;
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
}
