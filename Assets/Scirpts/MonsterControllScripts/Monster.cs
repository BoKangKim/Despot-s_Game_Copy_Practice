using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    int myIdx = -1;
    Unit targetUnit = null;
    private void Awake()
    {
        GameManager.Instance.AddMonster(this);
        myIdx = GameManager.Instance.GetMonsterIdx();
    }

    private void OnEnable()
    {
        StartCoroutine(State_MOVE());
    }

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
            if (targetUnit == null)
                targetUnit = GameManager.Instance.FindTargetUnit(this);

            Vector3 dirVector = (targetUnit.transform.position - gameObject.transform.position).normalized;

            gameObject.transform.Translate(dirVector * 3f * Time.deltaTime);


            if (Vector3.Distance(gameObject.transform.position, targetUnit.transform.position) < 1f)
            {
                yield break;
            }

            yield return null;

        }
    }

    //private void OnEnable()
    //{
    //    GameManager.Instance.AddMonster(this);
    //    myIdx = GameManager.Instance.GetMonsterIdx();
    //}
}
