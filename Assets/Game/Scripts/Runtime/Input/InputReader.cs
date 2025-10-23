using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

namespace Game.Scripts.Runtime.Input
{
    [CreateAssetMenu(menuName = StrattonConstants.MENU_NAME.INPUT_MENU)]
    public class InputReader : ScriptableObject, GridActions.IUIActions
    {
        public event Action OnClick;
        public event Action OnDragPerformed;
        public event Action OnDragCancelled;
        public event Action<Vector2> OnScrollEvent; 
        public event Action<Vector2> OnDraggingEvent; 

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

        public void OnScroll(InputAction.CallbackContext context)
        {
            OnScrollEvent?.Invoke(context.ReadValue<Vector2>());
        }

        public void OnPointerPosition(InputAction.CallbackContext context)
        {
            OnDraggingEvent?.Invoke(context.ReadValue<Vector2>());
        }

        public void OnDrag(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                OnDragPerformed?.Invoke();
            }
            else if (context.canceled)
            {
                OnDragCancelled?.Invoke();
            }
        }
    }
}