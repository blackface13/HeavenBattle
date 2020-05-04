using Assets.Code._4.CORE;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Champ2Atk : SkillController
{
    #region Initialize
    /// <summary>
    /// Khởi tạo và setup các thông số
    /// </summary>
    private void Awake()
    {
        CollisionType = 0;//
        SetupEffectExtension("Champ2AtkEffect"); //Khởi tạo hiệu ứng effect riêng cho từng skill của hero (nếu có)
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    private void OnEnable()
    {

    }
    #endregion

    #region Functions
    // Update is called once per frame
    void Update()
    {
        this.transform.Translate(IsViewLeft ? -MoveSpeed * Time.deltaTime : MoveSpeed * Time.deltaTime, 0, 0);
    }

    /// Xử lý va chạm
    private void OnTriggerEnter2D(Collider2D col)
    {
        try
        {
            if ((this.gameObject.layer.Equals((int)GameSettings.LayerSettings.SkillTeam1ToVictim) && col.gameObject.layer.Equals((int)GameSettings.LayerSettings.HeroTeam2)) || (this.gameObject.layer.Equals((int)GameSettings.LayerSettings.SkillTeam2ToVictim) && col.gameObject.layer.Equals((int)GameSettings.LayerSettings.HeroTeam1)))
            {
                CheckExistAndCreateEffectExtension(this.transform.position, EffectExtension); //Hiển thị hiệu ứng trúng đòn lên đối phương
                //var victim = col.GetComponent<HeroBase>();
                //var timestatusaction = TimeStatus - (TimeStatus * victim.DataValues.vTenacity / 100f); //Tính thời gian gây ra hiệu ứng
                //if (this.gameObject.activeSelf)
                //    StartCoroutine(victim.ActionBuffValues("vAtkSpeed", -victim.DataValues.vAtkSpeed * 10 / 100, timestatusaction)); //Làm chậm đòn đánh của đối phương 10%
                print("ok");
                if (CollisionType.Equals(0)) //Nếu kiểu va chạm rồi ẩn
                   StartCoroutine(HideParticle(this.gameObject, 1f)); //Ẩn object sau khi va chạm 

            }
        }
        catch { }
    }
    #endregion
}
