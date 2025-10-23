using System;
using System.Collections.Generic;
using Game.Scripts.Runtime.Cell;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace Game.Scripts.Runtime.Input
{
    public class InputHandler : MonoBehaviour
    {
        #region Members

        [Header(StrattonConstants.INSPECTOR.REFERENCES)]
        [SerializeField] private GameObject _camera;
        [SerializeField] private InputReader _reader;
        public GameManager gameController;

        [SerializeField] private Slider _zoomSlider;

        [Header(StrattonConstants.INSPECTOR.SETTINGS)] 
        [SerializeField] private float _maxScrollValue = 100;
        [SerializeField] private float _dragSpeed = 0.01f;
        [SerializeField] private bool useBounds = true;
        [SerializeField] private Vector2 minBounds;
        [SerializeField] private Vector2 maxBounds;

        private Vector2 previousMousePos;
        private bool isDragging = false;

        private Camera _cam;

        #endregion

        #region Unity Functions

        private void Start()
        {
            _cam = _camera.GetComponent<Camera>();
            
            _reader.OnClick += OnLeftMouseClick;
            _reader.OnScrollEvent += OnScroll;
            _reader.OnDragPerformed += StartDrag;
            _reader.OnDragCancelled += EndDrag;
            _reader.OnDraggingEvent += DragCamera;
            _zoomSlider.onValueChanged.AddListener(OnZoomValueChanged);
        }

        private void OnDestroy()
        {
            _reader.OnClick -= OnLeftMouseClick;
            _reader.OnScrollEvent -= OnScroll;
            _reader.OnDragPerformed -= StartDrag;
            _reader.OnDragCancelled -= EndDrag;
            _reader.OnDraggingEvent -= DragCamera;
            _zoomSlider.onValueChanged.RemoveListener(OnZoomValueChanged);
        }

        #endregion

        #region Input Events

        private void OnZoomValueChanged(float value)
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

        private void StartDrag()
        {
            isDragging = true;
            previousMousePos = Mouse.current.position.ReadValue();
        }

        private void EndDrag()
        {
            isDragging = false;
        }

        private void DragCamera(Vector2 currentMousePos)
        {
            if (!isDragging) return;
            
            var prevWorld = _cam.ScreenToWorldPoint(previousMousePos);
            var currWorld = _cam.ScreenToWorldPoint(currentMousePos);
            var delta = prevWorld - currWorld;

            _cam.transform.position += delta;

            if (useBounds)
                ClampToBounds();

            previousMousePos = currentMousePos;
        }

        #endregion

        #region Helpers

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

        private void ClampToBounds()
        {
            if (!useBounds) return;

            var pos = _cam.transform.position;
            pos.x = Mathf.Clamp(pos.x, minBounds.x, maxBounds.x);
            pos.y = Mathf.Clamp(pos.y, minBounds.y, maxBounds.y);
            _cam.transform.position = pos;
        }

        #endregion
        
    }
}