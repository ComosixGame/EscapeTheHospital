using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Zoombie : Enemy
{
    public GameObject eye;
    public GameObject tickMask;
    public GameObject xMask;
    public GameObject doorPos;
    public GameObject effectPoison;
    public GameObject effectCloudPoison;
    private EnemyState state;
    private float nurseIdleSpeed = 5.5f;
    private float nurseIdleAngularSpeed = 120;
    private float nursePatrolSpeed = 6f;
    private float nursePatrolAngularSpeed = 140;
    private float nurseKeySpeed = 7f;
    private float nurseKeyAngularSpeed = 150;
    private float nurseElecSpeed = 4f;
    private float nurseElecAngularSpeed = 100;

    protected override void OnEnable()
    {
        base.OnEnable();
        gameManager.OnDetectedLostkey.AddListener(StatePatrolLostKey);
        gameManager.OnElectricOff.AddListener(StateLostElectric);
        gameManager.onPlayerDetected.AddListener(StatePatrolDetected);
        playerScanner.OnDetectedTarget.AddListener(HandleWhenDetected);
    }

    protected override void Update()
    {
        base.Update();
        //To do
        StateManager();
    }

    protected override void StateManager()
    {
        switch(state)
        {
            case EnemyState.Idle:
                // tickMask.SetActive(false);
                // xMask.SetActive(false);
                agent.speed = nurseIdleSpeed;
                agent.angularSpeed = nurseIdleAngularSpeed;
                Idle();
                break;
            case EnemyState.Patrol:
                agent.speed = nursePatrolSpeed;
                agent.angularSpeed = nursePatrolAngularSpeed;
                Patrol();
                break;
            case EnemyState.PatrolDetected:
                agent.speed = nursePatrolSpeed;
                agent.angularSpeed = nursePatrolAngularSpeed;
                PatrolWhenDetected();
                break;
            case EnemyState.PatrolElectric:
                agent.speed = nurseElecSpeed;
                agent.angularSpeed = nurseElecAngularSpeed;
                PatrolWhenLostElectric();
                break;
            case EnemyState.PatrolKey:
                agent.speed = nurseKeySpeed;
                agent.angularSpeed = nurseKeyAngularSpeed;
                PatrolWhenLostKey();
                break;
            case EnemyState.Win:
                // tickMask.SetActive(true);
                // xMask.SetActive(false);
                break;
            case EnemyState.Lose:
                // tickMask.SetActive(false);
                // xMask.SetActive(true);
                break;
            default:
                break;
        }
    }

    protected override void PatrolWhenDetected()
    {
        //Nothing
    }

   protected override void PatrolWhenLostElectric()
    {
        agent.SetDestination(pos);
        if (agent.remainingDistance <= agent.stoppingDistance)
        {
            state = EnemyState.Idle;
        }
    }

    protected override void PatrolWhenLostKey()
    {
        agent.SetDestination(pos);
        if (agent.remainingDistance <= agent.stoppingDistance)
        {
            state = EnemyState.Idle;
        }
    }

     private void StatePatrolDetected(Vector3 playerDetectedPos)
    {
        pos = playerDetectedPos;
        state = EnemyState.PatrolDetected;
    }

    private void StatePatrolLostKey(Vector3 keyPos)
    {
        pos = keyPos;
        state = EnemyState.PatrolKey;
    }
    private void StateLostElectric(Vector3 Electpos)
    {
        pos = doorPos.transform.position;
        state = EnemyState.PatrolElectric;
    }

    protected override void HandleWhenDetected(List<RaycastHit> hitList)
    {
        pos = playerScanner.DetectSingleTarget(hitList).position;
        base.HandleWhenDetected(hitList);
        effectLose.transform.position = pos;
        Debug.Log(effectLose.transform.position);
        // effectCloudPoison.transform.position = pos;
        // effectPoison.transform.position = pos;
        effectPoison.SetActive(true);
        effectCloudPoison.SetActive(true);
        GameManager.Instance.ChangeZoombie(pos);
        // GameManager.Instance.EndGame(false);
    }

    protected override void OnEndGame(bool end)
    {
        base.OnEndGame(end);
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        gameManager.onPlayerDetected.RemoveListener(StatePatrolDetected);
        gameManager.OnDetectedLostkey.RemoveListener(StatePatrolLostKey);
        playerScanner.OnDetectedTarget.RemoveListener(HandleWhenDetected);
        gameManager.OnElectricOff.RemoveListener(StateLostElectric);
    }
}
