using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitClass : MonoBehaviour
{
    Vector3 startPos;
    bool isMatched = false;

    private void Start()
    {
        startPos = gameObject.transform.position;
    }

    void Update()
    {
        if(MouseControll.Instance.IsClicked == false
            && MouseControll.Instance.IsDrag == false
            && isMatched == false)
        {
            return;
        }    

        if(MouseControll.Instance.IsClicked == true 
            && MouseControll.Instance.mousePos == gameObject.transform.position)
        {
            gameObject.transform.position = MouseControll.Instance.mousePos;

            isMatched = true;
            MouseControll.Instance.IsClicked = false;
        }
        else if (isMatched == true 
            && MouseControll.Instance.IsDrag == true 
            && MouseControll.Instance.IsClicked == false)
        {
            gameObject.transform.position = MouseControll.Instance.mousePos - new Vector3(0,0,MouseControll.Instance.mousePos.z);
            Debug.Log(gameObject.transform.position);
        }
        else if(isMatched == true 
            && MouseControll.Instance.IsDrag == false)
        {
            gameObject.transform.position = GameManager.Instance.GetTileMap().GetCellCenterLocal(Vector3Int.FloorToInt(gameObject.transform.position));
            if(gameObject.transform.position.x < -13.5f
                || gameObject.transform.position.y < -6.5f
                || gameObject.transform.position.x > 12.5f
                || gameObject.transform.position.y > 4.5f)
            {
                gameObject.transform.position = startPos;
            }

            isMatched = false;
        }
    }
}
