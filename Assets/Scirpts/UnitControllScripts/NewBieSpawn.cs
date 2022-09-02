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
        for(int i = 0;i < 2; i++)
        {
            Unit Sword = Instantiate(BroadSwordPrefab);
            Sword.gameObject.transform.position = new Vector3(-13.5f,(float)i,0);
            Sword.gameObject.transform.position = GameManager.Instance.GetTileMap().GetCellCenterLocal(Vector3Int.FloorToInt(Sword.gameObject.transform.position));
            Sword.IsNewBie = false;
            Sword.transform.SetParent(NewbieBox.transform,false);
        }

        Unit newbie = Instantiate(newBiePrefab);
        newbie.gameObject.transform.position = new Vector3(-13.5f, (float)startNew - 1, 0);
        newbie.gameObject.transform.position = GameManager.Instance.GetTileMap().GetCellCenterLocal(Vector3Int.FloorToInt(newbie.gameObject.transform.position));
        newbie.transform.SetParent(NewbieBox.transform, false);
    }

}
