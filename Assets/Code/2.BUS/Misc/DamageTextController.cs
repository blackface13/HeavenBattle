using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DamageTextController : MonoBehaviour
{
    #region Variables
    public int DamageNumber = 0;
    public TextMeshPro TextMesh;
    private Rigidbody2D Rigid;
    private float AutoHideDelayTime = 1.6f;
    public int DmgType;//Kiểu dmg, 0 = vật lý, 1 = phép thuật
    #endregion

    #region Initialize
    // Start is called before the first frame update
    void Start()
    {
        TextMesh = this.GetComponent<TextMeshPro>();
        Rigid = this.GetComponent<Rigidbody2D>();
        SetColorText();
    }
    #endregion

    #region Functions
    private void OnEnable()
    {
        SetColorText();
        if (TextMesh == null)
            TextMesh = this.GetComponent<TextMeshPro>();
        if (Rigid == null)
            Rigid = this.GetComponent<Rigidbody2D>();
        TextMesh.text = DamageNumber.ToString();
        Rigid.AddForce(transform.up * Random.Range(450f, 500f) * Time.deltaTime, ForceMode2D.Impulse);
        StartCoroutine(AutoHide(AutoHideDelayTime));
    }

    /// <summary>
    /// Thay đổi màu text theo dmg vật lý hoặc phép thuật
    /// </summary>
    private void SetColorText()
    {
        try
        {
            TextMesh.colorGradient = DmgType.Equals(0) ? new VertexGradient(new Color32(255, 231, 0, 255), new Color32(255, 231, 0, 255), new Color32(255, 113, 0, 255), new Color32(255, 113, 0, 255)) : new VertexGradient(new Color32(0, 206, 255, 255), new Color32(0, 206, 255, 255), new Color32(0, 96, 255, 255), new Color32(0, 96, 255, 255));
        }
        catch { }
    }

    private IEnumerator AutoHide(float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        this.gameObject.SetActive(false);
        transform.position = new Vector3(-1000, -1000, 0);
    }
    #endregion
}
