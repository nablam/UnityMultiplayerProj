using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;



public class PlayerNetworkSetup : NetworkBehaviour {


    [SerializeField]
    Camera FPScharCam;
    [SerializeField]
    AudioListener audioListner;

    void Start () {
        if (isLocalPlayer) {
            GameObject.Find("Scene Camera").SetActive(false);
            GetComponent<CharacterController>().enabled = true;
            GetComponent<UnityStandardAssets.Characters.FirstPerson.FirstPersonController>().enabled = true;
            FPScharCam.enabled = true;
            audioListner.enabled = true;


        }
	}
	
 
}
