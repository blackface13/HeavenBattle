using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero3 : HeroController
{
    /// <summary>
    /// Khởi tạo thông số cho nhân vật
    /// </summary>
    public override void Awake()
    {
        base.Awake();
    }

    public override void Start()
    {
        base.Start();
    }

    /// <summary>
    /// Khởi tạo atk 3
    /// </summary>
    public override void SetupAtk3()
    {
        if (!string.IsNullOrEmpty(PrefabNameAtk3))
        {
            Atk3Object = new List<GameObject>();
            Atk3Object.Add((GameObject)Instantiate(Resources.Load<GameObject>("Prefabs/Skills/" + PrefabNameAtk3), new Vector3(-1000, -1000, 0), QuaterAtk3));
            Atk3Object[0].transform.GetChild(0).GetComponent<SkillController>().SetupSkill(IsTeamLeft, DataValues);
            Atk3Object[0].transform.GetChild(1).GetComponent<SkillController>().SetupSkill(IsTeamLeft, DataValues);
            Atk3Object[0].SetActive(false);
        }
    }

    #region Functions
    //Thực hiện tung skil, hàm kế thừa
    public override void ActionSkill(int type)
    {
        base.ActionSkill(type);
    }

    /// <summary>
    /// Kế thừa và viết lại hàm này dành riêng cho skill này
    /// </summary>
    /// <param name="obj"></param>
    /// <param name="vec"></param>
    /// <param name="quater"></param>
    public override void ShowSkill(GameObject obj, Vector3 vec, Quaternion quater)
    {
        obj.transform.localScale = IsViewLeft ? new Vector3(-1, 1, 1) : new Vector3(1, 1, 1);
        obj.transform.position = vec;
        obj.transform.rotation = quater;
        obj.SetActive(true);
    }
    #endregion
}
