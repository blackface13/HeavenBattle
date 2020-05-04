using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Code._2.BUS.Misc
{
    public class RankingUI : MonoBehaviour
    {
        public GameObject[] ObjectController;
        public Text[] TextUI;

        public void Initialize(string orderRank, string playerName, string point)
        {
            TextUI[0].text = orderRank;
            TextUI[1].text = playerName;
            TextUI[2].text = point;
        }
    }
}
