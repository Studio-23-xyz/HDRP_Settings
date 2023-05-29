using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Assets.Tools_23___Dynamic_Input_Switching.Scripts.Player
{
    public class PlayerController : EventActionHandler
    {
        [Header("Binding Sprite")]
        public TextMeshProUGUI BindingSprite;
        public TextMeshProUGUI BindingSprite2;

        private CharacterController _controller;
        private Animator _animator;
        private PlayerInput _playerInput;



        // Start is called before the first frame update
        void Start()
        {
            _controller = GetComponent<CharacterController>();
            _animator = GetComponent<Animator>();
            _playerInput = GetComponent<PlayerInput>();
            UpdateBindingSprite();
        }



    
        public void UpdateBindingSprite()
        {
            var bindingText = GameInput.Instance.GetBindingText(InputType.Action1);
            BindingSprite.text = BindingDisplayHandler.Instance.GetBindingSprite(bindingText, InputType.Action1);

            var bindingText2 = GameInput.Instance.GetBindingText(InputType.Action2);
            BindingSprite2.text = BindingDisplayHandler.Instance.GetBindingSprite(bindingText2, InputType.Action2);
        }

        public void Update()
        {
        
        }

        //Define your actions here
        public override void HandleInputAction(string contextName)
        {
            Dictionary<string, Action> actions = new Dictionary<string, Action>()
            {
                { "Action1", () => {
                    Debug.Log("pressed action 1");
                    _animator.SetTrigger("Attack1");
                }},
                { "Action2", () => {
                    Debug.Log("pressed action 2");
                    _animator.SetTrigger("Attack2");
                }},
                { "Action3", () => {
                    Debug.Log("pressed action 3");
               
                }},
            };
            if (actions.ContainsKey(contextName))
            {
                actions[contextName].Invoke();
            }
            else
            {
                Debug.LogError("Event action not defined");
            }
        }
    }
}
