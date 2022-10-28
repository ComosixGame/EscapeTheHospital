using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvisibleController : MonoBehaviour
{
    private GameObject _player;
    private bool _flag = false;
    public GameObject position;
    public LayerMask layer;
    private void OnTriggerEnter(Collider other) 
    {
        if ((layer & (1 << other.gameObject.layer)) != 0)
        {
            // other.gameObject.GetComponentInChildren
            StartCoroutine(Visible());
            if (_flag)
            {
                Debug.Log("ABC");
                other.gameObject.SetActive(true);
            }
        }
    }

    private IEnumerator Visible()
    {
        position.SetActive(false);
        yield return new WaitForSeconds(5);
        _flag = true;
    }
}
