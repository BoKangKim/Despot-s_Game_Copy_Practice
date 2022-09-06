using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitClassControll : MonoBehaviour
{
    // (0,0) -> Center
    // (12,4) -> Right Top
    // (12,-7) -> Left Bottom

    int unitMaxIdx = 4;
    int count = 0;
    List<UnitClass> units = new List<UnitClass>();
    
    private void OnEnable()
    {
        CreateList();
    }

    private void OnDisable()
    {
        count = 0;
        DestroyList();
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            count = 0;
            DestroyList();
            CreateList();
            GameManager.Instance.ChangeCoinText(-2);
        }
    }

    private void CreateList()
    {
        UnitClass obj = null;
        for (int i = 0; i < unitMaxIdx; i++)
        {
            int rndIdx = Random.Range(0, 9);
            obj = Instantiate(GameManager.Instance.GetUnitPrefab(rndIdx));
            obj.gameObject.transform.position = GameManager.Instance.GetTileMap().GetCellCenterLocal(new Vector3Int(2 + i + count, 1));
            obj.MyIndex = rndIdx;
            obj.IsNovice = false;
            obj.gameObject.SetActive(true);
            units.Add(obj);
            count++;
        }

        obj = Instantiate(GameManager.Instance.GetUnitPrefab(GameManager.Instance.UnitClassCount() - 1));
        obj.MyIndex = unitMaxIdx;
        obj.gameObject.transform.position = GameManager.Instance.GetTileMap().GetCellCenterLocal(new Vector3Int(2, -1));

        obj.gameObject.SetActive(true);
        units.Add(obj);
        count++;
    }

    private void DestroyList()
    {
        for(int i = 0; i < units.Count; i++)
        {
            if (units[i] != null)
            {
                Destroy(units[i].gameObject);
            }
        }

        units.Clear();
    }
}
