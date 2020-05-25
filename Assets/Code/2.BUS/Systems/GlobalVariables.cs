using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Net;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
public static class GlobalVariables {

    #region Battle realtime
    public static GameObject PopupChampSelectedInBattle;//Object hình ảnh temp của tướng khi tap giữ để kéo thả
    public static GameObject BlackBGUI1InBattle, BlackBGUI2InBattle;//Object hình ảnh temp của tướng khi tap giữ để kéo thả
    public static GameObject ObjectArrowIntroInBattle;
    public static GameObject[] ObjectButton3LaneInBattle;//3 button để chọn lane
    public static Image ImgWaitingHoldChamp;
    public static bool IsMoving;
    public static int SlotChampSelectedInBattle;
    public static int SlotBattleSpeed;
    #endregion
}