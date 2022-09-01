using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    Vector3 startPos;
    public int MyIndex { get; set; } = 0;
    bool isSelected = false;
    Color startColor;
    SpriteRenderer RenColor;
    public bool IsNewBie { get; set; } = true;
    Vector3 target = Vector3.zero;
    Animator ani;

    [Header("À¯´Ö ½ºÅÈ")]
    float moveSpeed = 6f;

    private void Awake()
    {
        ani = GetComponent<Animator>();
        RenColor = gameObject.GetComponent<SpriteRenderer>();
        startColor = RenColor.color;
        MouseControll.Instance.setClicked += SetClicked;
        MouseControll.Instance.Match += Matched;
    }

    void Start()
    {
        gameObject.transform.position = GameManager.Instance.GetTileMap().GetCellCenterLocal(Vector3Int.FloorToInt(gameObject.transform.position));
        startPos = gameObject.transform.position;
    }

    #region À¯´Ö ¹èÄ¡ »óÅÂ
    public void SetClicked(int mouse)
    {
        if (mouse == 0)
        {
            if(MouseControll.Instance.mousePos == GameManager.Instance.GetTileMap().GetCellCenterLocal(Vector3Int.FloorToInt(gameObject.transform.position)))
            {
                isSelected = true;
                MouseControll.Instance.MatchUnit = this;
                RenColor.color = Color.green;
            }
            else
            {
                if (isSelected == true)
                    RenColor.color = startColor;

                MouseControll.Instance.MatchUnit = null;
            }

        }
    }

    public void Matched()
    {

        if (MouseControll.Instance.mousePos == GameManager.Instance.GetTileMap().GetCellCenterLocal(Vector3Int.FloorToInt(gameObject.transform.position))
            && IsNewBie == true)
        {
            MouseControll.Instance.MatchUnit = this;
        }
        else
        {
            MouseControll.Instance.MatchUnit = null;
        }
    }
    
    public IEnumerator MoveToTarget()
    {
        target = MouseControll.Instance.mousePos;
        Vector3 dirVector = (target - gameObject.transform.position).normalized;
        if(target.x < gameObject.transform.position.x)
        {
            RenColor.flipX = true;
        }

        ani.SetBool("IsRun",true);

        while (true)
        {
            gameObject.transform.Translate(dirVector * Time.deltaTime * moveSpeed);
            if(Mathf.Abs(Vector3.Distance(gameObject.transform.position, target)) < 0.1f)
            {
                gameObject.transform.position = GameManager.Instance.GetTileMap().GetCellCenterLocal(Vector3Int.FloorToInt(gameObject.transform.position));
                MouseControll.Instance.IsMove = false;
                ani.SetBool("IsRun", false);
                RenColor.flipX = false;
                yield break;
            }
            yield return null;
        }

    }

    public void DisConnectDelegate()
    {
        MouseControll.Instance.setClicked -= SetClicked;
        MouseControll.Instance.Match -= Matched;
    }

    #endregion
}
