using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolCamera : MonoBehaviour
{
	[SerializeField]
	private Scanner _playerScanner = new Scanner();
	private GameObject _fieldOfView;
	private Transform _player;
	private Vector3 _playerPosition;
	private GameManager _gameManager;
    public float speed = 5;
	public float waitTime = .3f;

	public Transform pathHolder;
	public Transform rootScanner;
    [Range(0, 360)]
    public float detectionAngle;
    public float viewDistance;
	
	private void Awake() 
	{
		_gameManager = GameManager.Instance;
	}

	private void OnEnable() 
	{
		_playerScanner.OnDetectedTarget.AddListener(HandleWhenDetected);

	}

	void Start() 
	{
		_fieldOfView = _playerScanner.CreataFieldOfView(rootScanner, rootScanner.position, detectionAngle, viewDistance);

		Vector3[] waypoints = new Vector3[pathHolder.childCount];
		for (int i = 0; i < waypoints.Length; i++) {
			waypoints [i] = pathHolder.GetChild (i).position;
		}

		StartCoroutine (FollowPath (waypoints));
	}

	void Update() 
	{
		_playerScanner.Scan();
	}


	IEnumerator FollowPath(Vector3[] waypoints) 
	{
		transform.position = waypoints [0];

		int targetWaypointIndex = 1;
		Vector3 targetWaypoint = waypoints [targetWaypointIndex];
		transform.LookAt (targetWaypoint);

		while (true) {
			transform.position = Vector3.MoveTowards (transform.position, targetWaypoint, speed * Time.deltaTime);
			if (transform.position == targetWaypoint) {
				targetWaypointIndex = (targetWaypointIndex + 1) % waypoints.Length;
				targetWaypoint = waypoints [targetWaypointIndex];
				yield return new WaitForSeconds (waitTime);
				// yield return StartCoroutine (TurnToFace (targetWaypoint));
			}
			yield return null;
		}
	}

	// IEnumerator TurnToFace(Vector3 lookTarget) 
	// {
	// 	Vector3 dirToLookTarget = (lookTarget - transform.position).normalized;
	// 	float targetAngle = 90 - Mathf.Atan2 (dirToLookTarget.z, dirToLookTarget.x) * Mathf.Rad2Deg;

	// 	while (Mathf.Abs(Mathf.DeltaAngle(transform.eulerAngles.y, targetAngle)) > 0.05f) {
	// 		float angle = Mathf.MoveTowardsAngle (transform.eulerAngles.y, targetAngle, Time.deltaTime);
	// 		transform.eulerAngles = Vector3.up * angle;
	// 		yield return null;
	// 	}
	// }



	public void HandleWhenDetected(List<RaycastHit> hitList) {
        Transform _player = _playerScanner.DetectSingleTarget(hitList);
        _playerPosition = _player.position;
		GameManager.Instance.PlayerDetected(_player.position);
    }
	private void OnDisable() 
	{
		_playerScanner.OnDetectedTarget.RemoveListener(HandleWhenDetected);
	}

	void OnDrawGizmos() 
	{
		Vector3 startPosition = pathHolder.GetChild (0).position;
		Vector3 previousPosition = startPosition;

		foreach (Transform waypoint in pathHolder) {
			Gizmos.DrawSphere (waypoint.position, .3f);
			Gizmos.DrawLine (previousPosition, waypoint.position);
			previousPosition = waypoint.position;
		}
		Gizmos.DrawLine (previousPosition, startPosition);
	}

}
