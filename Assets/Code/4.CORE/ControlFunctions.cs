using Assets.Code._0.DTO.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlFunctions : MonoBehaviour
{
    /// <summary>
    /// Hiển thị thông báo lên màn hình
    /// </summary>
    /// <param name="text"></param>
    /// <returns></returns>
    public void ShowMessage(string text)
    {
        try
        {
            StartCoroutine(GameSystem.ShowMessage(text)); //Show tin nhắn từ GameSystem
        }
        catch (Exception e)
        {
            print(e);
        }
    }

    /// <summary>
    /// Đẩy dữ liệu lên server
    /// </summary>
    public void PutRanking()
    {
        //if (!string.IsNullOrEmpty(GameSystem.UserPlayer.UserID))
        //{
        //    //Khởi tạo model ranking
        //    var ranking = new RankingModel()
        //    {
        //        country = GameSystem.UserPlayer.Country,
        //        unoBasicLoseRound = GameSystem.UserPlayer.UnoBasicLoseRound,
        //        unoBasicPoint = GameSystem.UserPlayer.UnoBasicPoint,
        //        unoBasicWinRound = GameSystem.UserPlayer.UnoBasicWinRound,
        //        unoExtensionLoseRound = GameSystem.UserPlayer.UnoExtensionLoseRound,
        //        unoExtensionPoint = GameSystem.UserPlayer.UnoExtensionPoint,
        //        unoExtensionWinRound = GameSystem.UserPlayer.UnoExtensionWinRound,
        //        userID = GameSystem.UserPlayer.UserID,
        //        userName = GameSystem.UserPlayer.UserName,
        //    };
        //StartCoroutine(API.APIPut(API.ServerRankingPut, JsonUtility.ToJson(ranking)));
        //}
    }

    /// <summary>
    /// Khởi chạy âm thanh cho các skill của nhân vật
    /// </summary>
    /// <param name="audioFile">audio clip</param>
    /// <param name="timeDelay">thời gian chờ trước khi chạy âm thanh</param>
    /// <returns></returns>
    //public IEnumerator PlaySound(AudioClip audioFile, float timeDelay)
    //{
    //    if (GameSystem.Settings.SoundEnable)
    //    {
    //        yield return new WaitForSeconds(timeDelay);
    //        GameSystem.Sound.PlayOneShot(audioFile);
    //    }
    //}
}