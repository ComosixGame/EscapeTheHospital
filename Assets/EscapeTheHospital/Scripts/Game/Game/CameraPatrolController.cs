using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPatrolController : MonoBehaviour
{
  public Transform posLookAt;
    public Transform cameraTransform;

    // Update is called once per frame
    void Update()
    {
        cameraTransform.LookAt(posLookAt, Vector3.left);
    }
}
