using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBieSpawn : MonoBehaviour
{
    GameObject NewbieBox = null;
    int startNew;
    int cost = 1;
    float countx = 0;
    float county = 0;
    private void Awake()
    {
        startNew = GameManager.Instance.characterPrefab.Length;
        NewbieBox = new GameObject("NewbieBox");
        NewbieBox.transform.position = new Vector3(0f, 0f, 0f);
    }

    private void Start()
    {
        CreateStartUnit();
    }


    // X Max 12.5
    // X Min 0

    // Y Max 4.5
    // Y Min -6.5
    private void CreateStartUnit()
    {
        for(int i = 0; i < startNew; i++)
        {
            Unit unit = Instantiate(GameManager.Instance.characterPrefab[i]);
            unit.gameObject.transform.position = new Vector3(-11.5f + countx, 1.5f - county, 0);
            unit.gameObject.transform.position = GameManager.Instance.GetTileMap().GetCellCenterLocal(Vector3Int.FloorToInt(unit.gameObject.transform.position));
            unit.transform.SetParent(NewbieBox.transform, false);
            if(i == GameManager.Instance.characterPrefab.Length - 1)
                unit.IsNewBie = true;

            county++;
            if (i == 4)
            {
                countx++;
                county = 0;
            }
        }

        countx = 0;
    }

    private void CreateNewBie(Vector3 pos)
    {
        if (GameManager.Instance.coinCount < cost)
            return;

        Unit unit = Instantiate(GameManager.Instance.characterPrefab[GameManager.Instance.characterPrefab.Length - 1]);
        unit.gameObject.transform.position = pos;
        unit.transform.SetParent(NewbieBox.transform, false);
        unit.IsNewBie = true;
        GameManager.Instance.ChangeCoinText(-cost);
    }

}
