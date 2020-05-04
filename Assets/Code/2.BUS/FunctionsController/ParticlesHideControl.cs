using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticlesHideControl : MonoBehaviour {

    //Dành cho các hiệu ứng cần hide mà không có script điều khiển
    // Use this for initialization
    public float time;//Thời gian cần hide, set interface
    private void OnEnable()
    {
        StartCoroutine(AutoHiden());
    }
    IEnumerator AutoHiden()
    {
        yield return new WaitForSeconds(time);
        Hide();
    }
    private void Hide()
    {
        gameObject.SetActive(false);
        gameObject.transform.position = new Vector3(-1000, -1000, 0);
        //gameObject.transform.localEulerAngles = new Vector3();
    }
}
