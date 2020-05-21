﻿using Anima2D;
using Assets.Code._3.DAO;
using Assets.Code._4.CORE;
using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Experimental.UIElements;
using UnityEngine.UI;
using Image = UnityEngine.UI.Image;

public class BattleSystemController : MonoBehaviour
{
    #region Variables
    [TitleGroup("Cài đặt hệ thống battle")]
    [HorizontalGroup("Cài đặt hệ thống battle/Split", Width = 1f)]
    [TabGroup("Cài đặt hệ thống battle/Split/Tab1", "Cấu hình thông số")]
    public int NumberObjectDmgTextCreate;

    [TitleGroup("Cài đặt hệ thống battle")]
    [HorizontalGroup("Cài đặt hệ thống battle/Split", Width = 1f)]
    [TabGroup("Cài đặt hệ thống battle/Split/Tab1", "Cấu hình thông số")]
    public float DelayTimeBetween2Soldier, DelayTimeBetween2GroupSoldier;

    [TitleGroup("Cài đặt hệ thống battle")]
    [HorizontalGroup("Cài đặt hệ thống battle/Split", Width = 1f)]
    [TabGroup("Cài đặt hệ thống battle/Split/Tab1", "Cấu hình thông số")]
    public GameObject BoxControl, BtnExpand, PopupChampSelected, BlackBGUI1, BlackBGUI2, ObjectArrowIntro;

    [TitleGroup("Cài đặt hệ thống battle")]
    [HorizontalGroup("Cài đặt hệ thống battle/Split", Width = 1f)]
    [TabGroup("Cài đặt hệ thống battle/Split/Tab1", "Cấu hình thông số")]
    public GameObject[] ObjectButton3Lane, ObjectContent4Lane;


    public GameObject ObjectTest;

    public List<GameObject> ChampTeam1;
    public List<GameObject> LayoutChampTeam1;
    public List<GameObject> ChampTeam2;
    private List<GameObject> SoldierTeam1;
    private List<GameObject> SoldierTeam2;

    public List<GameObject> DamageText;
    public List<DamageTextController> DamageTextControl;
    private bool IsBoxControlExpand;
    private float BoxControlPosXOrigin;
    private float BoxControlRangeMove = 917;
    public GameObject[] test;
    private List<GameObject> SoldierTeam1Type1;//lính cận chiến team 1
    private List<GameObject> SoldierTeam1Type2;//lính đánh xa team 1
    private List<GameObject> SoldierTeam2Type1;//lính cận chiến team 2
    private List<GameObject> SoldierTeam2Type2;//lính đánh xa team 2
    public AnimationCurve Curve;
    private int SlotSelected;
    #endregion

    #region Initialize
    void Start()
    {
        GameSettings.CreateChampDefault();
        GameSettings.CreateSoldierDefault();
        CreateTeam();
        CreateDmgText();
        CreateSoldier();
        BoxControlPosXOrigin = BoxControl.transform.localPosition.x;
        StartCoroutine(AutoCreateSoldier(2, 2));
        SetupUI();
    }

    /// <summary>
    /// Khởi tạo các thành phần giao diện control
    /// </summary>
    private void SetupUI()
    {
        GlobalVariables.PopupChampSelectedInBattle = PopupChampSelected;
        GlobalVariables.BlackBGUI1InBattle = BlackBGUI1;
        GlobalVariables.BlackBGUI2InBattle = BlackBGUI2;
        GlobalVariables.ObjectArrowIntroInBattle = ObjectArrowIntro;
        GlobalVariables.ObjectButton3LaneInBattle = ObjectButton3Lane;
    }

    /// <summary>
    /// Khởi tạo các object dmg text
    /// </summary>
    private void CreateDmgText()
    {
        DamageText = new List<GameObject>();
        DamageTextControl = new List<DamageTextController>();
        for (int i = 0; i < NumberObjectDmgTextCreate; i++)
        {
            DamageText.Add((GameObject)Instantiate(Resources.Load<GameObject>("Prefabs/UI/DamageText"), new Vector3(-1000, -1000, 0), Quaternion.identity));
            DamageTextControl.Add(DamageText[i].GetComponent<DamageTextController>());
            DamageText[i].SetActive(false);
        }
    }

    /// <summary>
    /// Khởi tạo tướng của 2 đội
    /// </summary>
    private void CreateTeam()
    {
        ChampTeam1 = new List<GameObject>();
        ChampTeam1.Add(Instantiate(Resources.Load<GameObject>("Prefabs/Champs/Champ1"), new Vector3(-1000, -2, 0), Quaternion.identity));
        ChampTeam1[0].GetComponent<HeroController>().SetupChamp(true);
        ChampTeam1[0].SetActive(false);
        ChampTeam1.Add(Instantiate(Resources.Load<GameObject>("Prefabs/Champs/Champ2"), new Vector3(-1000, -2, 0), Quaternion.identity));
        ChampTeam1[1].GetComponent<HeroController>().SetupChamp(true);
        ChampTeam1[1].SetActive(false);
        ChampTeam1.Add(Instantiate(Resources.Load<GameObject>("Prefabs/Champs/Champ1"), new Vector3(-1000, 5, 0), Quaternion.identity));
        ChampTeam1[2].GetComponent<HeroController>().SetupChamp(true);
        ChampTeam1[2].SetActive(false);
        ChampTeam1.Add(Instantiate(Resources.Load<GameObject>("Prefabs/Champs/Champ1"), new Vector3(-1000, -10, 0), Quaternion.identity));
        ChampTeam1[3].GetComponent<HeroController>().SetupChamp(true);
        ChampTeam1[3].SetActive(false);


        LayoutChampTeam1 = new List<GameObject>();
        var count = ChampTeam1.Count;
        ObjectContent4Lane[0].GetComponent<RectTransform>().sizeDelta = new Vector2((120 * 2) + (215 * (count - 1)), ObjectContent4Lane[0].GetComponent<RectTransform>().sizeDelta.y);
        for (int i = 0; i < count; i++)
        {
            LayoutChampTeam1.Add(Instantiate(Resources.Load<GameObject>("Prefabs/UI/LayoutChamp"), new Vector3(0, 0, 0), Quaternion.identity));
            LayoutChampTeam1[i].transform.SetParent(ObjectContent4Lane[0].transform, false);
            LayoutChampTeam1[i].transform.localPosition = new Vector3(120 + (215 * i), 2f, 0);
            LayoutChampTeam1[i].GetComponent<LayoutChampController>().ButtonSelect.GetComponent<Image>().sprite = Resources.Load<Sprite>("ChampAvt/" + ChampTeam1[i].GetComponent<HeroController>().ChampID);
            LayoutChampTeam1[i].GetComponent<LayoutChampController>().AddEventHandle(i);
        }

        ChampTeam2 = new List<GameObject>();
        ChampTeam2.Add(Instantiate(Resources.Load<GameObject>("Prefabs/Champs/Champ2"), new Vector3(190, -2, 0), Quaternion.identity));
        ChampTeam2[0].GetComponent<HeroController>().SetupChamp(false);
        ChampTeam2.Add(Instantiate(Resources.Load<GameObject>("Prefabs/Champs/Champ2"), new Vector3(180, -2, 0), Quaternion.identity));
        ChampTeam2[1].GetComponent<HeroController>().SetupChamp(false);
        ChampTeam2.Add(Instantiate(Resources.Load<GameObject>("Prefabs/Champs/Champ2"), new Vector3(200, 5, 0), Quaternion.identity));
        ChampTeam2[2].GetComponent<HeroController>().SetupChamp(false);
        ChampTeam2.Add(Instantiate(Resources.Load<GameObject>("Prefabs/Champs/Champ2"), new Vector3(185, -10, 0), Quaternion.identity));
        ChampTeam2[3].GetComponent<HeroController>().SetupChamp(false);
    }

    /// <summary>
    /// Khởi tạo lính của 2 đội
    /// </summary>
    private void CreateSoldier()
    {
        SoldierTeam1Type1 = new List<GameObject>();//lính cận chiến team 1
        SoldierTeam1Type2 = new List<GameObject>(); //lính đánh xa team 1
        SoldierTeam2Type1 = new List<GameObject>();//lính cận chiến team 2
        SoldierTeam2Type2 = new List<GameObject>();//lính đánh xa team 2

        //Tạo lính cận chiến cho 2 team
        SoldierTeam1Type1.Add(Instantiate(Resources.Load<GameObject>("Prefabs/Soldiers/Soldier1"), new Vector3(-1000, -1000, 0), Quaternion.identity));
        SoldierTeam1Type1[0].GetComponent<SoldierController>().SetupChamp(true);
        SoldierTeam1Type1[0].SetActive(false);
        SoldierTeam2Type1.Add(Instantiate(Resources.Load<GameObject>("Prefabs/Soldiers/Soldier1"), new Vector3(-1000, -1000, 0), Quaternion.identity));
        SoldierTeam2Type1[0].GetComponent<SoldierController>().SetupChamp(false);
        SoldierTeam2Type1[0].SetActive(false);

        //Tạo lính đánh xa cho 2 team
        SoldierTeam1Type2.Add(Instantiate(Resources.Load<GameObject>("Prefabs/Soldiers/Soldier2"), new Vector3(-1000, -1000, 0), Quaternion.identity));
        SoldierTeam1Type2[0].GetComponent<SoldierController>().SetupChamp(true);
        SoldierTeam1Type2[0].SetActive(false);
        SoldierTeam2Type2.Add(Instantiate(Resources.Load<GameObject>("Prefabs/Soldiers/Soldier2"), new Vector3(-1000, -1000, 0), Quaternion.identity));
        SoldierTeam2Type2[0].GetComponent<SoldierController>().SetupChamp(false);
        SoldierTeam2Type2[0].SetActive(false);

        //SoldierTeam1 = new List<GameObject>();
        //SoldierTeam2 = new List<GameObject>();
        //SoldierTeam1.Add(Instantiate(Resources.Load<GameObject>("Prefabs/Soldiers/Soldier1"), new Vector3(5, -2, 0), Quaternion.identity));
        //SoldierTeam1.Add(Instantiate(Resources.Load<GameObject>("Prefabs/Soldiers/Soldier2"), new Vector3(0, -2, 0), Quaternion.identity));
        //SoldierTeam1.Add(Instantiate(Resources.Load<GameObject>("Prefabs/Soldiers/Soldier1"), new Vector3(5, 5, 0), Quaternion.identity));
        //SoldierTeam1.Add(Instantiate(Resources.Load<GameObject>("Prefabs/Soldiers/Soldier2"), new Vector3(0, 5, 0), Quaternion.identity));
        //SoldierTeam1.Add(Instantiate(Resources.Load<GameObject>("Prefabs/Soldiers/Soldier1"), new Vector3(5, -10, 0), Quaternion.identity));
        //SoldierTeam1.Add(Instantiate(Resources.Load<GameObject>("Prefabs/Soldiers/Soldier2"), new Vector3(0, -10, 0), Quaternion.identity));

        //SoldierTeam2.Add(Instantiate(Resources.Load<GameObject>("Prefabs/Soldiers/Soldier1"), new Vector3(180, -2, 0), Quaternion.identity));
        //SoldierTeam2.Add(Instantiate(Resources.Load<GameObject>("Prefabs/Soldiers/Soldier2"), new Vector3(175, -2, 0), Quaternion.identity));
        //SoldierTeam2.Add(Instantiate(Resources.Load<GameObject>("Prefabs/Soldiers/Soldier1"), new Vector3(180, 5, 0), Quaternion.identity));
        //SoldierTeam2.Add(Instantiate(Resources.Load<GameObject>("Prefabs/Soldiers/Soldier2"), new Vector3(175, 5, 0), Quaternion.identity));
        //SoldierTeam2.Add(Instantiate(Resources.Load<GameObject>("Prefabs/Soldiers/Soldier1"), new Vector3(180, -10, 0), Quaternion.identity));
        //SoldierTeam2.Add(Instantiate(Resources.Load<GameObject>("Prefabs/Soldiers/Soldier2"), new Vector3(175, -10, 0), Quaternion.identity));

        //var count = SoldierTeam1.Count;
        //for (int i = 0; i < count; i++)
        //{
        //    SoldierTeam1[i].GetComponent<SoldierController>().SetupChamp(true);
        //    SoldierTeam2[i].GetComponent<SoldierController>().SetupChamp(false);
        //}
    }
    #endregion

    public void aaaaa()
    {
        ObjectTest.GetComponent<Rigidbody2D>().AddForce(transform.up * 25f, ForceMode2D.Impulse);
    }

    #region Functions

    #region Show Damage Controller

    /// <summary>
    /// Gọi ở các object kế thừa, enable skill của hero
    /// </summary>
    /// <param name="obj">Skill object</param>
    /// <param name="vec">Tọa độ xuât hiện</param>
    /// <param name="quater">Độ nghiêng, xoay tròn</param>
    public void ShowDmg(int numberDmg, Vector3 vec, int dmgType)
    {
        vec += new Vector3(0, 2f, 0);
        CheckExistAndCreateDmgText(vec, DamageText, numberDmg, dmgType);
    }

    /// <summary>
    /// Trả về object skill đang ko hoạt động để xuất hiện
    /// </summary>
    /// <param name="obj"></param>
    /// <returns></returns>
    GameObject GetObjectNonActive(List<GameObject> obj, int numberDmg, int dmgType)
    {
        int count = obj.Count;
        for (int i = 0; i < count; i++)
        {
            if (!obj[i].activeSelf)
            {
                DamageTextControl[i].DamageNumber = numberDmg;
                DamageTextControl[i].DmgType = dmgType;
                return obj[i];
            }
        }
        return null;
    }

    /// <summary>
    /// Check khởi tạo và hiển thị hiệu ứng trúng đòn lên đối phương
    /// </summary>
    /// <param name="col"></param>
    private void CheckExistAndCreateDmgText(Vector3 col, List<GameObject> gobject, int numberDmg, int dmgType)
    {
        var a = GetObjectNonActive(gobject, numberDmg, dmgType);
        if (a == null)
        {
            gobject.Add(Instantiate(gobject[0], new Vector3(col.x, col.y, col.z), Quaternion.identity));
            var tmp = gobject[gobject.Count - 1].GetComponent<DamageTextController>();
            tmp.DmgType = dmgType;
            DamageTextControl.Add(tmp);
        }
        else
        {
            a.transform.position = col;
            a.SetActive(true);
        }
    }
    #endregion

    #region Auto create soldier controller
    /// <summary>
    /// Gọi ở các object kế thừa, enable skill của hero
    /// </summary>
    /// <param name="obj">Skill object</param>
    /// <param name="vec">Tọa độ xuât hiện</param>
    /// <param name="quater">Độ nghiêng, xoay tròn</param>
    private IEnumerator AutoCreateSoldier(int meleeSoldier, int archerSoldier)
    {
        meleeSoldier = meleeSoldier * 2;
        archerSoldier = archerSoldier * 2;
        var count1 = 0;
        var count2 = 0;
        Begin:

        if (meleeSoldier <= 0)
            goto CreateArcherSoldier;
        CreateMeleeSoldier:
        yield return new WaitForSeconds(DelayTimeBetween2Soldier);//Chờ đợi sinh 
        count1++;
        if (count1 < meleeSoldier)
            goto CreateMeleeSoldier;
        else
        {
            CheckExistAndCreateSoldier(new Vector3(-20, 5, 0), SoldierTeam1Type1, true);
            CheckExistAndCreateSoldier(new Vector3(-20, -2, 0), SoldierTeam1Type1, true);
            CheckExistAndCreateSoldier(new Vector3(-20, -10, 0), SoldierTeam1Type1, true);

            CheckExistAndCreateSoldier(new Vector3(220, 5, 0), SoldierTeam2Type1, false);
            CheckExistAndCreateSoldier(new Vector3(220, -2, 0), SoldierTeam2Type1, false);
            CheckExistAndCreateSoldier(new Vector3(220, -10, 0), SoldierTeam2Type1, false);
        }

        if (archerSoldier <= 0)
            goto Begin;
        CreateArcherSoldier:
        yield return new WaitForSeconds(DelayTimeBetween2Soldier);//Chờ đợi sinh 
        count2++;
        if (count2 < archerSoldier)
            goto CreateArcherSoldier;
        else
        {
            CheckExistAndCreateSoldier(new Vector3(-20, 5, 0), SoldierTeam1Type2, true);
            CheckExistAndCreateSoldier(new Vector3(-20, -2, 0), SoldierTeam1Type2, true);
            CheckExistAndCreateSoldier(new Vector3(-20, -10, 0), SoldierTeam1Type2, true);

            CheckExistAndCreateSoldier(new Vector3(220, 5, 0), SoldierTeam2Type2, false);
            CheckExistAndCreateSoldier(new Vector3(220, -2, 0), SoldierTeam2Type2, false);
            CheckExistAndCreateSoldier(new Vector3(220, -10, 0), SoldierTeam2Type2, false);
        }
        yield return new WaitForSeconds(DelayTimeBetween2GroupSoldier);//Chờ đợi sinh 
        count1 = 0;
        count2 = 0;
        goto Begin;
    }

    /// <summary>
    /// Trả về object đang ko hoạt động để xuất hiện
    /// </summary>
    /// <param name="obj">List object soldier</param>
    /// <returns></returns>
    GameObject GetSoldierNonActive(List<GameObject> obj)
    {
        int count = obj.Count;
        for (int i = 0; i < count; i++)
        {
            if (!obj[i].activeSelf)
            {
                return obj[i];
            }
        }
        return null;
    }

    /// <summary>
    /// Check khởi tạo và hiển thị soldier
    /// </summary>
    /// <param name="col">Tọa độ hiển thị</param>
    /// <param name="gobject">List object soldier</param>
    private void CheckExistAndCreateSoldier(Vector3 col, List<GameObject> gobject, bool isTeamLeft)
    {
        var a = GetSoldierNonActive(gobject);
        if (a == null)
        {
            gobject.Add(Instantiate(gobject[0], new Vector3(col.x, col.y, col.z), Quaternion.identity));
            //gobject[gobject.Count - 1].GetComponent<SoldierController>().SetupChamp(isTeamLeft);
        }
        else
        {
            a.transform.position = col;
            a.SetActive(true);
        }
    }
    #endregion

    #endregion

    #region Events
    public void ButtonHide(int type)
    {
        test[type].SetActive(!test[type].activeSelf);
    }

    public void LoginServer()
    {
        ServerConnection a = new ServerConnection();
        a.OnLoginButtonClick();
    }

    /// <summary>
    /// Ẩn hiện box control
    /// </summary>
    public void ButtonShowBoxControl()
    {
        StartCoroutine(GameSystem.MoveObjectCurve(true, BoxControl, BoxControl.transform.localPosition, new Vector2(IsBoxControlExpand ? BoxControlPosXOrigin : BoxControlPosXOrigin + BoxControlRangeMove, BoxControl.transform.localPosition.y), .5f, Curve));
        IsBoxControlExpand = !IsBoxControlExpand;
        BtnExpand.transform.localScale = new Vector3(IsBoxControlExpand ? 0 - BtnExpand.transform.localScale.x : Math.Abs(BtnExpand.transform.localScale.x), BtnExpand.transform.localScale.y, BtnExpand.transform.localScale.z);
    }

    /// <summary>
    /// Ẩn tất cả các UI khi click vào nền tối
    /// </summary>
    public void HideAllUI()
    {
        GlobalVariables.PopupChampSelectedInBattle.SetActive(false);
        GlobalVariables.BlackBGUI1InBattle.SetActive(false);
        GlobalVariables.BlackBGUI2InBattle.SetActive(false);
        GlobalVariables.ObjectArrowIntroInBattle.SetActive(false);
        for (int i = 0; i < GlobalVariables.ObjectButton3LaneInBattle.Length; i++)
        {
            GlobalVariables.ObjectButton3LaneInBattle[i].SetActive(false);
        }
    }

    /// <summary>
    /// Chọn đường xuất tướng
    /// </summary>
    public void ChooseLane(int lane)
    {
        for (int i = 0; i < GlobalVariables.ObjectButton3LaneInBattle.Length; i++)
        {
            GlobalVariables.ObjectButton3LaneInBattle[i].SetActive(false);
        }
        HideAllUI();
        SlotSelected = lane;
        LayoutChampTeam1[GlobalVariables.SlotChampSelectedInBattle].transform.SetParent(BoxControl.transform, false);

        ChampTeam1[GlobalVariables.SlotChampSelectedInBattle].SetActive(false);
        ChampTeam1[GlobalVariables.SlotChampSelectedInBattle].transform.position = new Vector3(-10, lane.Equals(0) ? 5 : lane.Equals(1) ? -2 : -10, 0);
        ChampTeam1[GlobalVariables.SlotChampSelectedInBattle].SetActive(true);

        //GlobalVariables.ObjectImgChampTempInBattle.SetActive(true);//Hình ảnh tạm của tướng
        //StartCoroutine(GameSystem.MoveObjectCurve(false, LayoutChampTeam1[GlobalVariables.SlotChampSelectedInBattle], LayoutChampTeam1[GlobalVariables.SlotChampSelectedInBattle].transform.position, ObjectButton3Lane[lane].transform.position, GameSettings.TimeMoveImgTempInBattle, Curve));
        //StartCoroutine(GameSystem.ChangeSizeRect(LayoutChampTeam1[GlobalVariables.SlotChampSelectedInBattle], true, Vector2.zero, new Vector2(130, 130), GameSettings.TimeMoveImgTempInBattle));
        LayoutChampTeam1[GlobalVariables.SlotChampSelectedInBattle].GetComponent<RectTransform>().sizeDelta = new Vector2(130, 130);
        //StartCoroutine(WaitForAction(0));//Chờ move xong img temp
        LayoutChampTeam1[GlobalVariables.SlotChampSelectedInBattle].transform.SetParent(ObjectContent4Lane[SlotSelected + 1].transform, false);
        UpdatePosChampInLaneUI();
    }

    /// <summary>
    /// Chờ thao tác
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    private IEnumerator WaitForAction(int type)
    {
        switch (type)
        {
            case 0://Chờ move xong img temp
                yield return new WaitUntil(() => !GlobalVariables.IsMoving);
                LayoutChampTeam1[GlobalVariables.SlotChampSelectedInBattle].transform.SetParent(ObjectContent4Lane[SlotSelected + 1].transform, false);
                UpdatePosChampInLaneUI();
                break;
        }
    }

    /// <summary>
    /// Sắp xếp lại tọa độ các UI của champ
    /// </summary>
    private void UpdatePosChampInLaneUI()
    {
        //Sắp xếp lại tọa độ
        var count = 0;
        foreach (Transform child in ObjectContent4Lane[1].transform)
        {
            child.transform.localPosition = new Vector3(75 + (count * 130), -75f, 0);
            count++;
        }
        count = 0;
        foreach (Transform child in ObjectContent4Lane[2].transform)
        {
            child.transform.localPosition = new Vector3(75 + (count * 130), -75f, 0);
            count++;
        }
        count = 0;
        foreach (Transform child in ObjectContent4Lane[3].transform)
        {
            child.transform.localPosition = new Vector3(75 + (count * 130), -75f, 0);
            count++;
        }
    }
    #endregion
}