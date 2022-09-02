using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitClass : MonoBehaviour
{
    Vector3 startPos;
    bool isMatched = false;
    public int MyIndex { get; set; } = -1;
    bool isStart = false;

    
    void IsStart(bool isStart)
    {
        this.isStart = isStart;
    }

    private void Start()
    {
        GameManager.Instance.IsStart += IsStart;
        startPos = gameObject.transform.position;
    }

    void Update()
    {
        if (isStart == true)
            return;

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
            MouseControll.Instance.IsClassMatch = true;
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
                if(MouseControll.Instance.MatchUnit.IsNewBie == true)
                {
                    DestroyAndIntantiate();
                }
                else
                {
                    gameObject.transform.position = startPos;
                }
            }
            else
            {
                gameObject.transform.position = startPos;
            }

            isMatched = false;
        }

        
    }

    public void DestroyAndIntantiate()
    {
        MouseControll.Instance.MatchUnit.gameObject.SendMessage("DisConnectDelegate",SendMessageOptions.RequireReceiver) ;
        Destroy(MouseControll.Instance.MatchUnit.gameObject);
        MouseControll.Instance.ClickedUnitClass = null;
        MouseControll.Instance.IsClassMatch = false;
        Instantiate(GameManager.Instance.GetCharacterPrefab(MyIndex), MouseControll.Instance.GetUnitPos(), Quaternion.identity);
        MouseControll.Instance.MatchUnit = null;
        Destroy(gameObject);
    }


}
