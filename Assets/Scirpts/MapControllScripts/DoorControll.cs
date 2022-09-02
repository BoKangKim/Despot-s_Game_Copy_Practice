using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorControll : MonoBehaviour
{
    Animator myAnimator;
    bool isStart = false;

    void IsStart(bool isStart)
    {
        this.isStart = isStart;
    }

    private void Awake()
    {
        myAnimator = GetComponent<Animator>();
    }

    private void Start()
    {
        GameManager.Instance.IsStart += IsStart;
    }

    private void OnMouseEnter()
    {
        if (isStart == true)
            return;

        myAnimator.SetBool("isOpening",true);
    }

    private void OnMouseExit()
    {
        if (isStart == true)
            return;

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
