using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class EnemyController : MonoBehaviour
{

private enum State
{
    IDLE,
    PATROL,
    ENTER,
    WARNING,
    ALL
}
public enum TypePatrol 
{
    STANDINPLACE,
    MOVEAROUND
}
    private int _patrolIndex = 0;
    private Vector3 _playerPosition;
    private Transform _player;
    private float _speed;
    private int _velocityHash;
    private NavMeshAgent _agent;
    private State _state, _preState;
    private float _idleTime;
    private GameObject _FieldOfView;
    [SerializeField]
    private Scanner _playerScanner = new Scanner();
    public Vector3 standPos;
    public TypePatrol typePatrol;
    public Vector3[] patrolList;
    public Transform rootScanner;
    [Range(0, 360)]
    public float detectionAngle;
    public float viewDistance;

    private void Awake() 
    {
        _agent = GetComponent<NavMeshAgent>();
        _velocityHash = Animator.StringToHash("Velocity");

    }

    private void OnEnable() 
    {
        _playerScanner.OnDetectedTarget.AddListener(HandleWhenDetected);
    }
    // Start is called before the first frame update
    void Start()
    {
        _FieldOfView = _playerScanner.CreataFieldOfView(rootScanner, rootScanner.position,detectionAngle,viewDistance);
        GameEventManager.Instance.onDocumentTriggerEnter += StatePatrolEnter;

    }



    // Update is called once per frame
    void Update()
    {
        _playerScanner.Scan();
        StateManager();
    }

    private void StateManager()
    {
        switch(_state)
        {
            case State.IDLE:
                Patrol();
                break;
            case State.ENTER:
                PatrolEnter(_playerPosition);
                break;
            case State.WARNING:
                PatrolWarning();
                break;
        }
    }

    private void Idle()
    {
        _idleTime += Time.deltaTime;
        _agent.SetDestination(transform.position);
    }

    private void Patrol()
    {
        if (patrolList != null && patrolList.Length > 0)
        {
            Vector3 patrolPoint = patrolList[_patrolIndex];
            switch (typePatrol)
            {   
                case TypePatrol.STANDINPLACE:
                    _agent.SetDestination(standPos);
                    if (_agent.remainingDistance <= _agent.stoppingDistance)
                    _idleTime += Time.deltaTime;
                    transform.rotation = LerpRotation(patrolPoint, transform.position, 10f); 
                    {
                        if (_idleTime > 2)
                        {
                            _patrolIndex++;
                            if (_patrolIndex >= patrolList.Length)
                            {
                                _patrolIndex = 0;
                            }
                            _idleTime = 0;
                        }  
                    }
                    break;
                case TypePatrol.MOVEAROUND:
                    if (_agent.remainingDistance <= _agent.stoppingDistance)
                    {   
                        _idleTime += Time.deltaTime;
                        if (_idleTime > 2)
                        {
                            _patrolIndex++;
                            if (_patrolIndex >= patrolList.Length)
                            {
                                _patrolIndex = 0;
                            }
                            _agent.SetDestination(patrolPoint);
                            _idleTime = 0;
                        }
                    }
                    break;
                default:
                    break;
            }
        }
    }

    private void PatrolEnter(Vector3 playPos)
    {
        _agent.SetDestination(playPos);
        // _state = State.PATROL;
     
    }

    private void PatrolWarning()
    {

    }

    private void StatePatrolEnter()
    {
        _state = State.ENTER;
    }

    private Quaternion LerpRotation(Vector3 pos1, Vector3 pos2, float speed)
    {
        Vector3 dirLook = pos1 - pos2;
        Quaternion rotLook = Quaternion.LookRotation(dirLook.normalized);
        rotLook.x = 0;
        rotLook.z = 0;
        return Quaternion.Lerp(transform.rotation, rotLook, speed*Time.deltaTime);
    }

    public void HandleWhenDetected(List<RaycastHit> hitList) {
        _player = _playerScanner.DetectSingleTarget(hitList);
        _playerPosition = _player.position;
        GameManager.Instance.EndGame();
        Debug.Log(123);
    }

    private void OnDisable() 
    {
        _playerScanner.OnDetectedTarget.RemoveListener(HandleWhenDetected);
    }
}

