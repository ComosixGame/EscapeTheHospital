using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum TypePatrol 
{
    STANDINPLACE,
    RELAX,
    MOVEAROUND,
    ENTER,
    WARNING,
    ALL
}
public class EnemyController : MonoBehaviour
{
    private int _patrolIndex = 0;
    private Vector3 _playerPosition;
    private Transform _player;
    private float _speed;
    private int _velocityHash;
    public NavMeshAgent agent;
    public TypePatrol typePatrol;
    public Vector3[] patrolList;


    private void Awake() 
    {
        agent = GetComponent<NavMeshAgent>();
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
        // PatrolState();
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
        }
    }

    private void OnDisable() 
    {
        

    }
}
