using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Code._4.CORE;
public class Champ4Atk1 : SkillController
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

    /// <summary>
    /// Xử lý va chạm
    /// </summary>
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
