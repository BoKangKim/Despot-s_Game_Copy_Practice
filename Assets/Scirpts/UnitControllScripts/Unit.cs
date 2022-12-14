using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void Fire(Monster target);
public class Unit : MonoBehaviour
{
    public bool IsNewBie { get; set; } = false;
    Vector3 startPos;
    bool isSelected = false;
    Color startColor;
    Vector3 MyPos = Vector3.zero;
    SpriteRenderer RenColor;
    bool isStart = false;
    Vector3 target = Vector3.zero;
    Monster targetMonster = null;
    Animator ani;
    public int myIdx { get; set; } = -1;
    public bool Death { get { return curHP <= 0; }}

    public Fire fire = null;

    [Header("???? ????")]
    [SerializeField] ScriptableUnit MyData;
    public float curHP { get; set; }
    STATE state;

    public ScriptableUnit GetUnitData()
    {
        return MyData;
    }
    void IsStart(bool isStart)
    {
        if(isStart == true)
        {
            GameManager.Instance.sceneState = SCENE_STATE.BATTLE;
            state = STATE.IDLE;
            StartCoroutine("State_" + state);
        }
        this.isStart = isStart;
    }

    private void Awake()
    {
        GameManager.Instance.AddUnit(this);
        curHP = MyData.MaxHp;
        ani = GetComponent<Animator>();
        RenColor = gameObject.GetComponent<SpriteRenderer>();
        startColor = RenColor.color;
        GameManager.Instance.IsStart += IsStart;
        MouseControll.Instance.setClicked += SetClicked;
        MouseControll.Instance.Match += Matched;
    }

    void Start()
    {
        myIdx = GameManager.Instance.GetMyIdx();
        MyPos = gameObject.transform.position;
        gameObject.transform.position = GameManager.Instance.GetTileMap().GetCellCenterLocal(Vector3Int.FloorToInt(gameObject.transform.position));
        startPos = gameObject.transform.position;
    }

    private void OnDestroy()
    {
        if(Death == true)
        {
            GameManager.Instance.RemoveUnit(myIdx);
            GameManager.Instance.IsStart -= IsStart;
            MouseControll.Instance.setClicked -= SetClicked;
            MouseControll.Instance.Match -= Matched;
        }
    }

    #region ???? ??ġ ????
    public void SetClicked(int mouse)
    {
        if (GameManager.Instance.sceneState != SCENE_STATE.ASSIGN
            && GameManager.Instance.sceneState != SCENE_STATE.SHOP)
            return;

        if (mouse == 0)
        {
            if(MouseControll.Instance.mousePos == GameManager.Instance.GetTileMap().GetCellCenterLocal(Vector3Int.FloorToInt(gameObject.transform.position)))
            {
                isSelected = true;
                MouseControll.Instance.PreUnit = this;
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

    public void Matched(string name)
    {
        if (GameManager.Instance.sceneState != SCENE_STATE.ASSIGN
            && GameManager.Instance.sceneState != SCENE_STATE.SHOP)
            return;

        if (MouseControll.Instance.mousePos == GameManager.Instance.GetTileMap().GetCellCenterLocal(Vector3Int.FloorToInt(gameObject.transform.position)))
        {
            if(name == "NextUnit")
            {
                MouseControll.Instance.NextUnit = this;
            }
            else if(name == "MatchUnit")
            {
                MouseControll.Instance.MatchUnit = this;
            }
        }
       

    }
    
    public IEnumerator MoveToTarget(Unit unit)
    {
        if (unit == null)
            target = MouseControll.Instance.mousePos;
        else
            target = unit.transform.position;

        Vector3 dirVector = (target - gameObject.transform.position).normalized;
        if(target.x < gameObject.transform.position.x)
        {
            RenColor.flipX = true;
        }

        ani.SetBool("IsRun",true);

        while (true)
        {
            gameObject.transform.Translate(dirVector * Time.deltaTime * MyData.MoveSpeed);
            if(Mathf.Abs(Vector3.Distance(gameObject.transform.position, target)) < 0.1f)
            {
                gameObject.transform.position = GameManager.Instance.GetTileMap().GetCellCenterLocal(Vector3Int.FloorToInt(gameObject.transform.position));
                MouseControll.Instance.IsMove = false;
                ani.SetBool("IsRun", false);
                RenColor.flipX = false;
                MyPos = gameObject.transform.position;
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
        IDLE,
        MOVE,
        ATTACK,
        DEATH,
        MAX
    }

    void TransferState(STATE Nextstate)
    {
        StopAllCoroutines();
        state = Nextstate;
        StartCoroutine("State_" + state);
    }

    IEnumerator MoveToStartPos()
    {
        Vector3 dir = (MyPos - gameObject.transform.position).normalized;

        if (MyPos.x < gameObject.transform.position.x)
        {
            RenColor.flipX = true;
        }
        ani.SetBool("IsRun", true);

        while (true)
        {
            gameObject.transform.Translate(dir * MyData.MoveSpeed * Time.deltaTime);
            if(Vector3.Distance(MyPos,gameObject.transform.position) < 0.1f)
            {
                gameObject.transform.position = GameManager.Instance.GetTileMap().GetCellCenterLocal(Vector3Int.FloorToInt(gameObject.transform.position));
                ani.SetBool("IsRun", false);
                RenColor.flipX = false;
                GameManager.Instance.SendMessage("RequestIsStart", false, SendMessageOptions.DontRequireReceiver);
                yield break;
            }

            yield return null;
        }
    }

    IEnumerator State_IDLE()
    {
        if (targetMonster == null)
            targetMonster = GameManager.Instance.FindTargetMonster(this);

        if (targetMonster == null)
        {
            StopAllCoroutines();
            if(GameManager.Instance.sceneState != SCENE_STATE.SHOP)
                GameManager.Instance.sceneState = SCENE_STATE.SHOP;

            StartCoroutine(MoveToStartPos());
        }
        else
            TransferState(STATE.MOVE);

        yield return null;
    }

    IEnumerator State_MOVE()
    {
        
        ani.SetBool("IsRun",true);

        while (state == STATE.MOVE)
        {
            if (targetMonster.Death == true)
            {
                if (targetMonster == null)
                {
                    TransferState(STATE.IDLE);
                    yield break;
                }
            }

            Vector3 dirVector = (targetMonster.transform.position - gameObject.transform.position).normalized;
            if (targetMonster.transform.position.x < gameObject.transform.position.x)
            {
                RenColor.flipX = true;
            }
            else
            {
                RenColor.flipX = false;
            }
            gameObject.transform.Translate(dirVector * MyData.MoveSpeed * Time.deltaTime);

            if(Vector3.Distance(gameObject.transform.position, targetMonster.transform.position) < MyData.AttackRange)
            {
                ani.SetBool("IsRun", false);
                TransferState(STATE.ATTACK);
                yield break;
            }

            yield return null;

        }
    }

    IEnumerator State_ATTACK()
    {

        while (state == STATE.ATTACK)
        {
            if(targetMonster.Death == true)
            {
                if (targetMonster == null)
                {
                    TransferState(STATE.IDLE);
                    yield break;
                }
            }
            else if (Vector3.Distance(gameObject.transform.position, targetMonster.transform.position) > MyData.AttackRange)
            {
                TransferState(STATE.IDLE);
                yield break;
            }
            else
            {
                ani.SetTrigger("IsAttack");
            }


            yield return new WaitForSeconds(MyData.AttackSpeed);
        }
    }

    IEnumerator State_DEATH()
    {
        DisConnectDelegate();
        ani.SetTrigger("IsDeath");
        yield return null;
    }

    public void AttackMonster()
    {
        if (fire != null)
            fire(targetMonster);
        else
        {
            if (targetMonster != null)
                targetMonster.SendMessage("TransferHP", MyData.AttackDamage, SendMessageOptions.DontRequireReceiver);
        }
    }

    void TransferHP(float damage)
    {
        if (state == STATE.DEATH)
            return;

        curHP -= damage;

        if (Death == true)
        {
            TransferState(STATE.DEATH);
        }
    }

    void UnitDeath()
    {
        Destroy(this.gameObject);
    }

    public Monster GetTargetMonster()
    {
        return targetMonster;
    }


    #endregion
}
