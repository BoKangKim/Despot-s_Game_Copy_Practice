using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitClassControll : MonoBehaviour
{
    // (0,0) -> Center
    // (12,4) -> Right Top
    // (12,-7) -> Left Bottom

    int unitMaxIdx = 0;
    int count = 0;
    List<UnitClass> units = new List<UnitClass>();

    private void Start()
    {
        unitMaxIdx = 4;
        UnitClass obj = null;
        for (int i = 0; i < unitMaxIdx; i++)
        {
            int rndIdx = Random.Range(0,9);
            obj = Instantiate(GameManager.Instance.GetUnitPrefab(rndIdx));
            obj.gameObject.SetActive(true);
            obj.gameObject.transform.position = GameManager.Instance.GetTileMap().GetCellCenterLocal(new Vector3Int(2 + i + count, 1));
            obj.MyIndex = rndIdx;
            units.Add(obj);
            count++;
        }

        obj = Instantiate(GameManager.Instance.GetUnitPrefab(GameManager.Instance.UnitClassCount() - 1));
        obj.MyIndex = unitMaxIdx;
        obj.gameObject.transform.position = GameManager.Instance.GetTileMap().GetCellCenterLocal(new Vector3Int(2 , -1));
        obj.IsNovice = true;
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
