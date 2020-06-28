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
    public GameObject ImgSkillExtension;//Hình ảnh của skill này
    private float PosXOrigin;//Tọa độ ban đầu khi xuất chiêu
    private bool AllowMove;
    HeroController Victim;
    private Color32 OriginalColor = new Color(255, 255, 255, 255);
    private Color32 HideColor = new Color(255, 255, 255, 0);
    private Color32 CurentColor;
    #endregion

    #region Initialize
    /// <summary>
    /// Khởi tạo và setup các thông số
    /// </summary>
    public override void Awake()
    {
        base.Awake();
        CurentColor = ImgSkillExtension.GetComponent<SpriteRenderer>().color;
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
        ImgSkillExtension.GetComponent<SpriteRenderer>().color = OriginalColor;
        AllowMove = true;
    }
    #endregion

    #region Functions
    void Update()
    {
        if (AllowMove)
            this.transform.Translate(IsViewLeft ? -MoveSpeed * Time.deltaTime : MoveSpeed * Time.deltaTime, 0, 0);
    }

    /// <summary>
    /// Xử lý va chạm
    /// </summary>
    public override void OnTriggerEnter2D(Collider2D col)
    {
        //base.OnTriggerEnter2D(col);
        AllowMove = false;
        ThisCollider.enabled = false;
        ImgSkillExtension.GetComponent<SpriteRenderer>().color = HideColor;//Set độ mờ của object về = 0 khi va chạm với đối phương
        Victim = col.GetComponent<HeroController>();
        Victim.CurentAction = HeroController.ChampActions.Standing;

        StartCoroutine(GameSystem.MoveObjectCurve(false, col.gameObject, col.gameObject.transform.position, new Vector3(PosXOrigin, col.gameObject.transform.position.y, col.gameObject.transform.position.z), .5f, moveCurve));//Kéo đối phương về lại gần

        StartCoroutine(WaitSkill());
    }

    private IEnumerator WaitSkill()
    {
        yield return new WaitForSeconds(.5f);
        Victim.CurentAction = HeroController.ChampActions.Moving;
        Hide(this.gameObject);
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
