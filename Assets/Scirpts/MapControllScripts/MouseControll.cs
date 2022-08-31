using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseControll : Singleton<MouseControll>
{
    [Header("마우스 정보")]
    [SerializeField] Texture2D cursor;
    [SerializeField] Camera myCamera;
    public Vector3 mousePos {get; set;} = Vector3.zero;
    public bool IsDrag { get; set; } = false;
    public bool IsClicked { get; set; } = false;
    public bool MatchUnitClass { get; set; } = false;
    int clickCount = 0;

    private void Awake()
    {
        Cursor.SetCursor(cursor, Vector2.zero, CursorMode.ForceSoftware);
    }

    private void OnMouseDown()
    {
        if(IsClicked == false && clickCount == 0)
            IsClicked = true;

        clickCount++;
        TransferMousePos();
    }

    private void OnMouseDrag()
    {
        if (IsClicked == true)
            return;

        if(IsDrag == false)
            IsDrag = true;

        mousePos = Input.mousePosition;
        mousePos = myCamera.ScreenToWorldPoint(mousePos);
    }

    private void OnMouseUp()
    {
        IsClicked = false;
        clickCount = 0;
        IsDrag = false;
    }

    public void TransferMousePos()
    {
        mousePos = Input.mousePosition;
        mousePos = myCamera.ScreenToWorldPoint(mousePos);
        mousePos = GameManager.Instance.GetTileMap().GetCellCenterLocal(Vector3Int.FloorToInt(mousePos));
    }

    public Camera GetCamera()
    {
        return myCamera;
    }
}
