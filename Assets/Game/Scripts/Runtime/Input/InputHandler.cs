using System;
using UnityEngine;

namespace Game.Scripts.Runtime.Input
{
    public class InputHandler : MonoBehaviour
    {
        [Header(StrattonConstants.INSPECTOR.REFERENCES)]
        [SerializeField] private Camera _camera;
        [SerializeField] private InputReader _reader;

        private void Start()
        {
            _reader.OnClick += OnLeftMouseClick;
        }

        private void OnDestroy()
        {
            _reader.OnClick -= OnLeftMouseClick;
        }

        private void OnLeftMouseClick()
        {
            Debug.Log("[Input] Is this working");
        }
    }
}