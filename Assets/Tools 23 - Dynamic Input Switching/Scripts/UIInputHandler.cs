using TMPro;
using UnityEngine;

namespace Assets.Tools_23___Dynamic_Input_Switching.Scripts
{
    public class UIInputHandler : MonoBehaviour
    {
        public TextMeshProUGUI BindingSprite01;
        public TextMeshProUGUI BindingSprite02;
        public TextMeshProUGUI BindingSprite03;
        // Start is called before the first frame update

        //void Start()
        //{
        //    UpdateBindingSprite();
        //}


        //public void UpdateBindingSprite()
        //{
        //    var bindingText = GameInput.Instance.GetBindingText(InputType.Action1);
        //    BindingSprite01.text = BindingDisplayHandler.Instance.GetBindingSprite(bindingText, InputType.Action1);

        //    var bindingText02 = GameInput.Instance.GetBindingText(InputType.Action2);
        //    BindingSprite02.text = BindingDisplayHandler.Instance.GetBindingSprite(bindingText, InputType.Action2);

        //    var bindingText03 = GameInput.Instance.GetBindingText(InputType.Action3);
        //    BindingSprite03.text = BindingDisplayHandler.Instance.GetBindingSprite(bindingText, InputType.Action3);
        //}

    }
}
