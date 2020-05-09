using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroTemplate : HeroController
{
    /// <summary>
    /// Khởi tạo thông số cho nhân vật
    /// </summary>
    private void Awake()
    {
    }

    private void Start()
    {
        if (!string.IsNullOrEmpty(PrefabNameAtk1))
        {
            Atk1Object = new List<GameObject>();
            Atk1Object.Add((GameObject)Instantiate(Resources.Load<GameObject>("Prefabs/Skills/" + PrefabNameAtk1), new Vector3(-1000, -1000, 0), Quaternion.identity));
            //var skill = Atk1Object[0].GetComponent<SkillController>();
            //skill.IsViewLeft = IsViewLeft;
            Atk1Object[0].GetComponent<SkillController>().SetupSkill(IsTeamLeft);
            Atk1Object[0].SetActive(false);
            //Atk1Object[0].gameObject.layer = IsTeamLeft ? (int)GameSettings.LayerSettings.SkillTeam1ToVictim : (int)GameSettings.LayerSettings.SkillTeam2ToVictim;
        }
    }

    #region Functions
    //Thực hiện tung skil, hàm kế thừa
    public override void ActionSkill(int type)
    {
        base.ActionSkill(type);
    }
    #endregion

    // Start is called before the first frame update
    //void Start()
    //{

    //}

    // Update is called once per frame
    //void Update()
    //{

    //}
}
