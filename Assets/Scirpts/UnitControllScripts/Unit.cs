using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    Vector3 startPos;
    public int MyIndex { get; set; } = 0;
    bool isEnter = false;

    void Start()
    {
        gameObject.transform.position = GameManager.Instance.GetTileMap().GetCellCenterLocal(Vector3Int.FloorToInt(gameObject.transform.position));
        startPos = gameObject.transform.position;
        GameManager.Instance.AddPositionList(this);
        MyIndex = GameManager.Instance.GetUnitPosIndex();
    }

    private void OnMouseEnter()
    {
        if (isEnter)
            return;

        isEnter = true;
        MouseControll.Instance.UnitPosIdx = MyIndex;
    }

    private void OnMouseExit()
    {
        isEnter = false;
        MouseControll.Instance.UnitPosIdx = -1;
    }
}
