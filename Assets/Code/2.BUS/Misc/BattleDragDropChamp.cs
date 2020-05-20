using Assets.Code._4.CORE;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BattleDragDropChamp : MonoBehaviour, IPointerClickHandler
{
    PointerEventData eventDT;

    /// <summary>
    /// Chờ sau 1 khoảng thời gian mới có thể kéo được object
    /// </summary>
    /// <returns></returns>
    private IEnumerator WaitForEnableDrag()
    {
        yield return new WaitForSeconds(.1f);
        //this
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        GlobalVariables.PopupChampSelectedInBattle.SetActive(true);
        GlobalVariables.BlackBGUI2InBattle.SetActive(true);
        GlobalVariables.PopupChampSelectedInBattle.transform.position = this.transform.position;
    }
}
