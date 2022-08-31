using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitClass : MonoBehaviour
{
    Vector3 startPos;
    bool isMatched = false;
    public int MyIndex { get; set; } = -1;

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
        }
        else if(isMatched == true 
            && MouseControll.Instance.IsDrag == false)
        {
            gameObject.transform.position = GameManager.Instance.GetTileMap().GetCellCenterLocal(Vector3Int.FloorToInt(gameObject.transform.position));

            if(gameObject.transform.position == MouseControll.Instance.GetUnitPos())
            {
                Destroy(gameObject);
                Destroy(GameManager.Instance.GetIdxCharacter(MouseControll.Instance.UnitPosIdx).gameObject);
                Instantiate(GameManager.Instance.GetCharacterPrefab(MyIndex),MouseControll.Instance.GetUnitPos(),Quaternion.identity);
                MouseControll.Instance.UnitPosIdx = -1;
            }
            else
            {
                gameObject.transform.position = startPos;
            }

            isMatched = false;
        }

        
    }

    //private void OnMouseDown()
    //{
    //    gameObject.transform.position = MouseControll.Instance.mousePos;

    //    isMatched = true;
    //    MouseControll.Instance.IsClicked = false;
    //    MouseControll.Instance.ClickedUnitClass = this;
    //}

    //public void MouseDragEvent()
    //{
    //    gameObject.transform.position = MouseControll.Instance.mousePos - new Vector3(0, 0, MouseControll.Instance.mousePos.z);
    //}

    //public void MouseUpEvent()
    //{
    //    gameObject.transform.position = GameManager.Instance.GetTileMap().GetCellCenterLocal(Vector3Int.FloorToInt(gameObject.transform.position));

    //    if (gameObject.transform.position == MouseControll.Instance.GetUnitPos())
    //    {
    //        Destroy(gameObject);
    //        Destroy(GameManager.Instance.GetIdxCharacter(MouseControll.Instance.UnitPosIdx).gameObject);
    //        Instantiate(GameManager.Instance.GetCharacterPrefab(MyIndex), MouseControll.Instance.GetUnitPos(), Quaternion.identity);
    //        MouseControll.Instance.UnitPosIdx = -1;
    //    }
    //    else
    //    {
    //        gameObject.transform.position = startPos;
    //    }

    //    isMatched = false;
    //}
}
