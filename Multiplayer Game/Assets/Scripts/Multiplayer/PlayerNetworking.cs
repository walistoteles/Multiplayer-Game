using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerNetworking : MonoBehaviour
{

    public MonoBehaviour[] scripts;
    public Camera CAM;
    public PhotonView view;
    void Start()
    {

        if (!view.IsMine)
        {
            CAM.enabled = false;

            foreach (var item in scripts)
            {
                item.enabled = false;
            }

        }

    }

 
}
