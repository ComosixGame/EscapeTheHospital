using System.Collections.Generic;
using UnityEngine;

public class Nurse : Enemy
{
    public GameObject questionMask;
    public GameObject exclanmationMask;
    public GameObject lostKeyMask;
    public GameObject lostEleMask;
    public GameObject tickMask;
    public GameObject xMask;
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
        gameManager.onPlayerDetected.AddListener(StatePatrolDetected);
        gameManager.OnDetectedLostkey.AddListener(StatePatrolLostKey);
        playerScanner.OnDetectedTarget.AddListener(HandleWhenDetected);
        gameManager.OnElectricOff.AddListener(StateLostElectric);
    }

    protected override void Update()
    {
        base.Update();
        //To do
        if (!isStart)
        {
            return;
        }
        StateManager();
    }

    protected override void StateManager()
    {
        switch(state)
        {
            case EnemyState.Idle:
                // questionMask.SetActive(false);
                // exclanmationMask.SetActive(false);
                // lostEleMask.SetActive(false);
                // lostKeyMask.SetActive(false);
                // tickMask.SetActive(false);
                // xMask.SetActive(false);
                agent.speed = nurseIdleSpeed;
                agent.angularSpeed = nurseIdleAngularSpeed;
                Idle();
                break;
            case EnemyState.Patrol:
                // questionMask.SetActive(true);
                // exclanmationMask.SetActive(false);
                // lostEleMask.SetActive(false);
                // lostKeyMask.SetActive(false);
                // tickMask.SetActive(false);
                // xMask.SetActive(false);
                agent.speed = nursePatrolSpeed;
                agent.angularSpeed = nursePatrolAngularSpeed;
                Patrol();
                break;
            case EnemyState.PatrolDetected:
                // exclanmationMask.SetActive(true);
                // questionMask.SetActive(false);
                // lostEleMask.SetActive(false);
                // lostKeyMask.SetActive(false);
                // tickMask.SetActive(false);
                // xMask.SetActive(false);
                agent.speed = nursePatrolSpeed;
                agent.angularSpeed = nursePatrolAngularSpeed;
                PatrolWhenDetected();
                break;
            case EnemyState.PatrolElectric:
                // lostEleMask.SetActive(true);
                // exclanmationMask.SetActive(false);
                // questionMask.SetActive(false);
                // lostKeyMask.SetActive(false);
                // tickMask.SetActive(false);
                // xMask.SetActive(false);
                agent.speed = nurseElecSpeed;
                agent.angularSpeed = nurseElecAngularSpeed;
                PatrolWhenLostElectric();
                break;
            case EnemyState.PatrolKey:
                // lostKeyMask.SetActive(true);
                // questionMask.SetActive(false);
                // exclanmationMask.SetActive(false);
                // lostEleMask.SetActive(false);
                // tickMask.SetActive(false);
                // xMask.SetActive(false);
                agent.speed = nurseKeySpeed;
                agent.angularSpeed = nurseKeyAngularSpeed;
                PatrolWhenLostKey();
                break;
            case EnemyState.Win:
                // tickMask.SetActive(true);
                // lostKeyMask.SetActive(false);
                // questionMask.SetActive(false);
                // exclanmationMask.SetActive(false);
                // lostEleMask.SetActive(false);
                // xMask.SetActive(false);
                break;
            case EnemyState.Lose:
                // xMask.SetActive(true);
                // lostKeyMask.SetActive(false);
                // questionMask.SetActive(false);
                // exclanmationMask.SetActive(false);
                // lostEleMask.SetActive(false);
                // tickMask.SetActive(false);
                break;
            default:
                break;
        }
    }


    protected override void Idle()
    {
        base.Idle();
        state = EnemyState.Patrol;
    }

    protected override void PatrolWhenDetected()
    {
        agent.SetDestination(pos);
        if (agent.remainingDistance <= agent.stoppingDistance)
        {
            state = EnemyState.Idle;
        }
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
        pos = Electpos;
        state = EnemyState.PatrolElectric;
    }

    protected override void HandleWhenDetected(List<RaycastHit> hitList)
    {
        pos.x = playerScanner.DetectSingleTarget(hitList).position.x;
        pos.y = 10f;
        pos.z = playerScanner.DetectSingleTarget(hitList).position.z;
        effectLose.transform.position = pos;
        effectLose.SetActive(true);
        GameManager.Instance.EndGame(false);
    }

    protected override void OnEndGame(bool end)
    {
        if (end)
        {
            animator.SetTrigger(loseHash);
        }else
        {
            animator.SetTrigger(winHash);
        }
        base.OnEndGame(end);      
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        gameManager.onPlayerDetected.RemoveListener(StatePatrolDetected);
        gameManager.OnDetectedLostkey.RemoveListener(StatePatrolLostKey);
        gameManager.OnElectricOff.RemoveListener(StateLostElectric);
    }
}
