using UnityEngine;
using UnityEngine.EventSystems;

public class MouseDragBehaviour : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{

    /// <summary>
    /// This method will be called on the start of the mouse drag
    /// </summary>
    /// <param name="eventData">mouse pointer event data</param>
    public void OnBeginDrag(PointerEventData eventData)
    {
        //Debug.Log("Begin Drag");
        //lastMousePosition = eventData.position;
    }

    /// <summary>
    /// This method will be called during the mouse drag
    /// </summary>
    /// <param name="eventData">mouse pointer event data</param>
    public void OnDrag(PointerEventData eventData)
    {
        transform.localPosition += (Vector3)eventData.delta;
    }

    /// <summary>
    /// This method will be called at the end of mouse drag
    /// </summary>
    /// <param name="eventData"></param>
    public void OnEndDrag(PointerEventData eventData)
    {
        //Debug.Log("End Drag");
        //Implement your funtionlity here
    }
}