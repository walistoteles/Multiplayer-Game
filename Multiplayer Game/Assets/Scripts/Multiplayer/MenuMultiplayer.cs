using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuMultiplayer : MonoBehaviourPunCallbacks
{

    public GameObject cam;

    void Start()
    {

        PhotonNetwork.ConnectUsingSettings();
        Debug.Log("Connecting");


    }
    public override void OnConnectedToMaster()
    {

        Debug.Log("Conected");
        if (!PhotonNetwork.InLobby)
        {
            PhotonNetwork.JoinLobby();

        }
    }

    public override void OnJoinedLobby()
    {
        Debug.Log("InLobby");
        base.OnJoinedLobby();

        RoomOptions options = new RoomOptions();

        options.MaxPlayers = 10;

        PhotonNetwork.JoinOrCreateRoom("Quest", options, TypedLobby.Default);
     
    }

    public override void OnCreatedRoom()
    {

        Debug.Log("RoomCreated");
        PhotonNetwork.Instantiate("Player", transform.position, transform.rotation);
        cam.active = false;

    }


    public override void OnJoinedRoom()
    {
        Debug.Log("Join Room");
        if (!PhotonNetwork.IsMasterClient)
        {
            PhotonNetwork.Instantiate("Player", transform.position, transform.rotation);

        }

        cam.active = false;
    }

}
