using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCamera : MonoBehaviour
{
    [SerializeField] Transform cameraPosition;


    private void Update()
    {
        transform.position = cameraPosition.position;
    }

}
