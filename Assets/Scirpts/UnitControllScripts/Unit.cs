using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    enum SCENE_STATE
    {
        ASSIGN,
        BATTLE,
        MAX
    }

    SCENE_STATE sceneState;
    public bool IsNewBie { get; set; } = true;
    Vector3 startPos;
    bool isSelected = false;
    Color startColor;
    SpriteRenderer RenColor;
    bool isStart = false;
    Vector3 target = Vector3.zero;
    Monster targetMonster = null;
    Animator ani;
    int myIdx = -1;

    [Header("À¯´Ö ½ºÅÈ")]
    float moveSpeed = 3f;
    [SerializeField] float attackRange = 1f;
    void IsStart(bool isStart)
    {
        sceneState = SCENE_STATE.BATTLE;
        StartCoroutine(State_MOVE());
        this.isStart = isStart;
    }

    private void Awake()
    {
        sceneState = SCENE_STATE.ASSIGN;
        ani = GetComponent<Animator>();
        RenColor = gameObject.GetComponent<SpriteRenderer>();
        startColor = RenColor.color;
        GameManager.Instance.IsStart += IsStart;
        MouseControll.Instance.setClicked += SetClicked;
        MouseControll.Instance.Match += Matched;
    }

    void Start()
    {
        GameManager.Instance.IsStart += IsStart;
        gameObject.transform.position = GameManager.Instance.GetTileMap().GetCellCenterLocal(Vector3Int.FloorToInt(gameObject.transform.position));
        startPos = gameObject.transform.position;
    }

    private void OnEnable()
    {
        GameManager.Instance.AddUnit(this);
        myIdx = GameManager.Instance.GetMyIdx();
    }

    #region À¯´Ö ¹èÄ¡ »óÅÂ
    public void SetClicked(int mouse)
    {
        if (sceneState != SCENE_STATE.ASSIGN)
            return;

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
                isSelected = false;
            }

        }
    }

    public void Matched()
    {
        if (sceneState != SCENE_STATE.ASSIGN)
            return;

        if (MouseControll.Instance.mousePos == GameManager.Instance.GetTileMap().GetCellCenterLocal(Vector3Int.FloorToInt(gameObject.transform.position))
            && IsNewBie == true)
        {
            MouseControll.Instance.MatchUnit = this;
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

    #region Battle State
    enum STATE 
    {
        MOVE,
        ATTACK,
        DEATH,
        MAX
    }

    IEnumerator State_MOVE()
    {
        while (true)
        {
            if (targetMonster == null)
                targetMonster =  GameManager.Instance.FindTargetMonster(this);

            Vector3 dirVector = (targetMonster.transform.position - gameObject.transform.position).normalized;

            ani.SetBool("IsRun",true);
            gameObject.transform.Translate(dirVector * moveSpeed * Time.deltaTime);


            if(Vector3.Distance(gameObject.transform.position, targetMonster.transform.position) < attackRange)
            {
                ani.SetBool("IsRun", false);
                StartCoroutine(State_ATTACK());
                yield break;
            }

            yield return null;

        }
    }

    IEnumerator State_ATTACK()
    {
        ani.SetBool("IsAttack",true);

        while (true)
        {
            yield return null;
        }
    }


    #endregion
}
