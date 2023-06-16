using UnityEngine.EventSystems;
using UnityEngine;

public class TriggerBtn : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public int direction;

    public void OnPointerDown(PointerEventData eventData)
    {
        Player.direction = direction;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        Player.direction = 0;
    }
}
