﻿using Assets.Code._4.CORE;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Champ1Atk3 : SkillController
{
    #region Initialize
    /// <summary>
    /// Khởi tạo và setup các thông số
    /// </summary>
    public override void Awake()
    {
        base.Awake();
    }

    // Start is called before the first frame upd
    public override void Start()
    {
        base.Start();
    }

    public override void OnEnable()
    {
        base.OnEnable();
    }
    #endregion

    #region Functions
    // Update is called once per frame
    //void Update()
    //{
    //    this.transform.Translate(IsViewLeft ? -MoveSpeed * Time.deltaTime : MoveSpeed * Time.deltaTime, 0, 0);
    //}

    /// Xử lý va chạm
    public override void OnTriggerEnter2D(Collider2D col)
    {
        base.OnTriggerEnter2D(col);
        //try
        //{
        //    if ((this.gameObject.layer.Equals((int)GameSettings.LayerSettings.SkillTeam1ToVictim) && col.gameObject.layer.Equals((int)GameSettings.LayerSettings.HeroTeam2)) || (this.gameObject.layer.Equals((int)GameSettings.LayerSettings.SkillTeam2ToVictim) && col.gameObject.layer.Equals((int)GameSettings.LayerSettings.HeroTeam1)))
        //    {
        //        if (!string.IsNullOrEmpty(NameEffectExtension1))
        //            CheckExistAndCreateEffectExtension(this.transform.position, EffectExtension); //Hiển thị hiệu ứng trúng đòn lên đối phương
        //        if (!string.IsNullOrEmpty(NameEffectExtension2))
        //            CheckExistAndCreateEffectExtension(this.transform.position, EffectExtension2); //Hiển thị hiệu ứng trúng đòn lên đối phương
        //        if (!string.IsNullOrEmpty(NameEffectExtension3))
        //            CheckExistAndCreateEffectExtension(this.transform.position, EffectExtension3); //Hiển thị hiệu ứng trúng đòn lên đối phương

        //        BattleSystem.ShowDmg(Random.Range(123, 4564), col.transform.position);
        //        if (IsDisableColliderWhenTrigger) //Nếu kiểu va chạm rồi ẩn
        //        {
        //            if (!FirstAtk)
        //            {
        //                Hide(gameObject);
        //                FirstAtk = true;
        //            }
        //            else StartCoroutine(HideParticle(this.gameObject, DelayTimeBeforeHidden)); //Ẩn object sau khi va chạm 
        //        }
        //    }
        //}
        //catch { }
    }
    private void OnCollisionStay2D(Collision2D col)
    {
        try
        {

        }
        catch { }
    }
    #endregion
}
