using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero4 : HeroController
{
    /// <summary>
    /// Khởi tạo thông số cho nhân vật
    /// </summary>
    public override void Awake()
    {
        base.Awake();
    }

    public override void Start()
    {
        base.Start();
    }

    #region Functions
    //Thực hiện tung skil, hàm kế thừa
    public override void ActionSkill(int type)
    {
        base.ActionSkill(type);
    }
    #endregion
}
