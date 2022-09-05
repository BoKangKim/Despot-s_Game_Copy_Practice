using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    public int myIdx { get; set; } = -1;
    Unit targetUnit = null;
    Animator ani;
    STATE state;

    [Header("몬스터 정보")]
    [SerializeField] ScriptableMonster MyData;
    public bool Death { get { return curHP <= 0; } }
    float curHP;

    private void Awake()
    {
        ani = GetComponent<Animator>();
        curHP = MyData.MaxHp;
        GameManager.Instance.AddMonster(this);
        myIdx = GameManager.Instance.GetMonsterIdx();
    }

    private void OnEnable()
    {
        state = STATE.IDLE;
        StartCoroutine("State_" + state);
    }

    private void OnDestroy()
    {
        if(Death == true)
            GameManager.Instance.RemoveMonster(myIdx);
    }

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
        StopCoroutine("State_" + state);
        state = Nextstate;
        StartCoroutine("State_" + state);
    }

    IEnumerator State_IDLE()
    {
        if (targetUnit == null)
            targetUnit = GameManager.Instance.FindTargetUnit(this);

        if (targetUnit == null)
        {
            StopAllCoroutines();
        }
        else
            TransferState(STATE.MOVE);

        yield return null;
    }

    IEnumerator State_MOVE()
    {
        while (state == STATE.MOVE)
        {
            Vector3 dirVector = (targetUnit.transform.position - gameObject.transform.position).normalized;
            gameObject.transform.Translate(dirVector * MyData.MoveSpeed * Time.deltaTime);


            if (Vector3.Distance(gameObject.transform.position, targetUnit.transform.position) < MyData.AttackRange)
            {
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
            if(targetUnit.Death == true)
            {
                if (targetUnit == null)
                {
                    TransferState(STATE.IDLE);
                    yield break;
                }
            }
            else if (Vector3.Distance(gameObject.transform.position, targetUnit.transform.position) > MyData.AttackRange)
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
        GameManager.Instance.ChangeCoinText(MyData.Cost);
        ani.SetTrigger("IsDeath");
        yield return null;
    }
    public void AttackUnit()
    {
        if (targetUnit != null)
            targetUnit.SendMessage("TransferHP", MyData.AttackDamage, SendMessageOptions.DontRequireReceiver);
    }

    void TransferHP(float damage)
    {
        if (state == STATE.DEATH)
            return;

        curHP -= damage;

        if(Death == true)
        {
            TransferState(STATE.DEATH);
        }
    }
    
    void MonsterDeath()
    {
        Destroy(this.gameObject);
    }

    #endregion
}
