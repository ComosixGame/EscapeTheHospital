using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Scriptables", menuName = "Scriptable Objects/New Scriptables Info")]
public class ScriptableInfo : ScriptableObject
{
    public string nameObject;
    public int cost;
    public GameObject obj;
}
