using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorControll : MonoBehaviour
{
    Animator myAnimator;

    private void Awake()
    {
        myAnimator = GetComponent<Animator>();
    }

    private void OnMouseEnter()
    {
        myAnimator.SetBool("isOpening",true);
    }

    private void OnMouseExit()
    {
        myAnimator.SetBool("isOnMouseDoor",false);
    }

    private void SetAniOpening()
    {
        myAnimator.SetBool("isOpening",false);
        myAnimator.SetBool("isOnMouseDoor", true);
        myAnimator.SetBool("isClosing", true);
    }

    private void SetAniClosing()
    {
        myAnimator.SetBool("isClosing", false);
    }
}
