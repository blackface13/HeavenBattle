using UnityEngine;
public class Sol2Atk : SkillController
{
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
        HitEffectCustomPos = this.transform.position;
        base.OnTriggerEnter2D(col);
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
