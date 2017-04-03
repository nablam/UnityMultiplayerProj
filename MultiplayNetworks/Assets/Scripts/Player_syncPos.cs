using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
public class Player_syncPos : NetworkBehaviour
{
    //syncvar will be sent to all clients when it updates .. magic
    [SyncVar]
    Vector3 syncpos;  

    [SerializeField]
    Transform myTrans;

    [SerializeField]
    float lerpRate=15f;

    void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        TransmitPosition();
        LerpPos();
	}

    void LerpPos() {
        if (!isLocalPlayer) {
            myTrans.position = Vector3.Lerp(myTrans.position, syncpos, Time.deltaTime * lerpRate);
        }
    }

    //tell server about this player pos , then tell all player s about this position, and clients will use the lerp pos 
    //client tels server to run the COmmand 
    //MUST START WITH "Cmd"
    [Command]
    void CmdProvidePositionToServer(Vector3 pos) {
        //this will only run oin server , but can be called from client . then server will execite
        syncpos = pos;   //on server side, there will be a value in syncpos
    }


    [ClientCallback]
    void TransmitPosition() {
        if (isLocalPlayer)
        {
            CmdProvidePositionToServer(myTrans.position);
        }
  
    }

}
