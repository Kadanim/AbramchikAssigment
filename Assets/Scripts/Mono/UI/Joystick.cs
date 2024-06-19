using UnityEngine;
using UnityEngine.EventSystems;

namespace Mono.UI
{
    public class Joystick : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler
    {
        public RectTransform JoystickBackground;
        public RectTransform JoystickHandle;
        private Canvas _canvas;
        private RectTransform _canvasRectTransform;

        private Vector2 _inputVector;

        public Vector2 Direction => _inputVector;

        void Start()
        {
            _canvas = GetComponentInParent<Canvas>();
            _canvasRectTransform = _canvas.GetComponent<RectTransform>();

            JoystickBackground.gameObject.SetActive(false);
            JoystickHandle.anchoredPosition = Vector2.zero;
        }

        public void OnDrag(PointerEventData eventData)
        {
            Vector2 position;
            if (RectTransformUtility.ScreenPointToLocalPointInRectangle(JoystickBackground, eventData.position,
                    eventData.pressEventCamera, out position))
            {
                position.x =
                    (position.x / JoystickBackground.sizeDelta.x) * 2;
                position.y =
                    (position.y / JoystickBackground.sizeDelta.y) * 2;

                _inputVector = new Vector2(position.x, position.y);
                _inputVector = _inputVector.magnitude > 1.0f ? _inputVector.normalized : _inputVector;

                JoystickHandle.anchoredPosition = new Vector2(_inputVector.x * (JoystickBackground.sizeDelta.x / 2),
                    _inputVector.y * (JoystickBackground.sizeDelta.y / 2));
            }
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            Vector2 position;
            if (RectTransformUtility.ScreenPointToLocalPointInRectangle(_canvasRectTransform, eventData.position,
                    eventData.pressEventCamera, out position))
            {
                JoystickBackground.anchoredPosition = position;
                JoystickHandle.anchoredPosition = Vector2.zero;
            }

            JoystickBackground.gameObject.SetActive(true);
            OnDrag(eventData);
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            _inputVector = Vector2.zero;
            JoystickHandle.anchoredPosition = Vector2.zero;

            JoystickBackground.gameObject.SetActive(false);
        }
    }
}