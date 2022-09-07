using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitClass : MonoBehaviour
{
    Vector3 startPos;
    bool isMatched = false;
    public int MyIndex { get; set; } = -1;
    bool isStart = false;
    public bool IsNovice { get; set; } = true;
    int myCost;


    void IsStart(bool isStart)
    {
        this.isStart = isStart;
    }

    private void Start()
    {
        TextMesh cost = GetComponentInChildren<TextMesh>();
        myCost = int.Parse(cost.text);
        GameManager.Instance.IsStart += IsStart;
        startPos = gameObject.transform.position;
        MouseControll.Instance.IsClicked = false;
        MouseControll.Instance.IsDrag = false;
    }


    void Update()
    {
        if (GameManager.Instance.sceneState != SCENE_STATE.SHOP)
        {
            return;
        }
        if (MouseControll.Instance.IsClicked == false
            && MouseControll.Instance.IsDrag == false
            && isMatched == false)
        {
            return;
        }


        if (MouseControll.Instance.IsClicked == true 
            && MouseControll.Instance.mousePos == gameObject.transform.position)
        {
            gameObject.transform.position = MouseControll.Instance.mousePos;
            isMatched = true;
            MouseControll.Instance.IsClassMatch = true;
            MouseControll.Instance.IsClicked = false;
            MouseControll.Instance.ClickedUnitClass = this;
        }
        else if (isMatched == true 
            && MouseControll.Instance.IsDrag == true 
            )
        {
            gameObject.transform.position = MouseControll.Instance.mousePos - new Vector3(0,0,MouseControll.Instance.mousePos.z);
        }
        else if(isMatched == true 
            && MouseControll.Instance.IsDrag == false)
        {
            gameObject.transform.position = GameManager.Instance.GetTileMap().GetCellCenterLocal(Vector3Int.FloorToInt(gameObject.transform.position));

            if (gameObject.transform.position == MouseControll.Instance.GetUnitPos())
            {
                if (MouseControll.Instance.MatchUnit.IsNewBie == true &&
                    GameManager.Instance.coinCount >= myCost
                    && IsNovice == false)
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
        GameManager.Instance.ChangeCoinText(-myCost);
        MouseControll.Instance.MatchUnit.gameObject.SendMessage("DisConnectDelegate",SendMessageOptions.RequireReceiver) ;
        MouseControll.Instance.MatchUnit.curHP = -1;
        Destroy(MouseControll.Instance.MatchUnit.gameObject);
        MouseControll.Instance.ClickedUnitClass = null;
        MouseControll.Instance.IsClassMatch = false;
        Instantiate(GameManager.Instance.GetCharacterPrefab(MyIndex), MouseControll.Instance.GetUnitPos(), Quaternion.identity,GameManager.Instance.UnitBox.transform);
        MouseControll.Instance.MatchUnit = null;
        Destroy(gameObject);
    }


}
