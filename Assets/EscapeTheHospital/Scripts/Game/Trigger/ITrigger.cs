using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITrigger
{
    void OnTriggerEnter(Collider other);
    void OnTriggerExit(Collider other);
    void OnTriggerStay(Collider other);
}
