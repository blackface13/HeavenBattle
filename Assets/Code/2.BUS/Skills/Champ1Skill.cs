using Assets.Code._4.CORE;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Skill kéo đối phương từ tầm xa lại gần
/// </summary>
public class Champ1Skill : SkillController
{
    //Tùy chọn riêng cho skill
    #region Variables
    [TitleGroup("Cài đặt Skill")]
    [HorizontalGroup("Cài đặt Skill/Split", Width = 1f)]
    [TabGroup("Cài đặt Skill/Split/Tab1", "Cấu hình thông số")]
    AnimationCurve animCurve;

    private float PosXOrigin;//Tọa độ ban đầu khi xuất chiêu
    #endregion

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
        PosXOrigin = this.transform.position.x;
    }
    #endregion

    #region Functions
    void Update()
    {
        this.transform.Translate(IsViewLeft ? -MoveSpeed * Time.deltaTime : MoveSpeed * Time.deltaTime, 0, 0);
    }

    /// <summary>
    /// Xử lý va chạm
    /// </summary>
    public override void OnTriggerEnter2D(Collider2D col)
    {
        //base.OnTriggerEnter2D(col);
        ThisCollider.enabled = false;
        StartCoroutine(GameSystem.MoveObjectCurve(false, col.gameObject, col.gameObject.transform.position, new Vector3(PosXOrigin, col.gameObject.transform.position.y, col.gameObject.transform.position.z), .5f, animCurve));
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
