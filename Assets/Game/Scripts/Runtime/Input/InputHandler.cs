using System;
using UnityEngine;

namespace Game.Scripts.Runtime.Input
{
    public class InputHandler : MonoBehaviour
    {
        [Header(StrattonConstants.INSPECTOR.REFERENCES)]
        [SerializeField] private GameObject _camera;
        [SerializeField] private InputReader _reader;
        public GameManager gameController;
        
        private void Start()
        {
            _reader.OnClick += OnLeftMouseClick;
        }

        private void OnDestroy()
        {
            _reader.OnClick -= OnLeftMouseClick;
        }

        private void OnLeftMouseClick(Vector3 ctx)
        {
            Debug.Log("[Input] Testing input");
            var cam = _camera.GetComponent<Camera>();
            var world = cam.ScreenToWorldPoint(ctx);
            var x = Mathf.FloorToInt(world.x + gameController.GridWidth / 2f);
            var y = Mathf.FloorToInt(world.y + gameController.GridHeight / 2f);
            gameController.ToggleCell(x, y);
        }
    }
}