using System.Collections;
using System.Collections.Generic;
using Studio23.Input.Rebinding;
using UnityEngine;
using UnityEngine.InputSystem;

public class RebindMenu : MonoBehaviour
{
    public Transform UiElementParent;
    public RebindUI RebindUiPrefab;

    public InputActionAsset InputAsset;

    public void GenerateRebindingElements()
    {
        var actionRef = new List<InputActionReference>();

        foreach (var actionMap in InputAsset.actionMaps)
        {
            foreach (var action in actionMap.actions)
            {
                
            }
        }
    }
}
