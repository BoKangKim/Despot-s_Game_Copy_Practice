using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodShop : MonoBehaviour
{
    [Header("음식 정보")]
    [SerializeField] GameObject[] FoodPrefab;
    List<GameObject> foodList;
    GameObject foodBox = null;
    int count = 0;

    private void Awake()
    {
        foodList = new List<GameObject>();
        foodBox = new GameObject("FoodBox");
    }

    private void OnEnable()
    {
        CreateList();
    }

    private void OnDisable()
    {
        DestroyList();
    }

    private void CreateList()
    {
        for(int i = 0; i < FoodPrefab.Length; i++)
        {
            GameObject food = Instantiate(FoodPrefab[i]);
            food.gameObject.transform.position = GameManager.Instance.GetTileMap().GetCellCenterLocal(new Vector3Int(2 + i + count, 1));
            food.gameObject.transform.SetParent(foodBox.transform);
            foodList.Add(food);
            count++;
        }

        count = 0;
    }

    private void DestroyList()
    {
        for (int i = 0; i < foodList.Count; i++)
        {
            if (foodList[i] != null)
            {
                Destroy(foodList[i].gameObject);
            }
        }

        foodList.Clear();
    }
}
