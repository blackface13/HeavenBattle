using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleSystemController : MonoBehaviour
{
    [TitleGroup("Cài đặt hệ thống battle")]
    [HorizontalGroup("Cài đặt hệ thống battle/Split", Width = 1f)]
    [TabGroup("Cài đặt hệ thống battle/Split/Tab1", "Cấu hình thông số")]
    public int NumberObjectDmgTextCreate;


    public GameObject ObjectTest;

    public GameObject[] ChampTeam1;
    public GameObject[] ChampTeam2;

    public List<GameObject> DamageText;
    public List<DamageTextController> DamageTextControl;
    void Start()
    {
        CreateTeam();
        CreateDmgText();
    }

    /// <summary>
    /// Khởi tạo các object dmg text
    /// </summary>
    private void CreateDmgText()
    {
        DamageText = new List<GameObject>();
        DamageTextControl = new List<DamageTextController>();
        for(int i = 0;i< NumberObjectDmgTextCreate; i++)
        {
            DamageText.Add((GameObject)Instantiate(Resources.Load<GameObject>("Prefabs/UI/DamageText"), new Vector3(-1000, -1000, 0), Quaternion.identity));
            DamageTextControl.Add(DamageText[i].GetComponent<DamageTextController>());
            DamageText[i].SetActive(false);
        }
    }

    private void CreateTeam()
    {
        ChampTeam1 = new GameObject[2];
        ChampTeam1[0] = Instantiate(Resources.Load<GameObject>("Prefabs/Champs/Champ1"), new Vector3(-10, 0, 0), Quaternion.identity);
        ChampTeam1[1] = Instantiate(Resources.Load<GameObject>("Prefabs/Champs/Champ2"), new Vector3(-10, 0, 0), Quaternion.identity);
        ChampTeam1[0].GetComponent<HeroController>().SetupChamp(true);
        ChampTeam1[1].GetComponent<HeroController>().SetupChamp(true);


        ChampTeam2 = new GameObject[2];
        ChampTeam2[0] = Instantiate(Resources.Load<GameObject>("Prefabs/Champs/Champ2"), new Vector3(10, 0, 0), Quaternion.identity);
        ChampTeam2[0].GetComponent<HeroController>().SetupChamp(false);
    }
    public void aaaaa()
    {
        ObjectTest.GetComponent<Rigidbody2D>().AddForce(transform.up * 25f, ForceMode2D.Impulse);
    }

    #region Core Handle

    /// <summary>
    /// Gọi ở các object kế thừa, enable skill của hero
    /// </summary>
    /// <param name="obj">Skill object</param>
    /// <param name="vec">Tọa độ xuât hiện</param>
    /// <param name="quater">Độ nghiêng, xoay tròn</param>
    public void ShowDmg(int numberDmg, Vector3 vec)
    {
        vec += new Vector3(0, 2f, 0);
        CheckExistAndCreateEffectExtension(vec, DamageText, numberDmg);
    }

    /// <summary>
    /// Trả về object skill đang ko hoạt động để xuất hiện
    /// </summary>
    /// <param name="obj"></param>
    /// <returns></returns>
    GameObject GetObjectNonActive(List<GameObject> obj, int numberDmg)
    {
        int count = obj.Count;
        for (int i = 0; i < count; i++)
        {
            if (!obj[i].activeSelf)
            {
                DamageTextControl[i].DamageNumber = numberDmg;
                return obj[i];
            }
        }
        return null;
    }

    /// <summary>
    /// Check khởi tạo và hiển thị hiệu ứng trúng đòn lên đối phương
    /// </summary>
    /// <param name="col"></param>
    private void CheckExistAndCreateEffectExtension(Vector3 col, List<GameObject> gobject, int numberDmg)
    {
        var a = GetObjectNonActive(gobject, numberDmg);
        if (a == null)
        {
            gobject.Add(Instantiate(gobject[0], new Vector3(col.x, col.y, col.z), Quaternion.identity));
            DamageTextControl.Add(gobject[gobject.Count - 1].GetComponent<DamageTextController>());
        }
        else
        {
            a.transform.position = col;
            a.SetActive(true);
        }    
    }
    #endregion
}