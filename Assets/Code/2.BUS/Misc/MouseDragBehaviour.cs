using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Experimental.UIElements;

public class MouseDragBehaviour : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler, IPointerDownHandler, IPointerUpHandler, IPointerEnterHandler, IPointerExitHandler
{
    private bool IsDrag = false;//Đang kéo thả
    private bool IsHoldTap = false;//Đang giữ tap trên màn hình
    private bool IsInRange = true;
    private float DelayTimeTap = 1f;

    /// <summary>
    /// Nhấn chuột
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerDown(PointerEventData eventData)
    {
        IsHoldTap = true;
        StartCoroutine(WaitForEnableDrag());
    }

    /// <summary>
    /// Chờ sau 1 khoảng thời gian mới có thể kéo được object
    /// </summary>
    /// <returns></returns>
    private IEnumerator WaitForEnableDrag()
    {
        yield return new WaitForSeconds(DelayTimeTap);
        if (IsHoldTap && IsInRange)
            IsDrag = true;
    }

    /// <summary>
    /// Nhả chuột
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerUp(PointerEventData eventData)
    {
        IsHoldTap = IsDrag = false;
    }

    /// <summary>
    /// Bắt đầu kéo
    /// </summary>
    /// <param name="eventData">mouse pointer event data</param>
    public void OnBeginDrag(PointerEventData eventData)
    {
        //Debug.Log("Begin Drag");
        //lastMousePosition = eventData.position;
    }

    /// <summary>
    /// Đang kéo
    /// </summary>
    /// <param name="eventData">mouse pointer event data</param>
    public void OnDrag(PointerEventData eventData)
    {
        if (IsDrag)
            transform.localPosition += (Vector3)eventData.delta;
    }

    /// <summary>
    /// Thả
    /// </summary>
    /// <param name="eventData"></param>
    public void OnEndDrag(PointerEventData eventData)
    {
        IsDrag = false;
        //Debug.Log("End Drag");
        //Implement your funtionlity here
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        //if (IsHoldTap)
            IsInRange = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        IsHoldTap = IsInRange = false;
    }
}