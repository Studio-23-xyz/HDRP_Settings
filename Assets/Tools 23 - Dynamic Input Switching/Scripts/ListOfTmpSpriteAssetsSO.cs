using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Assets.Tools_23___Dynamic_Input_Switching.Scripts
{
    [CreateAssetMenu(fileName = "List of Sprite Assets", menuName = "TMP Sprite Asset", order = 0)]
    public class ListOfTmpSpriteAssetsSO : ScriptableObject
    {
        public List<TMP_SpriteAsset> SpriteAssets;
    }
}