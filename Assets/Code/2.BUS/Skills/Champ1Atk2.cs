using Assets.Code._4.CORE;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Champ1Atk2 : SkillController
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
    }
    //private void OnCollisionStay2D(Collision2D col)
    //{
    //    try
    //    {

    //    }
    //    catch { }
    //}
    #endregion
}
