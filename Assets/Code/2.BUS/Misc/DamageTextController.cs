using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DamageTextController : MonoBehaviour
{
    #region Variables
    public int DamageNumber = 0;
    private TextMeshPro Text;
    private Rigidbody2D Rigid;
    private float AutoHideDelayTime = 1.6f;
    #endregion

    #region Initialize
    // Start is called before the first frame update
    void Start()
    {
        Text = this.GetComponent<TextMeshPro>();
        Rigid = this.GetComponent<Rigidbody2D>();
    }
    #endregion

    #region Functions
    private void OnEnable()
    {
        if (Text == null)
            Text = this.GetComponent<TextMeshPro>();
        if (Rigid == null)
            Rigid = this.GetComponent<Rigidbody2D>();
        Text.text = DamageNumber.ToString();
        Rigid.AddForce(transform.up * Random.Range(450f, 500f) * Time.deltaTime, ForceMode2D.Impulse);
        StartCoroutine(AutoHide(AutoHideDelayTime));
    }

    private IEnumerator AutoHide(float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        this.gameObject.SetActive(false);
        transform.position = new Vector3(-1000, -1000, 0);
    }
    #endregion
}
