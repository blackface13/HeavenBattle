using Assets.Code._4.CORE;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class LayoutChampController : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public Image ImgChamp;
    private Button ButtonSelect;
    private bool IsTapHold;//Đang giữ chuột hay ko
    private string TapKey;//Mỗi lần tap, tạo 1 mã GUID khác nhau để xác định lần tap đó
    private bool IsWaitingShowPopup;//Đang trong thời gian chờ đợi show popup

    //private void Start()
    //{
    //}

    /// <summary>
    /// Thêm sự kiện cho button
    /// </summary>
    public void AddEventHandle(int slot)
    {
        ButtonSelect = this.GetComponent<Button>();
        ButtonSelect.onClick.AddListener(() =>
        {//Nhấn chọn tướng
            //GlobalVariables.PopupChampSelectedInBattle.SetActive(true);
            //GlobalVariables.BlackBGUI2InBattle.SetActive(true);
            //GlobalVariables.PopupChampSelectedInBattle.transform.position = this.transform.position;//Set tọa độ popup
            if (!IsWaitingShowPopup)
            {
                GlobalVariables.SlotChampSelectedInBattle = slot;
                GlobalVariables.BlackBGUI2InBattle.SetActive(false);
                GlobalVariables.BlackBGUI1InBattle.SetActive(true);
                GlobalVariables.ObjectArrowIntroInBattle.SetActive(true);

                for (int i = 0; i < GlobalVariables.ObjectButton3LaneInBattle.Length; i++)
                {
                    GlobalVariables.ObjectButton3LaneInBattle[i].SetActive(true);
                }
            }
            IsWaitingShowPopup = false;
        });
    }

    /// <summary>
    /// Nhấn giữ
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerDown(PointerEventData eventData)
    {
        IsTapHold = true;
        TapKey = Guid.NewGuid().ToString();
        StartCoroutine(WaitForShowPopup(.2f, TapKey));
    }

    /// <summary>
    /// Nhả chuột
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerUp(PointerEventData eventData)
    {
        IsTapHold = false;
        GlobalVariables.ImgWaitingHoldChamp.gameObject.SetActive(false);//Hình tròn waiting
        GlobalVariables.ImgWaitingHoldChamp.fillAmount = 0;
        //IsWaitingShowPopup = false;
    }

    /// <summary>
    /// Chờ sau 1 khoảng thời gian sẽ hiển thị hình tròn waiting
    /// </summary>
    /// <param name="delayTime"></param>
    /// <returns></returns>
    private IEnumerator WaitForShowPopup(float delayTime, string tapKey)
    {
        var tapKeyTemp = tapKey;
        yield return new WaitForSeconds(delayTime);
        if (tapKeyTemp.Equals(TapKey) && IsTapHold)
        {
            GlobalVariables.ImgWaitingHoldChamp.gameObject.SetActive(true);//Hình tròn waiting
            GlobalVariables.ImgWaitingHoldChamp.gameObject.transform.position = this.transform.position;
            IsWaitingShowPopup = true;
        }
    }

    private void Update()
    {
        if (IsWaitingShowPopup)//Nếu cho phép xuất hiện chờ show popup
        {
            GlobalVariables.ImgWaitingHoldChamp.fillAmount += GameSettings.SpeedFillImgWaitingHoldTap * Time.deltaTime;
            if (GlobalVariables.ImgWaitingHoldChamp.fillAmount >= 1)
            {
                IsWaitingShowPopup = false;

                GlobalVariables.ImgWaitingHoldChamp.gameObject.SetActive(false);//Hình tròn waiting
                GlobalVariables.PopupChampSelectedInBattle.SetActive(true);//Popup
                GlobalVariables.BlackBGUI2InBattle.SetActive(true);//Nền tối
                GlobalVariables.PopupChampSelectedInBattle.transform.position = this.transform.position;//Set tọa độ popup
            }
        }
    }
}
