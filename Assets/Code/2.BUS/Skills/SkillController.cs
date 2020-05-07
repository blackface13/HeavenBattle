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
    public float MoveSpeed;//Tốc độ bay của skill

    [TitleGroup("Cài đặt Skill")]
    [HorizontalGroup("Cài đặt Skill/Split", Width = 1f)]
    [TabGroup("Cài đặt Skill/Split/Tab1", "Cấu hình thông số")]
    public int CollisionType = 0;//Kiểu va chạm. 0 = chạm là ẩn, 1 = xuyên đối thủ

    [Header("Draw Curve")]
    public AnimationCurve moveCurve;
    public bool IsViewLeft;//Hướng trái hoặc phải
    public bool PercentHealthAtkEnable = false;//Đòn đánh này có trừ % máu hay ko
    public float PercentHealthCreated = 0;//Số % máu gây ra cho đối phương
    public float TimeMove;//Thời gian move của các Object muốn định hướng di chuyển sẵn
                          // public HeroesProperties DataValues;//Các chỉ số của nhân vật sẽ được truyền vào khi khởi tạo skill
    public int DamagePercent = 100;//Số phần trăm damage gây ra so với damage gốc (default = 100)
    public int SkillType = 0;//Kiểu sát thương, vật lý hoặc phép (default = vật lý)
    /// <summary>
    /// Kiểu va chạm. 0 = chạm là ẩn, 1 = xuyên đối thủ
    /// </summary>
    public float TimeDelay;//Thời gian delay trước khi cho phép va chạm
    //public BattleSystem Battle;

    public float TimeAction;//Thời gian va chạm được hoạt động trước khi bị vô hiệu hóa
    public int Team;//0: team trái, 1 team phải
    public float TimeStatus;//Thời gian của hiệu ứng, set trong hàm kế thừa
    public float PercentDamageFireBurn = 30;//Số % dame gây ra hiệu ứng thiêu đốt cho đối phương, mặc định 30%
    public float RatioStatus;//Tỉ lệ gây ra hiệu ứng, set trong hàm kế thừa
    public status Status;
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
    //public HeroBase Hero;

    public AudioClip[] SoundClip;//Âm thanh của skill
    public AudioClip[] SoundClipHited;//Âm thanh của skill

    public BattleSystemController BattleSystem;
    #endregion

    #region Initialize
    public virtual void Awake()
    {
        BattleSystem = GameObject.Find("Controller").GetComponent<BattleSystemController>();
    }
    // Start is called before the first frame update
    void Start()
    {
    }

    /// <summary>
    /// Khởi tạo hiệu ứng effect riêng cho từng skill của hero (nếu có)
    /// </summary>
    public virtual void SetupEffectExtension(string prefabname)
    {
        EffectExtension.Add((GameObject)Instantiate(Resources.Load<GameObject>("Prefabs/Skills/" + prefabname), new Vector3(-1000, -1000, 0), Quaternion.identity));
        EffectExtension[0].SetActive(false);
    }

    /// <summary>
    /// Khởi tạo cài đặt skill
    /// </summary>
    public virtual void SetupSkill(bool isTeamLeft)
    {
        this.gameObject.layer = isTeamLeft ? (int)GameSettings.LayerSettings.SkillTeam1ToVictim : (int)GameSettings.LayerSettings.SkillTeam2ToVictim;
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

    /// <summary>
    /// Tự động vô hiệu hóa va chạm sau 1 khoảng thời gian
    /// </summary>
    public IEnumerator AutoDisCol(float time, GameObject obj)
    {
        yield return new WaitForSeconds(time);
        try
        {
            obj.GetComponent<Collider2D>().enabled = false;
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
            obj.GetComponent<Collider2D>().enabled = true;
        }
        catch { }
    }

    #endregion
}
