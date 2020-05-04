using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroTargetDetect : MonoBehaviour
{
    HeroController _listener;
    public bool IsTeamLeft;
    public void Initialize(HeroController l)
    {
        _listener = l;
        IsTeamLeft = l.IsTeamLeft;
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        _listener.OnCollisionEnter2D(collision);
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        _listener.OnTriggerEnter2D(other);
    }
}
