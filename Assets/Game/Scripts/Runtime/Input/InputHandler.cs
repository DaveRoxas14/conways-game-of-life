using System;
using System.Collections.Generic;
using Game.Scripts.Runtime.Cell;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Game.Scripts.Runtime.Input
{
    public class InputHandler : MonoBehaviour
    {
        [Header(StrattonConstants.INSPECTOR.REFERENCES)]
        [SerializeField] private GameObject _camera;
        [SerializeField] private InputReader _reader;
        public GameManager gameController;

        [SerializeField] private Slider _zoomSlider;

        [Header(StrattonConstants.INSPECTOR.SETTINGS)] 
        [SerializeField] private float _maxScrollValue = 100;

        private Camera _cam;
        
        private void Start()
        {
            _cam = _camera.GetComponent<Camera>();
            
            _reader.OnClick += OnLeftMouseClick;
            _reader.OnScrollEvent += OnScroll;
            _zoomSlider.onValueChanged.AddListener(OnZoomVaueChanged);
        }

        private void OnDestroy()
        {
            _reader.OnClick -= OnLeftMouseClick;
            _reader.OnScrollEvent += OnScroll;
        }

        private void OnZoomVaueChanged(float value)
        {
            _cam.orthographicSize = Mathf.Clamp(_maxScrollValue * value, 1, _maxScrollValue);
        }

        private void OnScroll(Vector2 value)
        {
            var target = _cam.orthographicSize - value.y;
            _cam.orthographicSize = Mathf.Clamp(target, 1, _maxScrollValue);
            _zoomSlider.value = target / _maxScrollValue;
        }

        private void OnLeftMouseClick()
        {
            if(IsPointerOverUI(UnityEngine.Input.mousePosition)) return;
            
            var world = _cam.ScreenToWorldPoint(UnityEngine.Input.mousePosition);

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

        private bool IsPointerOverUI(Vector2 pos)
        {
            var pointerData = new PointerEventData(EventSystem.current)
            {
                position = pos
            };

            var results = new List<RaycastResult>();
            
            EventSystem.current.RaycastAll(pointerData, results);

            return results.Count > 0;
        }
        
    }
}