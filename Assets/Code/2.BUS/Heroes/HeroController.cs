using Assets.Code._4.CORE;
using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroController : MonoBehaviour
{
    #region Variables
    [TitleGroup("Cài đặt nhân vật")]
    [HorizontalGroup("Cài đặt nhân vật/Split", Width = 1f)]
    [TabGroup("Cài đặt nhân vật/Split/Tab1", "Cấu hình thông số")]
    public float ChampID, MoveSpeed, DetectRange, SafeRange;

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
    public Vector2 Atk1ShowPos, Atk2ShowPos, Atk3ShowPos, SkillShowPos;//Tọa độ các hiệu ứng kỹ năng

    [TitleGroup("Object cần thiết")]
    public GameObject A;

    public bool IsEnemyInLeft;//Đối phương đang bên trái hoặc phải
    public bool IsViewLeft;//Hướng nhìn trái hoặc phải
    private bool IsInRangeDetect;//Đã phát hiện đối phương trong tầm
    private bool IsInSafeRange;//Đối phươgn trong vùng an toàn của champ
    HeroTargetDetect ChampDetect;//Sự kiện phát hiện trong vùng tấn công
    HeroSafeDetect ChampSafe;//Sự kiện phát hiện trong vùng an toàn
    Animator Anim;//Hoạt cảnh nhân vật
    public bool IsTeamLeft;//Team bên trái hoặc bên phải

    public List<GameObject> Atk1Object;
    public List<GameObject> Atk2Object;
    public List<GameObject> Atk3Object;
    public List<GameObject> SkillObject;
    private enum ChampActions
    {
        Standing,
        Moving,
        Attacking,
        Hiting,
        Skilling,
        LeavingDangerRange
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

    void Start()
    {
        //RaycastHit2D hit = Physics2D.Raycast(this.transform.position, transform.forward * -10, 3f);
        //if (hit.collider != null)
        //    print("fgfgf");

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
            ChampDetect = collider[1].gameObject.AddComponent<HeroTargetDetect>();
            ChampDetect.Initialize(this);
        }
        if (collider[2].gameObject != gameObject)
        {
            ChampSafe = collider[2].gameObject.AddComponent<HeroSafeDetect>();
            ChampSafe.Initialize(this);
        }

        //Set layer cho champ
        this.gameObject.layer = IsTeamLeft ? (int)GameSettings.LayerSettings.HeroTeam1 : (int)GameSettings.LayerSettings.HeroTeam2;

        //Set layer cho vùng detect va chạm
        this.gameObject.transform.GetChild(0).gameObject.layer = IsTeamLeft ? (int)GameSettings.LayerSettings.DetectEnemyTeam1 : (int)GameSettings.LayerSettings.DetectEnemyTeam2;

        //Set khoảng cách phát hiện va chạm của nhân vật
        this.gameObject.transform.GetChild(0).GetComponent<BoxCollider2D>().size = new Vector2(DetectRange, 1);

        //Set layer cho vùng an toàn
        this.gameObject.transform.GetChild(1).gameObject.layer = IsTeamLeft ? (int)GameSettings.LayerSettings.SafeRegionTeam1 : (int)GameSettings.LayerSettings.SafeRegionTeam2;

        //Set khoảng cách vùng an toàn
        this.gameObject.transform.GetChild(1).GetComponent<BoxCollider2D>().size = new Vector2(SafeRange, 1);

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
        //Debug.DrawRay(transform.position, (Vector2)transform.position - new Vector2(10, 0), Color.red, .1f);

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
                this.transform.Translate(IsViewLeft ? -MoveSpeed * Time.deltaTime : MoveSpeed * Time.deltaTime, 0, 0);
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
                Anim.SetTrigger("Atk" + UnityEngine.Random.Range(1, 4).ToString());
                break;
            case ChampActions.Moving:
                Anim.SetTrigger("Move");
                break;
            case ChampActions.Standing:
                Anim.SetTrigger("Stand");
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
        if (!IsMeleeChamp)//Nếu là nhân vật đánh xa => hit and run
        {
            if (!IsInSafeRange)//Nếu trong vùng an toàn
            {
                IsViewLeft = IsEnemyInLeft;
                ChangeView(IsViewLeft);//Set hướng nhìn cho nhân vật
                if (IsInRangeDetect)
                {
                    AnimController(ChampActions.Attacking);
                }
                else
                    AnimController(ChampActions.Moving);
            }
            else//Ngoài vùng an toàn
            {
                ChangeView(!IsViewLeft);//Set hướng nhìn cho nhân vật
                AnimController(ChampActions.Moving);
                StartCoroutine(WaitForAction(0, .5f));
            }
        }
        else//Cận chiến
        {
            IsViewLeft = IsEnemyInLeft;
            ChangeView(IsViewLeft);//Set hướng nhìn cho nhân vật
            if (IsInRangeDetect)
            {
                AnimController(ChampActions.Attacking);
            }
            else
                AnimController(ChampActions.Moving);
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
                //yield return new WaitUntil(() => !IsInSafeRange);//Đến dc vùng an toàn => quay lại đánh tiếp
                IsViewLeft = !IsViewLeft;
                ChangeView(IsViewLeft);//Set hướng nhìn cho nhân vật
                if (IsInRangeDetect)
                    AnimController(ChampActions.Attacking);
                else
                    AnimController(ChampActions.Moving);
                break;
        }
    }
    /// <summary>
    /// Nhân vật tạch
    /// </summary>
    public void Die()
    {
        gameObject.SetActive(false);
        gameObject.transform.position = new Vector3(-1000, -1000, 0);
    }

    /// <summary>
    /// Thực hiện ra đòn, đánh thường hoặc skill
    /// 1, 2, 3 = đánh thường
    /// 0 = tung skill
    /// </summary>
    public virtual void ActionSkill(int type)
    {

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
        if ((this.gameObject.layer.Equals((int)GameSettings.LayerSettings.HeroTeam2) && other.gameObject.layer.Equals((int)GameSettings.LayerSettings.DetectEnemyTeam1)) || (this.gameObject.layer.Equals((int)GameSettings.LayerSettings.HeroTeam1) && other.gameObject.layer.Equals((int)GameSettings.LayerSettings.DetectEnemyTeam2)))
        {
            //if (this.gameObject.layer.Equals((int)GameSettings.LayerSettings.HeroTeam2))
            //    print("Team B detected");
            //if (this.gameObject.layer.Equals((int)GameSettings.LayerSettings.HeroTeam1))
            //    print("Team A detected");
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
        //Phát hiện đối phương và thực hiện hành động
        if ((other.gameObject.layer.Equals((int)GameSettings.LayerSettings.HeroTeam2) && ChampDetect.IsTeamLeft) || (other.gameObject.layer.Equals((int)GameSettings.LayerSettings.HeroTeam1) && !ChampDetect.IsTeamLeft))
        {
            IsInRangeDetect = true;
            IsViewLeft = IsEnemyInLeft;
            ChangeView(IsViewLeft);//Set hướng nhìn cho nhân vật
            if (!CurentAction.Equals(ChampActions.Attacking))
                AnimController(ChampActions.Attacking);
        }
    }

    /// <summary>
    /// Ngoài phạm vi phát hiện đối phương
    /// </summary>
    /// <param name="other"></param>
    public void OutDetectRange(Collider2D other)
    {
        //Ngoài tầm đánh
        if ((other.gameObject.layer.Equals((int)GameSettings.LayerSettings.HeroTeam2) && ChampDetect.IsTeamLeft) || (other.gameObject.layer.Equals((int)GameSettings.LayerSettings.HeroTeam1) && !ChampDetect.IsTeamLeft))
        {
            IsInRangeDetect = false;
            //if(CurentAction.Equals(ChampActions.Attacking))
            //AnimController(ChampActions.Moving);
            //print(this.name);
        }
    }

    /// <summary>
    /// Đối phương nằm trong vùng an toàn của mình (không được an toàn)
    /// </summary>
    /// <param name="other"></param>
    public void InSafeRange(Collider2D other)
    {
        //Bản thân rời khỏi vùng không an toàn
        if ((other.gameObject.layer.Equals((int)GameSettings.LayerSettings.HeroTeam2) && ChampSafe.IsTeamLeft) || (other.gameObject.layer.Equals((int)GameSettings.LayerSettings.HeroTeam1) && !ChampSafe.IsTeamLeft))
        {
            IsInSafeRange = true;
        }
    }

    /// <summary>
    /// Đã an toàn
    /// </summary>
    /// <param name="other"></param>
    public void OutSafeRange(Collider2D other)
    {
        //Bản thân rời khỏi vùng không an toàn
        if ((other.gameObject.layer.Equals((int)GameSettings.LayerSettings.HeroTeam2) && ChampSafe.IsTeamLeft) || (other.gameObject.layer.Equals((int)GameSettings.LayerSettings.HeroTeam1) && !ChampSafe.IsTeamLeft))
        {
            IsInSafeRange = false;
            //IsViewLeft = !ChampSafe.IsTeamLeft;
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
