using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierTargetDetect : MonoBehaviour
{
    SoldierController _listener;
    public bool IsTeamLeft;
    public void Initialize(SoldierController l)
    {
        _listener = l;
        IsTeamLeft = l.IsTeamLeft;
    }
    //void OnCollisionEnter2D(Collision2D collision)
    //{
    //    _listener.OnCollisionEnter2D(collision);
    //}
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform.position.x <= _listener.transform.position.x)
            _listener.IsEnemyInLeft = true;
        else _listener.IsEnemyInLeft = false;
        _listener.InDetectRange(other);
    }
    void OnTriggerExit2D(Collider2D other)
    {
        _listener.OutDetectRange(other);
    }
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.transform.position.x <= _listener.transform.position.x)
            _listener.IsEnemyInLeft = true;
        else _listener.IsEnemyInLeft = false;
        _listener.IsInRangeDetect = true;
    }
}
