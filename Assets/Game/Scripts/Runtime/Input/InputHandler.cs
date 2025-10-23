using System;
using Game.Scripts.Runtime.Cell;
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

        private void OnLeftMouseClick()
        {
            var cam = _camera.GetComponent<Camera>();
            var world = cam.ScreenToWorldPoint(UnityEngine.Input.mousePosition);

            var hit = Physics2D.Raycast(world, Vector2.zero);

            if (hit.collider != null)
            {
                var cellRenderer = hit.collider.GetComponent<CellRendererView>();
                if (cellRenderer != null)
                {
                    var x = Mathf.RoundToInt(cellRenderer.transform.localPosition.x + gameController.GridWidth / 2f);
                    var y = Mathf.RoundToInt(cellRenderer.transform.localPosition.y + gameController.GridHeight / 2f);
                    gameController.ToggleCell(x, y);
                }
            }
           
        }
    }
}