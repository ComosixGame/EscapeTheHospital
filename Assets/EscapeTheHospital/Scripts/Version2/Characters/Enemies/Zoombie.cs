using System.Collections.Generic;
using UnityEngine;

public class Zoombie : Enemy
{
    public GameObject eye;
    public GameObject tickMask;
    public GameObject xMask;
    public GameObject doorPos;
    public GameObject poisonCloud;
    public GameObject poison;
    public Scripables scriptablePlayer;
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
        // gameManager.onPlayerDetected.AddListener(StatePatrolDetected);
        playerScanner.OnDetectedTarget.AddListener(HandleWhenDetected);
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
            // case EnemyState.PatrolDetected:
            //     agent.speed = nursePatrolSpeed;
            //     agent.angularSpeed = nursePatrolAngularSpeed;
            //     PatrolWhenDetected();
            //     break;
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

    protected override void Idle()
    {
        base.Idle();
        state = EnemyState.Patrol;
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
            state = EnemyState.Patrol;
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
        base.HandleWhenDetected(hitList);
        pos = playerScanner.DetectSingleTarget(hitList).position;
        playerScanner.DetectSingleTarget(hitList).transform.gameObject.SetActive(false);
        GameObject player =  scriptablePlayer.scriptableObjects[1].obj;
        Instantiate(player, playerScanner.DetectSingleTarget(hitList).transform.position, playerScanner.DetectSingleTarget(hitList).rotation);
        poison.transform.position = pos;
        poisonCloud.transform.position = pos;
        poison.SetActive(true);
        poisonCloud.SetActive(true);
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
        // gameManager.onPlayerDetected.RemoveListener(StatePatrolDetected);
        gameManager.OnDetectedLostkey.RemoveListener(StatePatrolLostKey);
        playerScanner.OnDetectedTarget.RemoveListener(HandleWhenDetected);
        gameManager.OnElectricOff.RemoveListener(StateLostElectric);
    }
}
