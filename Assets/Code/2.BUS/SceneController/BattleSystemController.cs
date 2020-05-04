using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleSystemController : MonoBehaviour
{
    public GameObject ObjectTest;
    // Start is called before the first frame update

    public GameObject[] ChampTeam1;
    public GameObject[] ChampTeam2;
    void Start()
    {
        ChampTeam1 = new GameObject[1];
        ChampTeam2 = new GameObject[1];
        ChampTeam1[0] = Instantiate(Resources.Load<GameObject>("Prefabs/Champs/Champ1"), new Vector3(-10, 0, 0), Quaternion.identity);
        ChampTeam2[0] = Instantiate(Resources.Load<GameObject>("Prefabs/Champs/Champ2"), new Vector3(10, 0, 0), Quaternion.identity);
        ChampTeam1[0].GetComponent<HeroController>().SetupChamp(true);
        ChampTeam2[0].GetComponent<HeroController>().SetupChamp(false);
    }

    private void CreateTeam()
    {

    }
    public void aaaaa()
    {
        ObjectTest.GetComponent<Rigidbody2D>().AddForce(transform.up * 25f, ForceMode2D.Impulse);
    }    
}