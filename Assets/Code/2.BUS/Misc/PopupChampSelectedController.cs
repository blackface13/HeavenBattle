using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopupChampSelectedController : MonoBehaviour
{
    #region Variables
    [TitleGroup("Cài đặt")]
    [HorizontalGroup("Cài đặt/Split", Width = 1f)]
    [TabGroup("Cài đặt/Split/Tab1", "Cấu hình thông số")]
    public Button BtnSelect, BtnEquip, BtnInfor, BtnCombatStyle;
    #endregion

    #region Initialize
    void Start()
    {
        AddButtonHandle();
    }
    #endregion

    #region Functions
    /// <summary>
    /// Thêm sự kiện nhấn cho button
    /// </summary>
    private void AddButtonHandle()
    {
        BtnSelect.onClick.AddListener(() => { });
        BtnEquip.onClick.AddListener(() => { });
        BtnInfor.onClick.AddListener(() => { });
        BtnCombatStyle.onClick.AddListener(() => { });
    }
    #endregion

    #region Events
    private void ButtonSelect()
    {
        GlobalVariables.BlackBGUI2InBattle.SetActive(false);
        GlobalVariables.BlackBGUI1InBattle.SetActive(true);
        GlobalVariables.ObjectArrowIntroInBattle.SetActive(true);

        for (int i = 0; i < GlobalVariables.ObjectButton3LaneInBattle.Length; i++)
        {
            GlobalVariables.ObjectButton3LaneInBattle[i].SetActive(true);
        }
    }
    #endregion
}
