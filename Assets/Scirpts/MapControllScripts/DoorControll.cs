using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorControll : MonoBehaviour
{
    Animator myAnimator;
    bool isStart = false;
    int dir = -1;


    void IsStart(bool isStart)
    {
        this.isStart = isStart;
    }

    private void Awake()
    {
        myAnimator = GetComponent<Animator>();
        if(gameObject.tag == "Top")
        {
            dir = 0;
        }
        else if (gameObject.tag == "Right")
        {
            dir = 1;
        }
        else if (gameObject.tag == "Bottom")
        {
            dir = 2;
        }
        else if (gameObject.tag == "Left")
        {
            dir = 3;
        }

    }

    private void Start()
    {
        GameManager.Instance.IsStart += IsStart;
    }

    private void OnMouseEnter()
    {
        if (GameManager.Instance.sceneState != SCENE_STATE.SHOP)
            return;

        if (isStart == true)
            return;

        myAnimator.SetBool("isOpening",true);
    }

    private void OnMouseExit()
    {
        if (GameManager.Instance.sceneState != SCENE_STATE.SHOP)
            return;

        if (isStart == true)
            return;

        myAnimator.SetBool("isOnMouseDoor",false);
    }

    private void OnMouseDown()
    {
        if (GameManager.Instance.sceneState != SCENE_STATE.SHOP)
            return;

        GameManager.Instance.dir = dir;
        GameManager.Instance.StartScrollCoroutine();
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
