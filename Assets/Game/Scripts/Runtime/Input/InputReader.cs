using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Game.Scripts.Runtime.Input
{
    [CreateAssetMenu(menuName = StrattonConstants.MENU_NAME.INPUT_MENU)]
    public class InputReader : ScriptableObject, GridActions.IUIActions
    {
        public event Action OnClick;

        private GridActions _gridInput;

        private void OnEnable()
        {
            if (_gridInput == null)
            {
                _gridInput = new GridActions();
                _gridInput.UI.SetCallbacks(this);
            }
            
            _gridInput.UI.Enable();
        }

        private void OnDisable()
        {
            _gridInput.UI.Disable();
        }

        void GridActions.IUIActions.OnClick(InputAction.CallbackContext context)
        {
            if(context.performed)
            {
                OnClick?.Invoke();
            }
        }
    }
}