using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectMapController : MonoBehaviour
{
    [TabGroup("Cấu hình thông số")]
    [Title("ID của object")]
    public int ObjectID;

    // Start is called before the first frame update
    void Start()
    {
        var anim = this.GetComponent<Animator>();
        anim.SetTrigger(ObjectID.ToString());
        anim.speed = Random.Range(.3f, 1f);
    }
}