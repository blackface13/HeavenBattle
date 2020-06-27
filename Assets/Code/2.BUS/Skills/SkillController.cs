using Assets.Code._4.CORE;
using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillController : MonoBehaviour
{
    #region Variables 

    [TitleGroup("Cài đặt Skill")]
    [HorizontalGroup("Cài đặt Skill/Split", Width = 1f)]
    [TabGroup("Cài đặt Skill/Split/Tab1", "Cấu hình thông số")]
    public float MoveSpeed, DelayTimeBeforeHidden, DelayTimeBeforeEnalbleCollider, DelayTimeBeforeDisableCollider, DelayTimeToHideAfterShow;
    //Tốc độ bay của skill, thời gian delay trước khi ẩn, thời gian delay trước khi hủy bỏ va chạm

    [TitleGroup("Cài đặt Skill")]
    [HorizontalGroup("Cài đặt Skill/Split", Width = 1f)]
    [TabGroup("Cài đặt Skill/Split/Tab1", "Cấu hình thông số")]
    public int SkillType = 0, DamagePercent = 100;
    //Kiểu sát thương, vật lý hoặc phép (default = vật lý)
    //Số phần trăm damage gây ra so với damage gốc (default = 100)

    [TitleGroup("Cài đặt Skill")]
    [HorizontalGroup("Cài đặt Skill/Split", Width = 1f)]
    [TabGroup("Cài đặt Skill/Split/Tab1", "Cấu hình thông số")]
    public string NameEffectExtension1, NameEffectExtension2, NameEffectExtension3;

    [TitleGroup("Cài đặt Skill")]
    [HorizontalGroup("Cài đặt Skill/Split", Width = 1f)]
    [TabGroup("Cài đặt Skill/Split/Tab1", "Cấu hình thông số")]
    public bool IsEnableColliderWhenStart, IsDisableColliderWhenTrigger, IsCustomPositionEffectHit;//Có disable va chạm khi chạm đối thủ hay ko

    [TitleGroup("Cài đặt Skill")]
    [HorizontalGroup("Cài đặt Skill/Split", Width = 1f)]
    [TabGroup("Cài đặt Skill/Split/Tab1", "Cấu hình thông số")]
    public Vector3 HitEffectCustomPos;//Có disable va chạm khi chạm đối thủ hay ko
    //Tên các hiệu ứng mở rộng

    [Header("Draw Curve")]
    public AnimationCurve moveCurve;
    public bool IsViewLeft;//Hướng trái hoặc phải
    public bool PercentHealthAtkEnable = false;//Đòn đánh này có trừ % máu hay ko
    public float PercentHealthCreated = 0;//Số % máu gây ra cho đối phương
    public float TimeMove;//Thời gian move của các Object muốn định hướng di chuyển sẵn
                          // public HeroesProperties DataValues;//Các chỉ số của nhân vật sẽ được truyền vào khi khởi tạo skill
    /// <summary>
    /// Kiểu va chạm. 0 = chạm là ẩn, 1 = xuyên đối thủ
    /// </summary>
    public float TimeDelay;//Thời gian delay trước khi cho phép va chạm
                           //public BattleSystem Battle;

    public bool FirstAtk;
    public float TimeAction;//Thời gian va chạm được hoạt động trước khi bị vô hiệu hóa
    public int Team;//0: team trái, 1 team phải
    public float TimeStatus;//Thời gian của hiệu ứng, set trong hàm kế thừa
    public float PercentDamageFireBurn = 30;//Số % dame gây ra hiệu ứng thiêu đốt cho đối phương, mặc định 30%
    public float RatioStatus;//Tỉ lệ gây ra hiệu ứng, set trong hàm kế thừa
    public status Status;
    public Collider2D ThisCollider;
    public ChampModel DataValues;
    public enum status
    {
        Normal,//Bình thường
        Silent,//Câm lặng
        Stun,//Choáng
        Root,//Giữ chân
        Ice,//Đóng băng
        Static,//Tĩnh, không thể bị chọn làm mục tiêu
        Blind,//Mù
        Slow,//Làm chậm
    }
    public Effect Eff;
    public enum Effect
    {
        Normal,//Bình thường
        Fire,//Thiêu đốt
        Poison,//Trúng độc
    }
    public List<GameObject> EffectExtension;
    public List<GameObject> EffectExtension2;
    public List<GameObject> EffectExtension3;
    //public HeroBase Hero;

    public AudioClip[] SoundClip;//Âm thanh của skill
    public AudioClip[] SoundClipHited;//Âm thanh của skill

    //public BattleSystemController BattleSystem;
    #endregion

    #region Initialize
    public virtual void Awake()
    {
        ThisCollider = this.GetComponent<Collider2D>();
        if (IsEnableColliderWhenStart)
            ThisCollider.enabled = true;
        else
            ThisCollider.enabled = false;
        //BattleSystem = GameObject.Find("Controller").GetComponent<BattleSystemController>();
        if (!string.IsNullOrEmpty(NameEffectExtension1))
            SetupEffectExtension(NameEffectExtension1); //Khởi tạo hiệu ứng effect riêng cho từng skill của hero (nếu có)
        if (!string.IsNullOrEmpty(NameEffectExtension2))
            SetupEffectExtension2(NameEffectExtension2); //Khởi tạo hiệu ứng effect riêng cho từng skill của hero (nếu có)
        if (!string.IsNullOrEmpty(NameEffectExtension3))
            SetupEffectExtension3(NameEffectExtension3); //Khởi tạo hiệu ứng effect riêng cho từng skill của hero (nếu có)

       // IsCustomPositionEffectHit = true;//Mặc định = true thì sẽ hiển thị hiệu ứng trúng đòn +2f tọa độ Y lên đối phương

        if (HitEffectCustomPos == null)
            HitEffectCustomPos = this.transform.position;
    }
    // Start is called before the first frame update
    public virtual void Start()
    {
    }

    public virtual void OnEnable()
    {
        try
        {
            //Tự động ẩn skill sau 1 khoảng thời gian
            if (DelayTimeToHideAfterShow > 0)
                StartCoroutine(AutoHiden(DelayTimeToHideAfterShow, this.gameObject));

            //Bật tắt va chạm khi enable
            if (IsEnableColliderWhenStart)
                ThisCollider.enabled = true;
            else
                ThisCollider.enabled = false;

            //Tự enable va chạm sau 1 khoảng time
            if (DelayTimeBeforeEnalbleCollider != 0)
                StartCoroutine(AutoEnableCol(DelayTimeBeforeEnalbleCollider, gameObject));

            //Tự disable va chạm sau 1 khoảng time
            if (DelayTimeBeforeDisableCollider != 0)
                StartCoroutine(AutoDisCol(DelayTimeBeforeDisableCollider, gameObject));
            ChangeView(IsViewLeft);
        }
        catch
        { }
    }

    /// <summary>
    /// Khởi tạo hiệu ứng effect riêng cho từng skill của hero (nếu có)
    /// </summary>
    public virtual void SetupEffectExtension(string prefabname)
    {
        EffectExtension.Add((GameObject)Instantiate(Resources.Load<GameObject>("Prefabs/Skills/" + prefabname), new Vector3(-1000, -1000, 0), Quaternion.identity));
        EffectExtension[0].SetActive(false);
    }
    public virtual void SetupEffectExtension2(string prefabname)
    {
        EffectExtension2.Add((GameObject)Instantiate(Resources.Load<GameObject>("Prefabs/Skills/" + prefabname), new Vector3(-1000, -1000, 0), Quaternion.identity));
        EffectExtension2[0].SetActive(false);
    }
    public virtual void SetupEffectExtension3(string prefabname)
    {
        EffectExtension3.Add((GameObject)Instantiate(Resources.Load<GameObject>("Prefabs/Skills/" + prefabname), new Vector3(-1000, -1000, 0), Quaternion.identity));
        EffectExtension3[0].SetActive(false);
    }

    /// <summary>
    /// Khởi tạo cài đặt skill
    /// </summary>
    public virtual void SetupSkill(bool isTeamLeft, ChampModel dataValue)
    {
        this.gameObject.layer = isTeamLeft ? (int)GameSettings.LayerSettings.SkillTeam1ToVictim : (int)GameSettings.LayerSettings.SkillTeam2ToVictim;
        DataValues = dataValue;
    }
    #endregion

    #region Functions

    // Update is called once per frame
    //void Update()
    //{

    //}

    /// <summary>
    /// Thay đổi hướng của skill
    /// </summary>
    public void ChangeView(bool isViewLeft)
    {
        this.transform.localScale = new Vector3(isViewLeft ? 0 - Mathf.Abs(this.transform.localScale.x) : Mathf.Abs(this.transform.localScale.x), this.transform.localScale.y, this.transform.localScale.z);
    }

    /// <summary>
    /// Check khởi tạo và hiển thị hiệu ứng trúng đòn lên đối phương
    /// </summary>
    /// <param name="col"></param>
    public void CheckExistAndCreateEffectExtension(Vector3 col, List<GameObject> objectExtension)
    {
        //col = new Vector3(col.x, col.y + 1.5f, col.z);//Dòng này fix vì thiết kế nhân vật position nằm dưới chân
        var a = GetObjectNonActive(objectExtension);
        if (a == null)
        {
            objectExtension.Add(Instantiate(objectExtension[0], new Vector3(col.x, col.y, 0), Quaternion.identity));
        }
        else
        {
            a.transform.localScale = new Vector3(IsViewLeft ? 0 - Math.Abs(a.transform.localScale.x) : Math.Abs(a.transform.localScale.x), a.transform.localScale.y, a.transform.localScale.z);
            ShowSkill(a, new Vector3(col.x, col.y, 0), Quaternion.identity);
        }
    }

    /// <summary>
    /// Hiện các object skill nhỏ nhơn như hiệu ứng hoặc các object con của skill
    /// </summary>
    public void ShowSkill(GameObject obj, Vector3 vec, Quaternion quater)
    {
        obj.transform.position = vec;
        obj.transform.rotation = quater;
        obj.SetActive(true);
    }

    /// <summary>
    /// Trả về object skill đang ko hoạt động để xuất hiện
    /// </summary>
    /// <param name="obj"></param>
    /// <returns></returns>
    public GameObject GetObjectNonActive(List<GameObject> obj)
    {
        int count = obj.Count;
        for (int i = 0; i < count; i++)
        {
            if (!obj[i].activeSelf)
                return obj[i];
        }
        return null;
    }

    /// <summary>
    /// Bật tắt collider
    /// </summary>
    public virtual void ColliderControl(bool isEnable)
    {
        this.GetComponent<Collider2D>().enabled = isEnable;
    }

    public virtual void CreateDamage(int dmg, Vector3 pos)
    {
        //BattleSystem.ShowDmg(UnityEngine.Random.Range(123, 4564), pos);
    }
    #endregion

    #region Ẩn hoặc tự động ẩn skill 

    //Tự động ẩn object skill sau 1 khoảng thời gian
    public virtual IEnumerator AutoHiden(float time, GameObject obj)
    {
        yield return new WaitForSeconds(time);
        Hide(obj);
    }

    /// <summary>
    /// Ẩn object skill ngay lập tức
    /// </summary>
    public virtual void Hide(GameObject obj)
    {
        obj.SetActive(false);
        obj.transform.position = new Vector3(-1000, -1000, 0);
        obj.transform.localEulerAngles = new Vector3();
    }

    /// <summary>
    /// Ẩn object skill particle sau 1 khoảng time
    /// </summary>
    public virtual IEnumerator HideParticle(GameObject obj, float delayTime)
    {
        obj.transform.position = new Vector3(-1000, -1000, 0);
        obj.transform.localEulerAngles = new Vector3();
        yield return new WaitForSeconds(delayTime);
        obj.SetActive(false);
    }

    /// <summary>
    /// Dừng particle kèm theo move object không cho sản sinh thêm object và ẩn sau 1 khoảng time (Dành cho các hiệu ứng particle theo object)
    /// </summary>
    public virtual IEnumerator ParticleStop(GameObject obj, ParticleSystem parEffect, float time)
    {
        obj.transform.position = new Vector3(-1000, -1000, 0);
        obj.transform.localEulerAngles = new Vector3();
        parEffect.Stop();
        yield return new WaitForSeconds(time);
        obj.SetActive(false);
    }
    #endregion

    #region Điều khiển va chạm 

    public virtual void OnTriggerEnter2D(Collider2D col)
    {
        try
        {
            if ((this.gameObject.layer.Equals((int)GameSettings.LayerSettings.SkillTeam1ToVictim) && col.gameObject.layer.Equals((int)GameSettings.LayerSettings.HeroTeam2))
                || (this.gameObject.layer.Equals((int)GameSettings.LayerSettings.SkillTeam2ToVictim) && col.gameObject.layer.Equals((int)GameSettings.LayerSettings.HeroTeam1))
                || (this.gameObject.layer.Equals((int)GameSettings.LayerSettings.SkillTeam1ToVictim) && col.gameObject.layer.Equals((int)GameSettings.LayerSettings.SoldierTeam2))
                || (this.gameObject.layer.Equals((int)GameSettings.LayerSettings.SkillTeam2ToVictim) && col.gameObject.layer.Equals((int)GameSettings.LayerSettings.SoldierTeam1))
                || (this.gameObject.layer.Equals((int)GameSettings.LayerSettings.SkillTeam1ToVictim) && col.gameObject.layer.Equals((int)GameSettings.LayerSettings.HomeTeam2))
                || (this.gameObject.layer.Equals((int)GameSettings.LayerSettings.SkillTeam2ToVictim) && col.gameObject.layer.Equals((int)GameSettings.LayerSettings.HomeTeam1))
                )
            {
                //print(IsCustomPositionEffectHit ? HitEffectCustomPos : (col.transform.position + GameSettings.PositionShowEffectFix));
                if (!string.IsNullOrEmpty(NameEffectExtension1))
                    CheckExistAndCreateEffectExtension(IsCustomPositionEffectHit ? HitEffectCustomPos : (col.transform.position + GameSettings.PositionShowEffectFix), EffectExtension); //Hiển thị hiệu ứng trúng đòn lên đối phương
                if (!string.IsNullOrEmpty(NameEffectExtension2))
                    CheckExistAndCreateEffectExtension(IsCustomPositionEffectHit ? HitEffectCustomPos : (col.transform.position + GameSettings.PositionShowEffectFix), EffectExtension2); //Hiển thị hiệu ứng trúng đòn lên đối phương
                if (!string.IsNullOrEmpty(NameEffectExtension3))
                    CheckExistAndCreateEffectExtension(IsCustomPositionEffectHit ? HitEffectCustomPos : (col.transform.position + GameSettings.PositionShowEffectFix), EffectExtension3); //Hiển thị hiệu ứng trúng đòn lên đối phương

                if (IsDisableColliderWhenTrigger) //Nếu kiểu va chạm rồi ẩn
                {
                    if (!FirstAtk)
                    {
                        Hide(gameObject);
                        FirstAtk = true;
                    }
                    else
                    {
                        if (gameObject.activeSelf)
                            StartCoroutine(HideParticle(this.gameObject, DelayTimeBeforeHidden)); //Ẩn object sau khi va chạm 
                    }
                }
            }
        }
        catch { }
    }

    /// <summary>
    /// Tự động vô hiệu hóa va chạm sau 1 khoảng thời gian
    /// </summary>
    public IEnumerator AutoDisCol(float time, GameObject obj)
    {
        yield return new WaitForSeconds(time);
        try
        {
            if (obj != null)
                obj.GetComponent<Collider2D>().enabled = false;
            else
                ThisCollider.enabled = false;
        }
        catch { }
    }

    /// <summary>
    /// Tự động cho phép va chạm sau 1 khoảng thời gian
    /// </summary>
    public IEnumerator AutoEnableCol(float time, GameObject obj)
    {
        yield return new WaitForSeconds(time);
        try
        {
            if (obj != null)
                obj.GetComponent<Collider2D>().enabled = true;
            else
                ThisCollider.enabled = true;
        }
        catch { }
    }

    #endregion
}
