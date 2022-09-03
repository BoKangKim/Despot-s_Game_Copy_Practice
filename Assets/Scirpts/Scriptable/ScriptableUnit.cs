using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "UnitData",menuName = "ScriptableObjects/UnitData")]

public class ScriptableUnit : ScriptableObject
{
    [Header("±‚∫ª Ω∫≈»")]
    public float MoveSpeed;
    public float AttackDamage;
    public float AttackRange;
    public float AttackSpeed;
    public float MaxHp;
    
}
