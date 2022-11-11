using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Doctor : Enemy
{
    public GameObject lostKeyMask;
    public GameObject lostEleMask;
    public GameObject tickMask;
    public GameObject xMask;
    private EnemyState state;
    protected override void OnEnable()
    {
        base.OnEnable();
    }

    protected override void Update()
    {
        base.Update();
        //To do
        StateManager();
    }

    protected override void OnStartGame()
    {
        base.OnStartGame();
    }

    protected override void StateManager()
    {
        switch(state)
        {
            case EnemyState.Idle:
                Idle();
                break;
            case EnemyState.Patrol:
                Patrol();
                break;
            case EnemyState.PatrolDetected:
                PatrolWhenDetected();
                break;
            case EnemyState.PatrolElectric:
                PatrolWhenLostElectric();
                break;
            case EnemyState.PatrolKey:
                PatrolWhenLostKey();
                break;
        }
    }

    protected override void PatrolWhenDetected()
    {
        state = EnemyState.Patrol;
    }

    protected override void PatrolWhenLostElectric()
    {
        throw new System.NotImplementedException();
    }

    protected override void PatrolWhenLostKey()
    {
        throw new System.NotImplementedException();
    }

    protected override void OnEndGame(bool end)
    {
        base.OnEndGame(end);
    }

    
    protected override void OnDisable()
    {
        base.OnDisable();
    }
}
