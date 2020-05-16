using Assets.Code._4.CORE;
using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierController : MonoBehaviour
{
    #region Variables
    [TitleGroup("Cài đặt nhân vật")]
    [HorizontalGroup("Cài đặt nhân vật/Split", Width = 1f)]
    [TabGroup("Cài đặt nhân vật/Split/Tab1", "Cấu hình thông số")]
    public float SoldierID, MoveSpeed, DetectRange;

    [TitleGroup("Cài đặt nhân vật")]
    [HorizontalGroup("Cài đặt nhân vật/Split", Width = 1f)]
    [TabGroup("Cài đặt nhân vật/Split/Tab1", "Cấu hình thông số")]
    public string PrefabNameAtk1, PrefabNameAtk2, PrefabNameAtk3, PrefabNameSkill;//Tên các prefabs

    [TitleGroup("Cài đặt nhân vật")]
    [HorizontalGroup("Cài đặt nhân vật/Split", Width = 1f)]
    [TabGroup("Cài đặt nhân vật/Split/Tab1", "Cấu hình thông số")]
    public bool IsMeleeChamp;//Là nhâm vật cận chiến

    [TitleGroup("Cài đặt nhân vật")]
    [HorizontalGroup("Cài đặt nhân vật/Split", Width = 1f)]
    [TabGroup("Cài đặt nhân vật/Split/Tab1", "Cấu hình thông số")]
    public Vector2 Atk1ShowPos, Atk2ShowPos, Atk3ShowPos, SkillShowPos, HPBarPos;//Tọa độ các hiệu ứng kỹ năng

    public bool IsEnemyInLeft;//Đối phương đang bên trái hoặc phải
    public bool IsViewLeft;//Hướng nhìn trái hoặc phải
    public bool IsInRangeDetect;//Đã phát hiện đối phương trong tầm
    SoldierTargetDetect ChampDetect;//Sự kiện phát hiện trong vùng tấn công
    HeroSafeDetect ChampSafe;//Sự kiện phát hiện trong vùng an toàn
    Animator Anim;//Hoạt cảnh nhân vật
    public bool IsTeamLeft;//Team bên trái hoặc bên phải
    private GameObject HPBarObject, HPBarParentObject;//Scale của thanh máu
    public BattleSystemController BattleSystem;
    public Collider2D ThisCollider;
    public Rigidbody2D ThisRigid;

    public List<GameObject> Atk1Object;
    public List<GameObject> Atk2Object;
    public List<GameObject> Atk3Object;
    public List<GameObject> SkillObject;

    public ChampModel DataValues;
    public bool IsAlive;
    private bool IsDieing;
    private enum ChampActions
    {
        Standing,
        Moving,
        Attacking,
        Hiting,
        Skilling,
        LeavingDangerRange,
        Dieing,
    }
    private ChampActions CurentAction;
    public States ChampState;
    public enum States
    {
        Normal, //Bình thường
        Silent, //Câm lặng
        Stun, //Choáng
        Root, //Giữ chân
        Ice, //Đóng băng
        Static, //Tĩnh, không thể bị chọn làm mục tiêu
        Blind, //Mù
        Slow, //Làm chậm
    }
    #endregion

    #region Initialize
    public virtual void Awake()
    {
        ThisCollider = GetComponent<Collider2D>();
        ThisRigid = GetComponent<Rigidbody2D>();
        BattleSystem = GameObject.Find("Controller").GetComponent<BattleSystemController>();
        HPBarParentObject = Instantiate(Resources.Load<GameObject>("Prefabs/Soldiers/SoldierHPBar"), Vector3.zero, Quaternion.identity);
        HPBarParentObject.transform.SetParent(this.transform, false);
        HPBarParentObject.transform.localPosition = new Vector3(HPBarPos.x, HPBarPos.y, 0);
        HPBarObject = HPBarParentObject.transform.GetChild(0).gameObject;
        try
        {
            DataValues = GameSettings.SoldierDefault.Find(x => x.ID == (SoldierID - 1)).Clone();
            DataValues.vHealthCurrent = DataValues.vHealth;
        }
        catch
        {
            DataValues = new ChampModel();
        }
    }

    public virtual void Start()
    {
        if (!string.IsNullOrEmpty(PrefabNameAtk1))
        {
            Atk1Object = new List<GameObject>();
            Atk1Object.Add((GameObject)Instantiate(Resources.Load<GameObject>("Prefabs/Skills/" + PrefabNameAtk1), new Vector3(-1000, -1000, 0), Quaternion.identity));
            Atk1Object[0].GetComponent<SkillController>().SetupSkill(IsTeamLeft, DataValues);
            Atk1Object[0].SetActive(false);
        }
        if (!string.IsNullOrEmpty(PrefabNameAtk2))
        {
            Atk2Object = new List<GameObject>();
            Atk2Object.Add((GameObject)Instantiate(Resources.Load<GameObject>("Prefabs/Skills/" + PrefabNameAtk2), new Vector3(-1000, -1000, 0), Quaternion.identity));
            Atk2Object[0].GetComponent<SkillController>().SetupSkill(IsTeamLeft, DataValues);
            Atk2Object[0].SetActive(false);
        }
        if (!string.IsNullOrEmpty(PrefabNameAtk3))
        {
            Atk3Object = new List<GameObject>();
            Atk3Object.Add((GameObject)Instantiate(Resources.Load<GameObject>("Prefabs/Skills/" + PrefabNameAtk3), new Vector3(-1000, -1000, 0), Quaternion.identity));
            Atk3Object[0].GetComponent<SkillController>().SetupSkill(IsTeamLeft, DataValues);
            Atk3Object[0].SetActive(false);
        }
        if (!string.IsNullOrEmpty(PrefabNameSkill))
        {
            SkillObject = new List<GameObject>();
            SkillObject.Add((GameObject)Instantiate(Resources.Load<GameObject>("Prefabs/Skills/" + PrefabNameSkill), new Vector3(-1000, -1000, 0), Quaternion.identity));
            SkillObject[0].GetComponent<SkillController>().SetupSkill(IsTeamLeft, DataValues);
            SkillObject[0].SetActive(false);
        }

        //StartCoroutine(RegenHealth());
        //RaycastHit2D hit = Physics2D.Raycast(this.transform.position, transform.forward * -10, 3f);
        //if (hit.collider != null)
        //    print("fgfgf");

    }

    public virtual void OnEnable()
    {
        IsAlive = true;
        ThisCollider.enabled = true;
        ThisRigid.constraints = RigidbodyConstraints2D.None;
        ThisRigid.constraints = RigidbodyConstraints2D.FreezePositionX;
        ThisRigid.constraints = RigidbodyConstraints2D.FreezeRotation;
        StartCoroutine(WaitForEvents(0));//Chờ nhân vật die
    }

    /// <summary>
    /// Cài đặt cấu hình thông số cho nhân vật
    /// </summary>
    public void SetupChamp(bool isTeamLeft)
    {
        IsTeamLeft = isTeamLeft;
        IsViewLeft = !IsTeamLeft;
        Anim = this.GetComponent<Animator>();
        //Quét các collider con
        //0 = collider cha -> bỏ qua
        //1 = collider phát hiện đối phương trong tầm đánh
        //2 = collider vùng an toàn
        Collider2D[] collider = GetComponentsInChildren<Collider2D>();
        if (collider[1].gameObject != gameObject)
        {
            ChampDetect = collider[1].gameObject.AddComponent<SoldierTargetDetect>();
            ChampDetect.Initialize(this);
        }

        //Set layer cho champ
        this.gameObject.layer = IsTeamLeft ? (int)GameSettings.LayerSettings.SoldierTeam1 : (int)GameSettings.LayerSettings.SoldierTeam2;

        //Set layer cho vùng detect va chạm
        this.gameObject.transform.GetChild(0).gameObject.layer = IsTeamLeft ? (int)GameSettings.LayerSettings.SoldierDetectTeam1 : (int)GameSettings.LayerSettings.SoldierDetectTeam2;

        //Set khoảng cách phát hiện va chạm của nhân vật
        this.gameObject.transform.GetChild(0).GetComponent<BoxCollider2D>().size = new Vector2(DetectRange, 1);

        //Set hướng nhìn cho nhân vật
        ChangeView(IsViewLeft);

        AnimController(ChampActions.Moving);
    }
    #endregion

    #region Functions

    /// <summary>
    /// Update
    /// </summary>
    private void Update()
    {
        ActionController();

        //Die
        //if (IsAlive)
        {
            HPBarObject.transform.localScale = new Vector3(Math.Abs(DataValues.vHealthCurrent / DataValues.vHealth), HPBarObject.transform.localScale.y, HPBarObject.transform.localScale.z);
            //print(HPBarObject.transform.localScale.x);
            if (DataValues.vHealthCurrent <= 0)
                DataValues.vHealthCurrent = 0;
        }
    }

    /// <summary>
    /// Chờ sự kiện xảy ra
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    private IEnumerator WaitForEvents(int type)
    {
        switch (type)
        {
            case 0://Chờ champ hết máu => chạy hiệu ứng die
                yield return new WaitUntil(() => DataValues.vHealthCurrent <= 0);
                IsDieing = true;
                AnimController(ChampActions.Dieing);
                ThisRigid.constraints = RigidbodyConstraints2D.FreezeAll;
                ThisCollider.enabled = false;
                IsAlive = false;
                break;
            default: break;
        }
    }

    /// <summary>
    /// Hồi máu mỗi giây
    /// </summary>
    /// <returns></returns>
    private IEnumerator RegenHealth()
    {
        yield return new WaitForSeconds(1f);
        if (IsAlive)
        {
            if (DataValues.vHealthCurrent > DataValues.vHealth)
                DataValues.vHealthCurrent = DataValues.vHealth;
            else
                DataValues.vHealthCurrent += DataValues.vHealthRegen;
        }
    }

    /// <summary>
    /// Điều khiển hành động của champ
    /// </summary>
    private void ActionController()
    {
        //if (!IsInRangeDetect && !CurentAction.Equals(ChampActions.Attacking))
        //{
        //    //Anim.SetTrigger("Move");
        //    this.transform.Translate(IsViewLeft ? -MoveSpeed * Time.deltaTime : MoveSpeed * Time.deltaTime, 0, 0);
        //}
        //else
        //{

        //}

        switch (CurentAction)
        {
            case ChampActions.Moving:
                this.transform.Translate(IsViewLeft ? -DataValues.vMoveSpeed * Time.deltaTime : DataValues.vMoveSpeed * Time.deltaTime, 0, 0);
                break;
            default: break;
        }
    }

    /// <summary>
    /// Điều khiển animation của champ
    /// </summary>
    /// <param name="type"></param>
    private void AnimController(ChampActions input)
    {
        CurentAction = input;
        switch (input)
        {
            case ChampActions.Attacking:
                Anim.speed = DataValues.vAtkSpeed;
                Anim.SetTrigger("Atk");
                break;
            case ChampActions.Moving:
                Anim.speed = DataValues.vMoveSpeed;
                Anim.SetTrigger("Move");
                break;
            case ChampActions.Standing:
                Anim.speed = 1f;
                Anim.SetTrigger("Stand");
                break;
            case ChampActions.Skilling:
                Anim.speed = 1f;
                Anim.SetTrigger("Atk0");
                break;
            case ChampActions.Dieing:
                //Anim.Rebind();
                Anim.speed = 1f;
                Anim.SetTrigger("Die");
                break;
            default:
                break;
        }
    }

    /// <summary>
    /// Kết thúc animation và trả về trạng thái cũ (hàm này gọi trong thiết kế anim)
    /// </summary>
    public void EndAnim()
    {
        if (IsAlive)
        {
            IsViewLeft = IsEnemyInLeft;
            ChangeView(IsViewLeft);//Set hướng nhìn cho nhân vật
            if (IsInRangeDetect)
            {
                AnimController(ChampActions.Attacking);
            }
            else
            {
                IsViewLeft = !IsTeamLeft;
                ChangeView(IsViewLeft);//Set hướng nhìn cho nhân vật
                AnimController(ChampActions.Moving);
            }
        }
        else
        {
            if (!IsDieing)
            {
                AnimController(ChampActions.Dieing);
                ThisRigid.constraints = RigidbodyConstraints2D.FreezeAll;
                ThisCollider.enabled = false;
                IsAlive = false;
            }
        }
    }

    /// <summary>
    /// Chờ thực hiện
    /// </summary>
    /// <returns></returns>
    private IEnumerator WaitForAction(int type, float delayTime)
    {
        switch (type)
        {
            case 0://Hit and run
                yield return new WaitForSeconds(delayTime);
                if (IsAlive)
                {
                    //print(DataValues.vHealthCurrent);
                    IsViewLeft = IsEnemyInLeft;
                    ChangeView(IsViewLeft);//Set hướng nhìn cho nhân vật
                    if (IsInRangeDetect)
                        AnimController(ChampActions.Attacking);
                    else
                        AnimController(ChampActions.Moving);
                }
                else break;
                break;
        }
    }

    /// <summary>
    /// Nhân vật tạch
    /// </summary>
    public void Die()
    {
        //gameObject.SetActive(false);
        gameObject.transform.position = new Vector3(-1000, gameObject.transform.position.y, 0);
        StartCoroutine(Reboot(.5f));
    }

    /// <summary>
    /// Nhân vật sống lại sau khi chết
    /// </summary>
    private IEnumerator Reboot(float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        //gameObject.SetActive(true);
        gameObject.transform.position = new Vector3(IsTeamLeft ? -25 : 220, gameObject.transform.position.y, 0);
        IsAlive = true;
        IsDieing = false;
        ThisCollider.enabled = true;
        ThisRigid.constraints = RigidbodyConstraints2D.None;
        ThisRigid.constraints = RigidbodyConstraints2D.FreezePositionX;
        ThisRigid.constraints = RigidbodyConstraints2D.FreezeRotation;
        DataValues.vHealthCurrent = DataValues.vHealth;
        StartCoroutine(WaitForEvents(0));//Chờ nhân vật die
        //StartCoroutine(RegenHealth());
        ChangeView(!IsTeamLeft);
        AnimController(ChampActions.Moving);
    }

    /// <summary>
    /// Thực hiện ra đòn, đánh thường hoặc skill
    /// 1, 2, 3 = đánh thường
    /// 0 = tung skill
    /// </summary>
    public virtual void ActionSkill(int type)
    {
        switch (type)
        {
            case 0://Skill
                CheckExistAndCreateEffectExtension(new Vector3(this.transform.position.x + (IsViewLeft ? -SkillShowPos.x : SkillShowPos.x), this.transform.position.y + SkillShowPos.y, 0), SkillObject, Quaternion.identity);
                break;
            case 1://Đánh thường 1
                CheckExistAndCreateEffectExtension(new Vector3(this.transform.position.x + (IsViewLeft ? -Atk1ShowPos.x : Atk1ShowPos.x), this.transform.position.y + Atk1ShowPos.y, 0), Atk1Object, Quaternion.identity);
                break;
            case 2:
                CheckExistAndCreateEffectExtension(new Vector3(this.transform.position.x + (IsViewLeft ? -Atk2ShowPos.x : Atk2ShowPos.x), this.transform.position.y + Atk2ShowPos.y, 0), Atk2Object, Quaternion.identity);
                break;
            case 3:
                CheckExistAndCreateEffectExtension(new Vector3(this.transform.position.x + (IsViewLeft ? -Atk3ShowPos.x : Atk3ShowPos.x), this.transform.position.y + Atk3ShowPos.y, 0), Atk3Object, Quaternion.identity);
                break;
        }
    }

    /// <summary>
    /// Thay đổi hướng nhìn của nhân vật
    /// </summary>
    private void ChangeView(bool isViewLeft)
    {
        //if (this.gameObject.layer.Equals((int)GameSettings.LayerSettings.HeroTeam2))
        //    print(IsViewLeft ? "Trai" : "Phai");
        IsViewLeft = isViewLeft;
        this.transform.localScale = new Vector3(IsViewLeft ? 0 - Mathf.Abs(this.transform.localScale.x) : Mathf.Abs(this.transform.localScale.x), this.transform.localScale.y, this.transform.localScale.z);

        HPBarParentObject.transform.localScale = new Vector3(isViewLeft ? 0 - Math.Abs(HPBarParentObject.transform.localScale.x) : Math.Abs(HPBarParentObject.transform.localScale.x), HPBarParentObject.transform.localScale.y, HPBarParentObject.transform.localScale.z);
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        // Do your stuff here
    }

    /// <summary>
    /// Xử lý va chạm
    /// </summary>
    /// <param name="other"></param>
    public void OnTriggerEnter2D(Collider2D other)
    {
        //Phát hiện đối phương trong tầm đánh
        if (IsAlive)
        {
            if ((this.gameObject.layer.Equals((int)GameSettings.LayerSettings.SoldierTeam2) && other.gameObject.layer.Equals((int)GameSettings.LayerSettings.SkillTeam1ToVictim)) || (this.gameObject.layer.Equals((int)GameSettings.LayerSettings.SoldierTeam1) && other.gameObject.layer.Equals((int)GameSettings.LayerSettings.SkillTeam2ToVictim)))
            {
                var victimSkill = other.GetComponent<SkillController>();
                //if (this.gameObject.layer.Equals((int)GameSettings.LayerSettings.HeroTeam2))
                //    print("2 Bi danh");
                //if (this.gameObject.layer.Equals((int)GameSettings.LayerSettings.HeroTeam1))
                //    print("1 Bi danh");

                //Tính toán sát thương gây ra
                var dmg = (int)BattleCore.Damage(victimSkill.DataValues, DataValues, victimSkill.DamagePercent, victimSkill.SkillType);
                //BattleSystem.ShowDmg(dmg, this.transform.position, victimSkill.SkillType);
                DataValues.vHealthCurrent -= dmg;
            }
        }
    }

    /// <summary>
    /// Xử lý ngoài va chạm
    /// </summary>
    /// <param name="other"></param>
    public void OnTriggerExit2D(Collider2D other)
    {

    }

    /// <summary>
    /// Phát hiện đối phương trong tầm đánh
    /// </summary>
    public void InDetectRange(Collider2D other)
    {
        if (IsAlive)
        {
            //Phát hiện đối phương và thực hiện hành động
            if ((other.gameObject.layer.Equals((int)GameSettings.LayerSettings.HeroTeam2) && ChampDetect.IsTeamLeft) || (other.gameObject.layer.Equals((int)GameSettings.LayerSettings.HeroTeam1) && !ChampDetect.IsTeamLeft)
                || (other.gameObject.layer.Equals((int)GameSettings.LayerSettings.SoldierTeam2) && ChampDetect.IsTeamLeft) || (other.gameObject.layer.Equals((int)GameSettings.LayerSettings.SoldierTeam1) && !ChampDetect.IsTeamLeft))
            {
                IsInRangeDetect = true;
                IsViewLeft = IsEnemyInLeft;
                ChangeView(IsViewLeft);//Set hướng nhìn cho nhân vật
                if (!CurentAction.Equals(ChampActions.Attacking))
                    AnimController(ChampActions.Attacking);
            }
        }
    }

    /// <summary>
    /// Ngoài phạm vi phát hiện đối phương
    /// </summary>
    /// <param name="other"></param>
    public void OutDetectRange(Collider2D other)
    {
        if (IsAlive)
        {
            //Ngoài tầm đánh
            if ((other.gameObject.layer.Equals((int)GameSettings.LayerSettings.HeroTeam2) && ChampDetect.IsTeamLeft) || (other.gameObject.layer.Equals((int)GameSettings.LayerSettings.HeroTeam1) && !ChampDetect.IsTeamLeft)
                || (other.gameObject.layer.Equals((int)GameSettings.LayerSettings.SoldierTeam2) && ChampDetect.IsTeamLeft) || (other.gameObject.layer.Equals((int)GameSettings.LayerSettings.SoldierTeam1) && !ChampDetect.IsTeamLeft))
            {
                IsInRangeDetect = false;
                //if(CurentAction.Equals(ChampActions.Attacking))
                //AnimController(ChampActions.Moving);
                //print(this.name);
            }
        }
    }
    #endregion

    #region Core Handle

    /// <summary>
    /// Gọi ở các object kế thừa, enable skill của hero
    /// </summary>
    /// <param name="obj">Skill object</param>
    /// <param name="vec">Tọa độ xuât hiện</param>
    /// <param name="quater">Độ nghiêng, xoay tròn</param>
    public void ShowSkill(GameObject obj, Vector3 vec, Quaternion quater)
    {
        obj.GetComponent<SkillController>().IsViewLeft = IsViewLeft;
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
    /// Sản sinh ra liên tục nhiều object skill
    /// </summary>
    /// <param name="objlist">List object skill</param>
    /// <param name="position">vị trí sinh ra object</param>
    /// <param name="delaytime">khoảng cách time giữa 2 lần sản sinh object</param>
    /// <param name="remaining">số object sẽ được sinh ra</param>
    /// <returns></returns>
    public virtual IEnumerator MultiAtk(List<GameObject> objlist, Vector3 position, float delaytime, int remaining, Quaternion quater)
    {
        int count = 0;
        Begin:
        CheckExistAndCreateEffectExtension(position, objlist, quater);
        count++;
        yield return new WaitForSeconds(delaytime);
        if (count < remaining)
            goto Begin;
    }

    /// <summary>
    /// Check khởi tạo và hiển thị hiệu ứng trúng đòn lên đối phương
    /// </summary>
    /// <param name="col"></param>
    public bool CheckExistAndCreateEffectExtension(Vector3 col, List<GameObject> gobject, Quaternion quater)
    {
        var a = GetObjectNonActive(gobject);
        if (a == null)
        {
            gobject.Add(Instantiate(gobject[0], new Vector3(col.x, col.y, col.z), quater));
            gobject[gobject.Count - 1].GetComponent<SkillController>().IsViewLeft = IsViewLeft;
            return true;
        }
        else
            ShowSkill(a, new Vector3(col.x, col.y, col.z), quater);
        return false;
    }
    #endregion
}