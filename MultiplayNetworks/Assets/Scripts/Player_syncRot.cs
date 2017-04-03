using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Player_syncRot : NetworkBehaviour
{

    [SyncVar]
    Quaternion _syncPlayerRotation;

    [SyncVar]
    Quaternion _syncCamRotation;


    [SerializeField]
    Transform _playerTrans;

    [SerializeField]
    Transform _camTrans;

    [SerializeField]
    float lerprate=15;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        TransmitRotations();
        LerpRotations();

    }


    void LerpRotations()
    {
        if (!isLocalPlayer)
        {
            _playerTrans.rotation = Quaternion.Lerp(_playerTrans.rotation, _syncPlayerRotation, Time.deltaTime * lerprate);
            _camTrans.rotation = Quaternion.Lerp(_camTrans.rotation, _syncCamRotation, Time.deltaTime * lerprate);
        }
    }

    [Command]
    void CmdProvideRotationsToServer(Quaternion playerRot, Quaternion camRot)
    {
        _syncPlayerRotation = playerRot;
        _syncCamRotation = camRot;
    }


    //call thecommands on client
    //a command can onlu=y be invoked by the owner of the player 
    [Client]
    void TransmitRotations()
    {
        if (isLocalPlayer)
        {
            CmdProvideRotationsToServer(_playerTrans.rotation, _camTrans.rotation);
        }
    }
}
