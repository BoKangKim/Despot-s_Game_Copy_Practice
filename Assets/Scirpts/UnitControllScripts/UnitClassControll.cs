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
    List<GameObject> units = new List<GameObject>();

    private void Start()
    {
        unitMaxIdx = 3;

        for (int i = 0; i < unitMaxIdx; i++)
        {
            GameObject obj = Instantiate(GameManager.Instance.GetUnitPrefab(i));

            obj.gameObject.transform.position = GameManager.Instance.GetTileMap().GetCellCenterLocal(new Vector3Int(4 + i + count, 1));
            obj.gameObject.SetActive(true);
            units.Add(obj);
            count++;
        }

    }
}
