using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoor : Command
{

    public Transform door;
    public GameObject locks;
    [HideInInspector] public Vector3 PosMove;
    private bool haveKey, unLocked;
    private Vector3 orginPos;

    private AudioManager audioManager;
   
    private void Awake() 
    {
        audioManager = AudioManager.Instance;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

     public override void Execute()
    {
        throw new System.NotImplementedException();
    }

    public override void Undo()
    {
        throw new System.NotImplementedException();
    }


}
