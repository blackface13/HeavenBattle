using Assets.Code._4.CORE;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Skill giống Lux
/// </summary>
public class Champ2Skill : SkillController
{
    //Tùy chọn riêng cho skill
    #region Variables

    [TitleGroup("Cài đặt Skill")]
    [HorizontalGroup("Cài đặt Skill/Split", Width = 1f)]
    [TabGroup("Cài đặt Skill/Split/Tab1", "Cấu hình thông số")]
    public float ScaleColliderSpeed, MaxScale;

    private BoxCollider2D ThisBoxCollider;
    private float CurentSizeXBoxCollider;
    //Tốc độ giãn nở scale
    #endregion

    #region Initialize
    /// <summary>
    /// Khởi tạo và setup các thông số
    /// </summary>
    public override void Awake()
    {
        base.Awake();
        ThisBoxCollider = this.GetComponent<BoxCollider2D>();
    }

    // Start is called before the first frame upd
    public override void Start()
    {
        base.Start();
    }

    public override void OnEnable()
    {
        base.OnEnable();
        ThisBoxCollider.size = new Vector2(1, 1);
        CurentSizeXBoxCollider = ThisBoxCollider.size.x;
    }
    #endregion

    #region Functions
    private void Update()
    {
        if (ThisCollider.enabled)
        {
            if (CurentSizeXBoxCollider >= MaxScale)
            {
                CurentSizeXBoxCollider = MaxScale;
            }
            else
            {
                CurentSizeXBoxCollider += ScaleColliderSpeed * Time.deltaTime;
            }
            ThisBoxCollider.size = new Vector2(CurentSizeXBoxCollider, 1);
        }
    }

    /// <summary>
    /// Xử lý va chạm
    /// </summary>
    public override void OnTriggerEnter2D(Collider2D col)
    {
        base.OnTriggerEnter2D(col);
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
