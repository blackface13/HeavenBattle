using System;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Code._2.BUS.Misc
{
    public class UnoCardRankGameEnding : MonoBehaviour
    {
        public GameObject[] ObjectController;
        public Text[] TextUI;

        public void Initialize(string orderRank, string playerName, string moneyBonus)
        {
            TextUI[0].text = orderRank;
            TextUI[1].text = playerName;
            TextUI[2].text = Convert.ToInt32(moneyBonus) > 0 ? "+" + moneyBonus : moneyBonus;
        }
    }
}
