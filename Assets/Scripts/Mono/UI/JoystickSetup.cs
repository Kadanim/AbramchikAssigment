using UnityEngine;
using UnityEngine.EventSystems;

namespace Mono.UI
{
    public class JoystickSetup : MonoBehaviour
    {
        public Joystick joystick;

        void Awake()
        {
            EventTrigger eventTrigger = gameObject.AddComponent<EventTrigger>();

            var pointerDownEntry = new EventTrigger.Entry
            {
                eventID = EventTriggerType.PointerDown
            };
            pointerDownEntry.callback.AddListener((eventData) =>
            {
                joystick.OnPointerDown((PointerEventData)eventData);
            });
            eventTrigger.triggers.Add(pointerDownEntry);

            var pointerUpEntry = new EventTrigger.Entry
            {
                eventID = EventTriggerType.PointerUp
            };
            pointerUpEntry.callback.AddListener((eventData) => { joystick.OnPointerUp((PointerEventData)eventData); });
            eventTrigger.triggers.Add(pointerUpEntry);

            var dragEntry = new EventTrigger.Entry
            {
                eventID = EventTriggerType.Drag
            };
            dragEntry.callback.AddListener((eventData) => { joystick.OnDrag((PointerEventData)eventData); });
            eventTrigger.triggers.Add(dragEntry);
        }
    }
}