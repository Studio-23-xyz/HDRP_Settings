using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Tools_23_Dynamic_Input_Switching.Scripts.Player
{
    public class PlayerController : EventActionHandler
    {
        private CharacterController _controller;
        private Animator _animator;

        void Start()
        {
            _controller = GetComponent<CharacterController>();
            _animator = GetComponent<Animator>();
        }

        public override void HandleInputAction(string contextName)
        {
            Dictionary<string, Action> actions = new Dictionary<string, Action>()
            {
                { "Sword Attack", () => {
                    _animator.SetTrigger("Attack1");
                }},
                { "Shield Attack", () => {
                    _animator.SetTrigger("Attack2");
                }},
                { "Action3", () =>
                {
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
