using Assets.Code._4.CORE;
using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class LayoutChampController : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    #region Variables
    [TitleGroup("Cài đặt thông số")]
    [HorizontalGroup("Cài đặt thông số/Split", Width = 1f)]
    [TabGroup("Cài đặt thông số/Split/Tab1", "Cấu hình thông số")]
    public Image ImgChamp;

    [TitleGroup("Cài đặt thông số")]
    [HorizontalGroup("Cài đặt thông số/Split", Width = 1f)]
    [TabGroup("Cài đặt thông số/Split/Tab1", "Cấu hình thông số")]
    public GameObject ImgHP;

    private Button ButtonSelect;
    public HeroController HeroControl;
    private bool IsTapHold;//Đang giữ chuột hay ko
    private string TapKey;//Mỗi lần tap, tạo 1 mã GUID khác nhau để xác định lần tap đó
    private bool IsWaitingShowPopup;//Đang trong thời gian chờ đợi show popup 
    private int ThisSlot;//Slot của tướng
    public BattleSystemController BattleSystem;
    private bool IsInLane;//Có đang dc chọn ra trận hay chưa
    #endregion

    private void Awake()
    {
        BattleSystem = GameObject.Find("Controller").GetComponent<BattleSystemController>();
    }

    //private void Start()
    //{
    //}

    /// <summary>
    /// Thêm sự kiện cho button
    /// </summary>
    public void AddEventHandle(int slot, HeroController heroControl)
    {
        ThisSlot = slot;
        HeroControl = heroControl;
        ButtonSelect = this.GetComponent<Button>();
        ButtonSelect.onClick.AddListener(() =>
        {
            //Nếu chưa ra trận
            if (!IsInLane)
            {
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
                IsInLane = true;//Ra trận
            }
            else
            //Nếu đã ra trận => chuyển vị trí camera tới đó
            BattleSystem.ViewLocationHero(ThisSlot);
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

        //Cập nhật thanh máu trong UI
        if (HeroControl.IsAlive)
        {
            ImgHP.transform.localScale = new Vector3(Math.Abs(HeroControl.DataValues.vHealthCurrent / HeroControl.DataValues.vHealth), ImgHP.transform.localScale.y, ImgHP.transform.localScale.z);
        }
        else
        {
            if (IsInLane)//Nếu đang ra trận
            {
                BattleSystem.PushToScrollViewTotal(ThisSlot);//Đẩy lên scroll view tổng
                ImgHP.transform.localScale = new Vector3(0, ImgHP.transform.localScale.y, ImgHP.transform.localScale.z);
                IsInLane = false;//Chưa xuất trận
            }
        }
    }
}
