using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitAnimationEventListener : MonoBehaviour
{
    Unit MyUnit = null;

    private void Awake()
    {
        MyUnit = gameObject.GetComponent<Unit>();
    }

    void Attack()
    {
        MyUnit.AttackMonster();
    }
    
}
