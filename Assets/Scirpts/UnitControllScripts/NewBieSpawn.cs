using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBieSpawn : MonoBehaviour
{
    Unit newBiePrefab = null;
    Unit BroadSwordPrefab = null;
    GameObject NewbieBox = null;
    int startNew = 3;

    private void Awake()
    {
        newBiePrefab = GameManager.Instance.GetCharacterPrefab(1);
        BroadSwordPrefab = GameManager.Instance.GetCharacterPrefab(0);
        NewbieBox = new GameObject("NewbieBox");
        NewbieBox.transform.position = new Vector3(0f, 0f, 0f);
    }

    private void Start()
    {
        for(int i = 0;i < startNew; i++)
        {
            Unit unit = Instantiate(GameManager.Instance.characterPrefab[i]);
            unit.gameObject.transform.position = new Vector3(-11.5f,(float)i,0);
            unit.gameObject.transform.position = GameManager.Instance.GetTileMap().GetCellCenterLocal(Vector3Int.FloorToInt(unit.gameObject.transform.position));
            unit.transform.SetParent(NewbieBox.transform,false);

            if(i != GameManager.Instance.characterPrefab.Length - 1)
            {
                unit.IsNewBie = false;
            }
        }
        
    }

}
