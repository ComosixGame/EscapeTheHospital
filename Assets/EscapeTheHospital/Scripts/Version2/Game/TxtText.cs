using UnityEngine;
using TMPro;

public class TxtText : MonoBehaviour
{
    public LayerMask layer;
    private void OnTriggerEnter(Collider other) 
    {
        if ((layer & (1 << other.gameObject.layer)) != 0)
        {
            GetComponentInChildren<TextMeshPro>().fontSize = 15;
        }
    }
}
