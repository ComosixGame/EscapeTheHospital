using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class Doctor : Enemy
{
    public GameObject lostKeyMask;
    public GameObject lostEleMask;
    public GameObject tickMask;
    public GameObject xMask;
    public UnityEvent OnChangeZoombie = new UnityEvent();
    private EnemyState state;
    private float doctorIdleSpeed = 3.5f;
    private float doctorIdleAngularSpeed = 100;
    private float doctorKeySpeed = 4.5f;
    private float doctorKeyAngularSpeed = 120;
    private float doctorElecSpeed = 2.5f;
    private float doctorElecAngularSpeed = 80;
    private Player player;
    protected override void OnEnable()
    {
        base.OnEnable();
        gameManager.OnDetectedLostkey.AddListener(StatePatrolLostKey);
        gameManager.OnElectricOff.AddListener(StateLostElectric);
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
                // lostKeyMask.SetActive(false);
                // lostEleMask.SetActive(false);
                // tickMask.SetActive(false);
                // xMask.SetActive(false);
                agent.speed = doctorIdleSpeed;
                agent.angularSpeed = doctorIdleAngularSpeed;
                Idle();
                break;
            case EnemyState.Patrol:
                // lostKeyMask.SetActive(false);
                // lostEleMask.SetActive(true);
                // tickMask.SetActive(false);
                // xMask.SetActive(false);
                agent.speed = doctorElecSpeed;
                agent.angularSpeed = doctorElecAngularSpeed;
                break;    
            case EnemyState.PatrolElectric:
                // lostKeyMask.SetActive(false);
                // lostEleMask.SetActive(true);
                // tickMask.SetActive(false);
                // xMask.SetActive(false);
                agent.speed = doctorElecSpeed;
                agent.angularSpeed = doctorElecAngularSpeed;
                PatrolWhenLostElectric();
                break;
            case EnemyState.PatrolKey:
                // lostKeyMask.SetActive(true);
                // lostEleMask.SetActive(false);
                // tickMask.SetActive(false);
                // xMask.SetActive(false);
                agent.speed = doctorKeySpeed;
                agent.angularSpeed = doctorKeyAngularSpeed;
                PatrolWhenLostKey();
                break;
            case EnemyState.Win:
                // lostKeyMask.SetActive(false);
                // lostEleMask.SetActive(false);
                // tickMask.SetActive(true);
                // xMask.SetActive(false);
                break;
            case EnemyState.Lose:
                // lostKeyMask.SetActive(false);
                // lostEleMask.SetActive(false);
                // tickMask.SetActive(false);
                // xMask.SetActive(true);
                break;
            default:
                break;
        }
    }
    protected override void OnStartGame()
    {
        base.OnStartGame();

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

    protected override void HandleWhenDetected(List<RaycastHit> hitlist)
    {
        pos = playerScanner.DetectSingleTarget(hitlist).position;
        // GameManager.Instance.EndGame(false);
    }

    protected override void OnEndGame(bool end)
    {
        base.OnEndGame(end);
    }

    
    protected override void OnDisable()
    {
        base.OnDisable();
        gameManager.OnDetectedLostkey.RemoveListener(StatePatrolLostKey);
        gameManager.OnElectricOff.RemoveListener(StateLostElectric);
        playerScanner.OnDetectedTarget.RemoveListener(HandleWhenDetected);
    }
}
