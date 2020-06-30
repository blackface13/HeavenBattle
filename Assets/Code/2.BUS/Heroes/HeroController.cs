using Assets.Code._4.CORE;
using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HeroController : MonoBehaviour
{
    #region Variables
    [TitleGroup("Cài đặt nhân vật")]
    [HorizontalGroup("Cài đặt nhân vật/Split", Width = 1f)]
    [TabGroup("Cài đặt nhân vật/Split/Tab1", "Cấu hình thông số")]
    public float ChampID, MoveSpeed, DetectRange, SafeRange, SkillDetectRange;

    [TitleGroup("Cài đặt nhân vật")]
    [HorizontalGroup("Cài đặt nhân vật/Split", Width = 1f)]
    [TabGroup("Cài đặt nhân vật/Split/Tab1", "Cấu hình thông số")]
    public string PrefabNameAtk1, PrefabNameAtk2, PrefabNameAtk3, PrefabNameSkill;//Tên các prefabs

    [TitleGroup("Cài đặt nhân vật")]
    [HorizontalGroup("Cài đặt nhân vật/Split", Width = 1f)]
    [TabGroup("Cài đặt nhân vật/Split/Tab1", "Cấu hình thông số")]
    public bool IsMeleeChamp, IsHaveSkillDetectRange;
    //Là nhân vật cận chiến, Có vùng phát hiện skill riêng

    [TitleGroup("Cài đặt nhân vật")]
    [HorizontalGroup("Cài đặt nhân vật/Split", Width = 1f)]
    [TabGroup("Cài đặt nhân vật/Split/Tab1", "Cấu hình thông số")]
    public Vector2 Atk1ShowPos, Atk2ShowPos, Atk3ShowPos, SkillShowPos, HPBarPos;//Tọa độ các hiệu ứng kỹ năng

    [TitleGroup("Cài đặt nhân vật")]
    [HorizontalGroup("Cài đặt nhân vật/Split", Width = 1f)]
    [TabGroup("Cài đặt nhân vật/Split/Tab1", "Cấu hình thông số")]
    public Quaternion QuaterAtk1, QuaterAtk2, QuaterAtk3, QuaterSkill;//Tọa độ các hiệu ứng kỹ năng

    [TitleGroup("Object cần thiết")]
    public GameObject A;

    public bool IsEnemyInLeft;//Đối phương đang bên trái hoặc phải
    public bool IsViewLeft;//Hướng nhìn trái hoặc phải
    public bool IsInRangeDetect;//Đã phát hiện đối phương trong tầm
    private bool IsInSafeRange;//Đối phươgn trong vùng an toàn của champ
    private bool SkillIsReady;//Skill đã hồi hay chưa
    HeroTargetDetect ChampDetect;//Sự kiện phát hiện trong vùng tấn công
    HeroSafeDetect ChampSafe;//Sự kiện phát hiện trong vùng an toàn
    HeroSkillTargetDetect ChampSkillDetect;//Sự kiện phát hiện đối phương trong vùng có thể dùng skill
    Animator Anim;//Hoạt cảnh nhân vật
    public bool IsTeamLeft;//Team bên trái hoặc bên phải
    private GameObject HPBarObject, HPBarParentObject, SkillCooldownBarObject;//Scale của thanh máu
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
    public TextMeshPro StateText;//Text trạng thái nhận vào (Choáng, câm lặng...)
    public enum ChampActions
    {
        Standing,
        Moving,
        Attacking,
        Hiting,
        Skilling,
        LeavingDangerRange,
        Dieing,
    }
    public ChampActions CurentAction;
    public States ChampState;
    public enum States
    {
        Silent, //Câm lặng
        Stun, //Choáng
        Root, //Giữ chân
        Ice, //Đóng băng
        Static, //Tĩnh, không thể bị chọn làm mục tiêu
        Blind, //Mù
        Slow, //Làm chậm
        Fire,//Thiêu đốt
        Poison,//Trúng độc
    }

    public SortedList<States, bool> ChampStates;//Các hiệu ứng mà nhân vật đang mang
    Coroutine[] RoutineState;//Danh sách các hiệu ứng đang nhận vào
    #endregion

    #region Initialize
    public virtual void Awake()
    {
        CreateState();
        ThisCollider = GetComponent<Collider2D>();
        ThisRigid = GetComponent<Rigidbody2D>();
        BattleSystem = GameObject.Find("Controller").GetComponent<BattleSystemController>();
        HPBarParentObject = Instantiate(Resources.Load<GameObject>("Prefabs/Champs/ChampHPBar"), Vector3.zero, Quaternion.identity);
        HPBarParentObject.transform.SetParent(this.transform, false);
        HPBarParentObject.transform.localPosition = new Vector3(HPBarPos.x, HPBarPos.y, 0);
        HPBarObject = HPBarParentObject.transform.GetChild(2).gameObject;
        SkillCooldownBarObject = HPBarParentObject.transform.GetChild(3).gameObject;
        StateText = HPBarParentObject.transform.GetChild(4).GetComponent<TextMeshPro>();
        Anim = this.GetComponent<Animator>();
        SkillIsReady = true;

        //Quét các collider con
        //0 = collider cha -> bỏ qua
        //1 = collider phát hiện đối phương trong tầm đánh
        //2 = collider vùng an toàn
        Collider2D[] collider = GetComponentsInChildren<Collider2D>();
        if (collider[1].gameObject != gameObject)
        {
            try
            {
                ChampDetect = collider[1].gameObject.GetComponent<HeroTargetDetect>();
                ChampDetect.Initialize(this);
            }
            catch
            {
                ChampDetect = collider[1].gameObject.AddComponent<HeroTargetDetect>();
                ChampDetect.Initialize(this);
            }
        }
        if (collider[2].gameObject != gameObject)
        {
            try
            {
                ChampSafe = collider[2].gameObject.GetComponent<HeroSafeDetect>();
                ChampSafe.Initialize(this);
            }
            catch
            {
                ChampSafe = collider[2].gameObject.AddComponent<HeroSafeDetect>();
                ChampSafe.Initialize(this);
            }
        }
        if (IsHaveSkillDetectRange)
        {
            if (collider[3].gameObject != gameObject && collider[3].gameObject.name.Equals("SkillDetectRange"))
            {
                try
                {
                    ChampSkillDetect = collider[3].gameObject.GetComponent<HeroSkillTargetDetect>();
                    ChampSkillDetect.Initialize(this);
                }
                catch
                {
                    ChampSkillDetect = collider[3].gameObject.AddComponent<HeroSkillTargetDetect>();
                    ChampSkillDetect.Initialize(this);
                }
            }
        }

        //Set khoảng cách phát hiện va chạm của nhân vật
        this.gameObject.transform.GetChild(0).GetComponent<BoxCollider2D>().size = new Vector2(DetectRange, 1);

        //Set khoảng cách vùng an toàn
        this.gameObject.transform.GetChild(1).GetComponent<BoxCollider2D>().size = new Vector2(SafeRange, 1);

        //Set khoảng cách vùng có thể dùng skill
        if (IsHaveSkillDetectRange)
            this.gameObject.transform.GetChild(2).GetComponent<BoxCollider2D>().size = new Vector2(SkillDetectRange, 1);

        AnimController(ChampActions.Moving);
        try
        {
            DataValues = GameSettings.ChampDefault.Find(x => x.ID == (ChampID - 1)).Clone();
            // DataValues.vHealthCurrent = DataValues.vHealth;
        }
        catch
        {
            DataValues = new ChampModel();
        }
    }

    public virtual void Start()
    {
        SetupAtk1();
        SetupAtk2();
        SetupAtk3();
        SetupSkill();
        StartCoroutine(RegenHealth());
    }

    /// <summary>
    /// Khởi tạo atk 1
    /// </summary>
    public virtual void SetupAtk1()
    {
        if (!string.IsNullOrEmpty(PrefabNameAtk1))
        {
            Atk1Object = new List<GameObject>();
            Atk1Object.Add((GameObject)Instantiate(Resources.Load<GameObject>("Prefabs/Skills/" + PrefabNameAtk1), new Vector3(-1000, -1000, 0), QuaterAtk1));
            Atk1Object[0].GetComponent<SkillController>().SetupSkill(IsTeamLeft, DataValues);
            Atk1Object[0].SetActive(false);
        }
    }

    /// <summary>
    /// Khởi tạo atk 2
    /// </summary>
    public virtual void SetupAtk2()
    {
        if (!string.IsNullOrEmpty(PrefabNameAtk2))
        {
            Atk2Object = new List<GameObject>();
            Atk2Object.Add((GameObject)Instantiate(Resources.Load<GameObject>("Prefabs/Skills/" + PrefabNameAtk2), new Vector3(-1000, -1000, 0), QuaterAtk2));
            Atk2Object[0].GetComponent<SkillController>().SetupSkill(IsTeamLeft, DataValues);
            Atk2Object[0].SetActive(false);
        }
    }

    /// <summary>
    /// Khởi tạo atk 3
    /// </summary>
    public virtual void SetupAtk3()
    {
        if (!string.IsNullOrEmpty(PrefabNameAtk3))
        {
            Atk3Object = new List<GameObject>();
            Atk3Object.Add((GameObject)Instantiate(Resources.Load<GameObject>("Prefabs/Skills/" + PrefabNameAtk3), new Vector3(-1000, -1000, 0), QuaterAtk3));
            Atk3Object[0].GetComponent<SkillController>().SetupSkill(IsTeamLeft, DataValues);
            Atk3Object[0].SetActive(false);
        }
    }

    /// <summary>
    /// Khởi tạo skill
    /// </summary>
    public virtual void SetupSkill()
    {
        if (!string.IsNullOrEmpty(PrefabNameSkill))
        {
            SkillObject = new List<GameObject>();
            SkillObject.Add((GameObject)Instantiate(Resources.Load<GameObject>("Prefabs/Skills/" + PrefabNameSkill), new Vector3(-1000, -1000, 0), QuaterSkill));
            SkillObject[0].GetComponent<SkillController>().SetupSkill(IsTeamLeft, DataValues);
            SkillObject[0].SetActive(false);
        }
    }

    public virtual void OnEnable()
    {
        IsAlive = true;
        IsDieing = false;
        ThisCollider.enabled = true;
        ThisRigid.constraints = RigidbodyConstraints2D.None;
        ThisRigid.constraints = RigidbodyConstraints2D.FreezePositionX;
        ThisRigid.constraints = RigidbodyConstraints2D.FreezeRotation;
        DataValues.vHealthCurrent = DataValues.vHealth;
        SkillCooldownBarObject.transform.localScale = new Vector3(2, 2, 2);
        SkillIsReady = true;
        ChangeView(!IsTeamLeft);
        try
        {
            AnimController(ChampActions.Moving);
        }
        catch
        {

        }
        StartCoroutine(WaitForEvents(0));//Chờ nhân vật die
        StartCoroutine(RegenHealth());
    }

    /// <summary>
    /// Cài đặt cấu hình thông số cho nhân vật
    /// </summary>
    public void SetupChamp(bool isTeamLeft)
    {
        IsTeamLeft = isTeamLeft;
        IsViewLeft = !IsTeamLeft;

        //Set layer cho champ
        this.gameObject.layer = IsTeamLeft ? (int)GameSettings.LayerSettings.HeroTeam1 : (int)GameSettings.LayerSettings.HeroTeam2;

        //Set layer cho vùng detect va chạm
        this.gameObject.transform.GetChild(0).gameObject.layer = IsTeamLeft ? (int)GameSettings.LayerSettings.DetectEnemyTeam1 : (int)GameSettings.LayerSettings.DetectEnemyTeam2;

        //Set layer cho vùng an toàn
        this.gameObject.transform.GetChild(1).gameObject.layer = IsTeamLeft ? (int)GameSettings.LayerSettings.SafeRegionTeam1 : (int)GameSettings.LayerSettings.SafeRegionTeam2;

        //Set layer cho vùng detect skill
        if (IsHaveSkillDetectRange)
        {
            this.gameObject.transform.GetChild(2).gameObject.layer = IsTeamLeft ? (int)GameSettings.LayerSettings.SkillDetectEnemyTeam1 : (int)GameSettings.LayerSettings.SkillDetectEnemyTeam2;
            ChampSkillDetect.Initialize(this);
        }

        ChampDetect.Initialize(this);
        ChampSafe.Initialize(this);

        //Set hướng nhìn cho nhân vật
        ChangeView(IsViewLeft);

    }

    /// <summary>
    /// Khởi tạo mảng trạng thái
    /// </summary>
    private void CreateState()
    {
        //Khởi tạo danh sách hiệu ứng
        ChampStates = new SortedList<States, bool>();
        ChampStates.Add(States.Silent, false);
        ChampStates.Add(States.Stun, false);
        ChampStates.Add(States.Root, false);
        ChampStates.Add(States.Ice, false);
        ChampStates.Add(States.Static, false);
        ChampStates.Add(States.Blind, false);
        ChampStates.Add(States.Slow, false);
        ChampStates.Add(States.Fire, false);
        ChampStates.Add(States.Poison, false);

        //Khởi tạo danh sách coroutine đang hoạt động
        RoutineState = new Coroutine[ChampStates.Count];
    }
    #endregion

    #region Functions

    /// <summary>
    /// Update
    /// </summary>
    private void Update()
    {
        ActionController();
        // print(ChampStates[States.Stun]);
        //Die
        //if (IsAlive)
        {
            HPBarObject.transform.localScale = new Vector3(Math.Abs(DataValues.vHealthCurrent / DataValues.vHealth) * 2, HPBarObject.transform.localScale.y, HPBarObject.transform.localScale.z);
            //print(HPBarObject.transform.localScale.x);
            if (DataValues.vHealthCurrent <= 0)
                DataValues.vHealthCurrent = 0;
        }
    }

    /// <summary>
    /// Set trạng thái hiệu ứng cho nhân vật
    /// </summary>
    public void SetStateForChamp(States state, float activeTime)
    {
        if (ChampStates[state])
        {
            StopCoroutine(RoutineState[ChampStates.IndexOfKey(state)]);
            RoutineState[ChampStates.IndexOfKey(state)] = StartCoroutine(SetState(state, activeTime));
        }
        else
        {
            RoutineState[ChampStates.IndexOfKey(state)] = StartCoroutine(SetState(state, activeTime));
        }
    }

    /// <summary>
    /// Set trạng thái hiệu ứng cho nhân vật
    /// </summary>
    private IEnumerator SetState(States state, float activeTime)
    {
        StateText.gameObject.SetActive(true);
        SetStateText(state);
        ChampStates[state] = true;
        yield return new WaitForSeconds(activeTime);
        StateText.text = "";
        ChampStates[state] = false;
        StateText.gameObject.SetActive(false);

        if (IsInRangeDetect)
        {
            AnimController(ChampActions.Attacking);
        }
        else
        {
            ChangeView(!IsTeamLeft);//Set hướng nhìn cho nhân vật
            AnimController(ChampActions.Moving);
        }
    }

    /// <summary>
    /// Set text cho trạng thái
    /// </summary>
    /// <param name="state"></param>
    private void SetStateText(States state)
    {
        switch (state)
        {
            case States.Silent: //Câm lặng
                StateText.text = "Câm lặng";
                break;
            case States.Stun: //Choáng
                StateText.text = "Choáng";
                break;
            case States.Root: //Giữ chân
                StateText.text = "Giữ chân";
                break;
            case States.Ice: //Đóng băng
                StateText.text = "Đóng băng";
                break;
            //case States.Static: //Tĩnh, không thể bị chọn làm mục tiêu
            //    StateText.text = "Choáng";
            //    break;
            case States.Blind: //Mù
                StateText.text = "Mù";
                break;
            case States.Slow: //Làm chậm
                StateText.text = "Làm chậm";
                break;
            case States.Fire://Thiêu đốt
                StateText.text = "Thiêu đốt";
                break;
            case States.Poison://Trúng độc
                StateText.text = "Trúng độc";
                break;
            default:break;
        }
    }

    /// <summary>
    /// Check xem có đc di chuyển hay ko
    /// </summary>
    public bool CheckAllowMove()
    {
        if (!ChampStates[States.Root] && !ChampStates[States.Ice] && !ChampStates[States.Stun])
            return true;
        return false;
    }

    /// <summary>
    /// Check xem có được tung đòn đánh hay ko
    /// </summary>
    public bool CheckAllowAtk()
    {
        if (!ChampStates[States.Ice] && !ChampStates[States.Stun])
            return true;
        return false;
    }

    /// <summary>
    /// Check xem có được dùng skill hay ko
    /// </summary>
    /// <returns></returns>
    public bool CheckAllowSkill()
    {
        if (!ChampStates[States.Silent] && !ChampStates[States.Ice] && !ChampStates[States.Stun])
            return true;
        return false;
    }

    /// <summary>
    /// Trả về true nếu HP > 0
    /// </summary>
    /// <returns></returns>
    public bool CheckHP()
    {
        if (DataValues.vHealthCurrent > 0)
            return true;
        return false;
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
        if (CheckHP())
        {
            switch (CurentAction)
            {
                case ChampActions.Moving:
                    if (CheckAllowMove())
                        this.transform.Translate(IsViewLeft ? -DataValues.vMoveSpeed * Time.deltaTime : DataValues.vMoveSpeed * Time.deltaTime, 0, 0);
                    break;
                default: break;
            }
        }
    }

    /// <summary>
    /// Điều khiển animation của champ
    /// </summary>
    /// <param name="type"></param>
    private void AnimController(ChampActions input)
    {
        switch (input)
        {
            case ChampActions.Attacking:
                if (CheckAllowAtk())
                {
                    Anim.speed = DataValues.vAtkSpeed;
                    Anim.SetTrigger("Atk" + UnityEngine.Random.Range(1, 4).ToString());
                    CurentAction = input;
                }
                break;
            case ChampActions.Moving:
                if (CheckAllowMove())
                {
                    Anim.speed = DataValues.vMoveSpeed;
                    Anim.SetTrigger("Move");
                    CurentAction = input;
                }
                break;
            case ChampActions.Standing:
                Anim.speed = 1f;
                Anim.SetTrigger("Stand");
                break;
            case ChampActions.Skilling:
                if (CheckAllowSkill())
                {
                    SkillCooldownBarObject.transform.localScale = new Vector3(0, 2, 2);
                    Anim.speed = 1f;
                    Anim.SetTrigger("Atk0");
                    SkillIsReady = false;
                    StartCoroutine(GameSystem.ScaleUI(false, SkillCooldownBarObject, new Vector3(2, 2, 2), DataValues.vSkillCooldown));
                    StartCoroutine(WaitForAction(1, DataValues.vSkillCooldown));
                    CurentAction = input;
                }
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
            if (!IsMeleeChamp)//Nếu là nhân vật đánh xa => hit and run
            {
                if (!IsInSafeRange)//Nếu trong vùng an toàn
                {
                    IsViewLeft = IsEnemyInLeft;
                    ChangeView(IsViewLeft);//Set hướng nhìn cho nhân vật
                    if (IsInRangeDetect)
                    {
                        AnimController(SkillIsReady ? ChampActions.Skilling : ChampActions.Attacking);
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
                {
                    IsViewLeft = !IsTeamLeft;
                    ChangeView(IsViewLeft);//Set hướng nhìn cho nhân vật
                    AnimController(ChampActions.Moving);
                }
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
                //yield return new WaitUntil(() => !IsInSafeRange);//Đến dc vùng an toàn => quay lại đánh tiếp
                if (IsAlive)
                {
                    //print(DataValues.vHealthCurrent);
                    IsViewLeft = IsEnemyInLeft;
                    ChangeView(IsViewLeft);//Set hướng nhìn cho nhân vật
                    if (IsInRangeDetect)
                        AnimController(SkillIsReady ? ChampActions.Skilling : ChampActions.Attacking);
                    //AnimController(ChampActions.Attacking);
                    else
                        AnimController(ChampActions.Moving);
                }
                else break;
                break;
            case 1://Chờ hồi chiêu
                yield return new WaitForSeconds(delayTime);
                SkillIsReady = true;
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
        if (!IsTeamLeft)
            StartCoroutine(Reboot(.5f));
        else
        {
            Anim.Rebind();
            gameObject.SetActive(false);
        }
    }

    /// <summary>
    /// Nhân vật sống lại sau khi chết
    /// </summary>
    private IEnumerator Reboot(float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        //gameObject.SetActive(true);
        gameObject.transform.position = new Vector3(IsTeamLeft ? GameSettings.StartPositionXTeam1 : GameSettings.StartPositionXTeam2, gameObject.transform.position.y, 0);
        IsAlive = true;
        IsDieing = false;
        ThisCollider.enabled = true;
        ThisRigid.constraints = RigidbodyConstraints2D.None;
        ThisRigid.constraints = RigidbodyConstraints2D.FreezePositionX;
        ThisRigid.constraints = RigidbodyConstraints2D.FreezeRotation;
        DataValues.vHealthCurrent = DataValues.vHealth;
        IsInSafeRange = false;
        StartCoroutine(WaitForEvents(0));//Chờ nhân vật die
        StartCoroutine(RegenHealth());
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
                CheckExistAndCreateEffectExtension(new Vector3(this.transform.position.x + (IsViewLeft ? -SkillShowPos.x : SkillShowPos.x), this.transform.position.y + SkillShowPos.y, 0), SkillObject, QuaterSkill);
                break;
            case 1://Đánh thường 1
                CheckExistAndCreateEffectExtension(new Vector3(this.transform.position.x + (IsViewLeft ? -Atk1ShowPos.x : Atk1ShowPos.x), this.transform.position.y + Atk1ShowPos.y, 0), Atk1Object, QuaterAtk1);
                break;
            case 2:
                CheckExistAndCreateEffectExtension(new Vector3(this.transform.position.x + (IsViewLeft ? -Atk2ShowPos.x : Atk2ShowPos.x), this.transform.position.y + Atk2ShowPos.y, 0), Atk2Object, QuaterAtk2);
                break;
            case 3:
                CheckExistAndCreateEffectExtension(new Vector3(this.transform.position.x + (IsViewLeft ? -Atk3ShowPos.x : Atk3ShowPos.x), this.transform.position.y + Atk3ShowPos.y, 0), Atk3Object, QuaterAtk3);
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
            if ((this.gameObject.layer.Equals((int)GameSettings.LayerSettings.HeroTeam2) && other.gameObject.layer.Equals((int)GameSettings.LayerSettings.SkillTeam1ToVictim)) || (this.gameObject.layer.Equals((int)GameSettings.LayerSettings.HeroTeam1) && other.gameObject.layer.Equals((int)GameSettings.LayerSettings.SkillTeam2ToVictim)) ||
                (this.gameObject.layer.Equals((int)GameSettings.LayerSettings.HeroTeam2) && other.gameObject.layer.Equals((int)GameSettings.LayerSettings.SkillTeam1ToVictimOnlyChamp)) || (this.gameObject.layer.Equals((int)GameSettings.LayerSettings.HeroTeam1) && other.gameObject.layer.Equals((int)GameSettings.LayerSettings.SkillTeam2ToVictimOnlyChamp)))
            {
                var victimSkill = other.GetComponent<SkillController>();
                //if (this.gameObject.layer.Equals((int)GameSettings.LayerSettings.HeroTeam2))
                //    print("2 Bi danh");
                //if (this.gameObject.layer.Equals((int)GameSettings.LayerSettings.HeroTeam1))
                //    print("1 Bi danh");

                //Tính toán sát thương gây ra
                var dmg = (int)BattleCore.Damage(victimSkill.DataValues, DataValues, victimSkill.DamagePercent, victimSkill.SkillType);
                BattleSystem.ShowDmg(dmg, this.transform.position, victimSkill.SkillType);
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
                || (other.gameObject.layer.Equals((int)GameSettings.LayerSettings.SoldierTeam2) && ChampDetect.IsTeamLeft) || (other.gameObject.layer.Equals((int)GameSettings.LayerSettings.SoldierTeam1) && !ChampDetect.IsTeamLeft)
                || (other.gameObject.layer.Equals((int)GameSettings.LayerSettings.HomeTeam2) && ChampDetect.IsTeamLeft) || (other.gameObject.layer.Equals((int)GameSettings.LayerSettings.HomeTeam1) && !ChampDetect.IsTeamLeft))
            {
                IsInRangeDetect = true;
                IsViewLeft = IsEnemyInLeft;
                ChangeView(IsViewLeft);//Set hướng nhìn cho nhân vật
                if (!CurentAction.Equals(ChampActions.Attacking) && !CurentAction.Equals(ChampActions.Skilling))
                    if (!IsMeleeChamp)//Nếu đánh xa (set tạm)
                        AnimController(SkillIsReady ? ChampActions.Skilling : ChampActions.Attacking);
                    //AnimController(ChampActions.Attacking);
                    else
                        AnimController(ChampActions.Attacking);
            }
        }
    }

    /// <summary>
    /// Phát hiện đối phương trong tầm sử dụng skill (Dành riêng cho 1 số champ nhất định)
    /// </summary>
    public void InSkillDetectRange(Collider2D other)
    {
        if (IsAlive && SkillIsReady)
        {
            //Phát hiện đối phương và thực hiện hành động
            if ((other.gameObject.layer.Equals((int)GameSettings.LayerSettings.HeroTeam2) && ChampDetect.IsTeamLeft) || (other.gameObject.layer.Equals((int)GameSettings.LayerSettings.HeroTeam1) && !ChampDetect.IsTeamLeft))
            {
                //IsInRangeDetect = true;
                IsViewLeft = IsEnemyInLeft;
                ChangeView(IsViewLeft);//Set hướng nhìn cho nhân vật
                if (!CurentAction.Equals(ChampActions.Attacking) && !CurentAction.Equals(ChampActions.Skilling))
                    AnimController(ChampActions.Skilling);
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
                || (other.gameObject.layer.Equals((int)GameSettings.LayerSettings.SoldierTeam2) && ChampDetect.IsTeamLeft) || (other.gameObject.layer.Equals((int)GameSettings.LayerSettings.SoldierTeam1) && !ChampDetect.IsTeamLeft)
                || (other.gameObject.layer.Equals((int)GameSettings.LayerSettings.HomeTeam2) && ChampDetect.IsTeamLeft) || (other.gameObject.layer.Equals((int)GameSettings.LayerSettings.HomeTeam1) && !ChampDetect.IsTeamLeft))
            {
                IsInRangeDetect = false;
                //if(CurentAction.Equals(ChampActions.Attacking))
                //AnimController(ChampActions.Moving);
                //print(this.name);
            }
        }
    }

    /// <summary>
    /// Đối phương nằm trong vùng an toàn của mình (không được an toàn)
    /// </summary>
    /// <param name="other"></param>
    public void InSafeRange(Collider2D other)
    {
        if (IsAlive)
        {
            //Bản thân rời khỏi vùng không an toàn
            if ((other.gameObject.layer.Equals((int)GameSettings.LayerSettings.HeroTeam2) && ChampDetect.IsTeamLeft) || (other.gameObject.layer.Equals((int)GameSettings.LayerSettings.HeroTeam1) && !ChampDetect.IsTeamLeft)
                || (other.gameObject.layer.Equals((int)GameSettings.LayerSettings.SoldierTeam2) && ChampDetect.IsTeamLeft) || (other.gameObject.layer.Equals((int)GameSettings.LayerSettings.SoldierTeam1) && !ChampDetect.IsTeamLeft))
            {
                IsInSafeRange = true;
            }
        }
    }

    /// <summary>
    /// Đã an toàn
    /// </summary>
    /// <param name="other"></param>
    public void OutSafeRange(Collider2D other)
    {
        if (IsAlive)
        {
            //Bản thân rời khỏi vùng không an toàn
            if ((other.gameObject.layer.Equals((int)GameSettings.LayerSettings.HeroTeam2) && ChampDetect.IsTeamLeft) || (other.gameObject.layer.Equals((int)GameSettings.LayerSettings.HeroTeam1) && !ChampDetect.IsTeamLeft)
                || (other.gameObject.layer.Equals((int)GameSettings.LayerSettings.SoldierTeam2) && ChampDetect.IsTeamLeft) || (other.gameObject.layer.Equals((int)GameSettings.LayerSettings.SoldierTeam1) && !ChampDetect.IsTeamLeft))
            {
                IsInSafeRange = false;
                //IsViewLeft = !ChampSafe.IsTeamLeft;
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
    public virtual void ShowSkill(GameObject obj, Vector3 vec, Quaternion quater)
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
    public virtual bool CheckExistAndCreateEffectExtension(Vector3 col, List<GameObject> gobject, Quaternion quater)
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