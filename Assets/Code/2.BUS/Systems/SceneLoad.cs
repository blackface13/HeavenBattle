using System;
using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class SceneLoad : MonoBehaviour
{
    public Camera cam;
    AsyncOperation asyn = null;
    public void Change_scene(string scn_name)
    {
        if (GameSystem.ControlActive)
        {
            try
            {
                ADS.HideBanner();
            }
            catch { }
            DontDestroyOnLoad(GameSystem.MessageCanvas);
            asyn = SceneManager.LoadSceneAsync(scn_name);

        }
    }
}