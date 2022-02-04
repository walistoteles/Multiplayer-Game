using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IKHeadLookAt : MonoBehaviour
{
    private Animator anim;
    public Transform pos;
    public float height;

    private void Awake()
    {
        anim = GetComponent<Animator>();


    }


    private void OnAnimatorIK(int layerIndex)
    {
        anim.SetLookAtWeight(height);
        anim.SetLookAtPosition(pos.position);
        
    }
}
