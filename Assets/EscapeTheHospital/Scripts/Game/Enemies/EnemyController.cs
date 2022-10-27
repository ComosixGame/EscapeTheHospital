using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class EnemyController : MonoBehaviour
{

private enum State
{
    IDLE,
    PATROL
}
public enum TypePatrol 
{
    STANDINPLACE,
    MOVEAROUND,
    ENTER,
    WARNING,
    ALL
}
    private int _patrolIndex = 0;
    private Vector3 _playerPosition;
    private Transform _player;
    private float _speed;
    private int _velocityHash;
    private NavMeshAgent _agent;
    private State _state, _preState;
    private float _idleTime;
    public Vector3 standPos;
    public TypePatrol typePatrol;
    public Vector3[] patrolList;


    private void Awake() 
    {
        _agent = GetComponent<NavMeshAgent>();
        _velocityHash = Animator.StringToHash("Velocity");
    }

    private void OnEnable() 
    {
        
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Patrol();
    }


    private void PatrolState()
    {

    }

    private void Idle()
    {

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
                    {
                        transform.rotation = LerpRotation(patrolPoint, transform.position, 1f);
                        _patrolIndex++;

                        if (_patrolIndex >= patrolList.Length)
                        {
                            _patrolIndex = 0;
                        }
                    }
                break;
                case TypePatrol.MOVEAROUND:
                    if (_agent.remainingDistance <= _agent.stoppingDistance)
                    {   
                        // _idleTime += Time.deltaTime;
                        _patrolIndex++;
                        if (_patrolIndex >= patrolList.Length)
                        {
                            _patrolIndex = 0;
                        }
                        _agent.SetDestination(patrolPoint);
                        _idleTime = 0;
                    }
                break;
            }
        }
    }

    private Quaternion LerpRotation(Vector3 pos1, Vector3 pos2, float speed)
    {
        Vector3 dirLook = pos1 - pos2;
        Quaternion rotLook = Quaternion.LookRotation(dirLook.normalized);
        rotLook.x = 0;
        rotLook.z = 0;
        return Quaternion.Lerp(transform.rotation, rotLook, speed*Time.deltaTime);
    }

    private void OnDisable() 
    {
        

    }
}

