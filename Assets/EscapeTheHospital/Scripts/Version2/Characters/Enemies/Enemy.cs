using UnityEngine;
using UnityEngine.AI;

[System.Serializable]
public class Enemy : Person
{
    public int          patrolIndex { get; protected set;}
    public Vector3      playerPosition { get; protected set;}
    public EnemyState   enemyState { get; protected set;}
    public EnemyState   preState { get; protected set;}
    public int          velocityHash { get; protected set;}
    public NavMeshAgent agent { get; protected set;}
    public float        idleTime { get; protected set;}
    public bool         isStart { get; protected set;}
    public bool         isEnd { get; protected set;}
    public GameObject   fieldOfView { get; set;}
    public Animator     animator { get; protected set;}
    public GameManager  gameManager { get; protected set;}
    public Vector3      standPos;
    public Vector3[]    patrolList;
    public Transform    rootScanner;
    [Range(0, 360)] 
    public float        detectionAngle;
    public float        viewDistance;
    [SerializeField]
    private Scanner playerScanner = new Scanner();

    protected virtual void Awake() 
    {
        agent        = GetComponent<NavMeshAgent>();
        animator     = GetComponent<Animator>();
        velocityHash = Animator.StringToHash("Velocity");
        gameManager  = GameManager.Instance;
    }

    private void OnEnable() 
    {
        gameManager.onStart.AddListener(OnStartGame);
        gameManager.onEndGame.AddListener(OnEndGame);
        gameManager.onPlayerDetected.AddListener(PatrolWhenDetected);
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
    }

    public Enemy()
    {

    }

    protected virtual void OnStartGame()
    {
        isStart = true;
    }

    protected virtual void PatrolWhenDetected(Vector3 pos)
    {

    }

    protected override void HandlAnimation()
    {
        base.HandlAnimation();
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

    protected virtual void OnEndGame(bool end)
    {

    }

    private void OnDisable() 
    {
        gameManager.onStart.RemoveListener(OnStartGame);
        gameManager.onEndGame.RemoveListener(OnEndGame);
        gameManager.onPlayerDetected.RemoveListener(PatrolWhenDetected);
    }
}
