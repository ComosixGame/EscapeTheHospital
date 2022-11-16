using UnityEngine;
using UnityEngine.AI;
using System.Collections.Generic;

[System.Serializable]
public abstract class Enemy : MonoBehaviour
{
    protected float        speed = 3;
    protected float        speedPatrol = 5;
    protected float        angularSpeed = 120;
    protected float        acceleration = 8;
    protected float        idleTime = 2;
    protected float        alertTime = 10;
    protected float        speedRotation = 7;
    protected int          patrolIndex;
    protected float        IdleTime;
    protected Vector3      pos;
    protected EnemyState   enemyState;
    protected EnemyState   preState;
    protected int          velocityHash;
    protected NavMeshAgent agent;
    protected bool         isStart;
    protected bool         isEnd;
    protected GameObject   fieldOfView;
    protected int          loseHash;
    
    protected Animator     animator;
    protected GameManager  gameManager;
    public Vector3         standPos;
    public Vector3[]       patrolList;
    public Transform       rootScanner;
    [Range(0, 360)] 
    public float           detectionAngle;
    public float           viewDistance;
    public EnemyTypePatrol typePatrol;
    public GameObject      effectLose;
    [SerializeField] public Scanner playerScanner = new Scanner();

    protected virtual void Awake() 
    {
        agent        = GetComponent<NavMeshAgent>();
        animator     = GetComponent<Animator>();
        velocityHash = Animator.StringToHash("Velocity");
        loseHash     = Animator.StringToHash("Lose");
        gameManager  = GameManager.Instance;
    }

    protected virtual void OnEnable() 
    {
        gameManager.onStart.AddListener(OnStartGame);
        gameManager.onEndGame.AddListener(OnEndGame);
    }

    // Start is called before the first frame update
    protected virtual void Start()
    {
        fieldOfView = playerScanner.CreataFieldOfView(rootScanner, rootScanner.position, detectionAngle, viewDistance);
    }

    // Update is called once per frame
    protected virtual void Update()
    { 
        playerScanner.Scan(); 
        if (!isStart)
        {
            return;
        }
        HandlAnimation();
    }

    protected virtual void OnStartGame()
    {
        isStart = true;
    }

    protected abstract void StateManager();

    protected virtual void Idle()
    {
        idleTime += Time.deltaTime;
        agent.SetDestination(transform.position);
    }

    protected virtual void Patrol()
    {
        {
            Vector3 patrolPoint = patrolList[patrolIndex];
            switch (typePatrol)
            {   
                case EnemyTypePatrol.StandInPlace:
                    agent.SetDestination(standPos);
                    if (agent.remainingDistance <= agent.stoppingDistance)
                    IdleTime += Time.deltaTime;
                    transform.rotation = LerpRotation(patrolPoint, transform.position, 10f); 
                    {
                        if (IdleTime > idleTime)
                        {
                            patrolIndex++;
                            if (patrolIndex >= patrolList.Length)
                            {
                                patrolIndex = 0;
                            }
                            IdleTime = 0;
                        }  
                    }
                    break;
                case EnemyTypePatrol.MoveAround:
                    if (agent.remainingDistance <= agent.stoppingDistance)
                    {   
                        IdleTime += Time.deltaTime;
                        if (IdleTime > idleTime)
                        {
                            patrolIndex++;
                            if (patrolIndex >= patrolList.Length)
                            {
                                patrolIndex = 0;
                            }
                            agent.SetDestination(patrolPoint);
                            IdleTime = 0;
                        }
                    }
                    break;
                default:
                    break;
            }
        }
    }

    protected abstract void PatrolWhenDetected();

    protected abstract void PatrolWhenLostElectric();

    protected abstract void PatrolWhenLostKey();

    protected virtual void HandlAnimation()
    {
        Vector3 horizontalVelocity = new Vector3(agent.velocity.x, 0, agent.velocity.z);
        float Velocity = horizontalVelocity.magnitude/3;
        if(Velocity > 0) {
            animator.SetFloat(velocityHash, Velocity);
        } else {
            float v = animator.GetFloat(velocityHash);
            v = Mathf.Lerp(v, -0.1f, 20f * Time.deltaTime);
            animator.SetFloat(velocityHash, v);
        }
    }
    protected virtual Quaternion LerpRotation(Vector3 pos1, Vector3 pos2, float speed)
    {
        Vector3 dirLook = pos1 - pos2;
        Quaternion rotLook = Quaternion.LookRotation(dirLook.normalized);
        rotLook.x = 0;
        rotLook.z = 0;
        return Quaternion.Lerp(transform.rotation, rotLook, speed*Time.deltaTime);
    }

    protected virtual void HandleWhenDetected(List<RaycastHit> hitlist)
    {
        // pos = playerScanner.DetectSingleTarget(hitlist).position;
        // GameManager.Instance.EndGame(false);
        // effectLose.SetActive(true);
    }
    protected virtual void OnEndGame(bool end)
    {

    }

    protected virtual void OnDisable() 
    {
        gameManager.onStart.RemoveListener(OnStartGame);
        gameManager.onEndGame.RemoveListener(OnEndGame);
    }
}
