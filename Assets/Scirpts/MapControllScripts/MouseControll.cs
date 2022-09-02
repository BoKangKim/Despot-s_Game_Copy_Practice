using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void SetClicked(int mouse);
public delegate void Matched();
public class MouseControll : Singleton<MouseControll>
{
    [Header("마우스 정보")]
    [SerializeField] Texture2D cursor;
    [SerializeField] Camera myCamera;
    public Vector3 mousePos {get; set;} = Vector3.zero;
    public bool IsDrag { get; set; } = false;
    public bool IsClicked { get; set; } = false;
    public UnitClass ClickedUnitClass { get; set; } = null;
    public Unit MatchUnit { get; set; } = null;
    public bool IsClassMatch { get; set; } = false;
    public bool IsMove { get; set; } = false;
    int clickCount = 0;
    public SetClicked setClicked = null;
    public Matched Match = null;
    bool isStart = false;

    void IsStart(bool isStart)
    {
        this.isStart = isStart;
    }

    private void Awake()
    {
        Cursor.SetCursor(cursor, Vector2.zero, CursorMode.ForceSoftware);
    }

    private void Start()
    {
        GameManager.Instance.IsStart += IsStart;
    }

    // X Max -5
    // X Min -11.5


    // Y Max 2.5
    // Y Min -4.5

    private void Update()
    {
        if (isStart == true)
            return;

        if (Input.GetMouseButtonDown(0))
        {
            MatchUnit = null;
            if (IsClicked == false && clickCount == 0)
                IsClicked = true;

            clickCount++;
            TransferMousePos();
            
            if(setClicked != null)
                setClicked(0);
        }
        else if (Input.GetMouseButtonDown(1))
        {
            if (MatchUnit != null && IsMove == false)
            {
                TransferMousePos();
                
                if (mousePos.x <= -5.5f && mousePos.x >= -11.5f
                    && mousePos.y >= -4.5f && mousePos.y <= 2.5f)
                {
                    IsMove = true;
                    MatchUnit.StartCoroutine(MatchUnit.MoveToTarget());
                }
            }

        }
    }


    private void OnMouseDrag()
    {
        if (isStart == true)
            return;

        if (IsClicked == true)
            return;

        if(IsDrag == false)
            IsDrag = true;

        mousePos = Input.mousePosition;
        mousePos = myCamera.ScreenToWorldPoint(mousePos);
    }

    private void OnMouseUp()
    {
        if (isStart == true)
            return;

        IsClicked = false;
        clickCount = 0;
        IsDrag = false;
        
        if(IsClassMatch == true)
        {
            mousePos = GameManager.Instance.GetTileMap().GetCellCenterLocal(Vector3Int.FloorToInt(mousePos));
            if(Match != null)
                Match();
        }

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

    public Vector3 GetUnitPos()
    {
        if(MatchUnit != null)
        {
            
            return MatchUnit.gameObject.transform.position;
        }
        else
        {
            return new Vector3(-1000,0,0);
        }
    }

    private void OnDestroy()
    {
        ClickedUnitClass = null;
        MatchUnit = null;
        setClicked = null;
        Match = null;
    }
}
