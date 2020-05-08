using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero1 : HeroController
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
            Atk1Object[0].GetComponent<SkillController>().SetupSkill(IsTeamLeft);
            Atk1Object[0].SetActive(false);
        }
    }

    #region Functions
    //Thực hiện tung skil, hàm kế thừa
    public override void ActionSkill(int type)
    {
        //base.ActionSkill(type);
        switch (type)
        {
            case 0://Skill
                break;
            case 1://Đánh thường 1
                CheckExistAndCreateEffectExtension(new Vector3(this.transform.position.x + (IsViewLeft ? -Atk1ShowPos.x : Atk1ShowPos.x), this.transform.position.y + Atk1ShowPos.y, 0), Atk1Object, Quaternion.identity);
                break;
            case 2:
                break;
            case 3:
                break;
        }
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
