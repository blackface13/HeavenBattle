using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroSkillTargetDetect : MonoBehaviour
{
    HeroController _listener;
    public bool IsTeamLeft;
    public void Initialize(HeroController l)
    {
        _listener = l;
        IsTeamLeft = l.IsTeamLeft;
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform.position.x <= _listener.transform.position.x)
            _listener.IsEnemyInLeft = true;
        else _listener.IsEnemyInLeft = false;
        _listener.InSkillDetectRange(other);
    }
    //void OnTriggerExit2D(Collider2D other)
    //{
    //    _listener.OutDetectRange(other);
    //}
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.transform.position.x <= _listener.transform.position.x)
            _listener.IsEnemyInLeft = true;
        else _listener.IsEnemyInLeft = false;
        _listener.InSkillDetectRange(other);
        //_listener.IsInRangeDetect = true;
    }
}
