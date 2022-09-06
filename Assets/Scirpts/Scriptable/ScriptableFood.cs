using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "FoodData", menuName = "ScriptableObjects/FoodData")]
public class ScriptableFood : ScriptableObject
{
    [Header("음식 정보")]
    [SerializeField] int count;
    [SerializeField] int addFoodCount;
    [SerializeField] int cost;

    public int Count
    {
        get
        {
            return count;
        }
    }

    public int AddFoodCount 
    {
        get
        {
            return addFoodCount;
        }
    }

    public int Cost 
    {
        get
        {
            return cost;
        }
    }
}


