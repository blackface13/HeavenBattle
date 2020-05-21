using Assets.Code._4.CORE;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class LayoutChampController : MonoBehaviour
{
    public Button ButtonSelect;

    //private void Start()
    //{
    //    AddEventHandle();
    //}

    /// <summary>
    /// Thêm sự kiện cho button
    /// </summary>
    public  void AddEventHandle(int slot)
    {
        ButtonSelect.onClick.AddListener(() => {//Nhấn chọn tướng
            GlobalVariables.SlotChampSelectedInBattle = slot;
            GlobalVariables.PopupChampSelectedInBattle.SetActive(true);
            GlobalVariables.BlackBGUI2InBattle.SetActive(true);
            GlobalVariables.PopupChampSelectedInBattle.transform.position = this.transform.position;//Set tọa độ popup
        });
    }
}
