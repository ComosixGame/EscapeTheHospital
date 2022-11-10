using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Nurse : Enemy,IEnemyBehavios
{
    private Enemy enemy = new Enemy();
    public EnemyTypePatrol typePatrol;
    public GameObject questionMask;
    public GameObject exclanmationMask;
    public GameObject lostKeyMask;
    public GameObject lostEleMask;
    public GameObject tickMask;
    public GameObject xMask;
    protected override void Update()
    {
        base.Update();
        //To do
    }

    public void Idle()
    {
        throw new System.NotImplementedException();
    }

    public void Patrol()
    {
        throw new System.NotImplementedException();
    }

    public void PatrolWhenDetected()
    {
        throw new System.NotImplementedException();
    }

    public void PatrolWhenLostElectric()
    {
        throw new System.NotImplementedException();
    }

    public void PatrolWhenLostKey()
    {
        throw new System.NotImplementedException();
    }
}
