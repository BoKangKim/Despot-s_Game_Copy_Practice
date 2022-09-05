using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBieSpawn : MonoBehaviour
{
    GameObject NewbieBox = null;
    int startNew = 4;
    int cost = 1;

    private void Awake()
    {
        NewbieBox = new GameObject("NewbieBox");
        NewbieBox.transform.position = new Vector3(0f, 0f, 0f);
    }

    private void Start()
    {
        CreateStartUnit();
    }

    private void CreateStartUnit()
    {
        Unit unit = Instantiate(GameManager.Instance.characterPrefab[0]);
        unit.gameObject.transform.position = new Vector3(-11.5f, -3f, 0);
        unit.gameObject.transform.position = GameManager.Instance.GetTileMap().GetCellCenterLocal(Vector3Int.FloorToInt(unit.gameObject.transform.position));
        unit.transform.SetParent(NewbieBox.transform, false);
        unit.IsNewBie = false;

        unit = Instantiate(GameManager.Instance.characterPrefab[5]);
        unit.gameObject.transform.position = new Vector3(-11.5f, -2f, 0);
        unit.gameObject.transform.position = GameManager.Instance.GetTileMap().GetCellCenterLocal(Vector3Int.FloorToInt(unit.gameObject.transform.position));
        unit.transform.SetParent(NewbieBox.transform, false);
        unit.IsNewBie = false;

        unit = Instantiate(GameManager.Instance.characterPrefab[9]);
        unit.gameObject.transform.position = new Vector3(-11.5f, -1f, 0);
        unit.gameObject.transform.position = GameManager.Instance.GetTileMap().GetCellCenterLocal(Vector3Int.FloorToInt(unit.gameObject.transform.position));
        unit.transform.SetParent(NewbieBox.transform, false);

        
    }

    private void CreateNewBie(Vector3 pos)
    {
        if (GameManager.Instance.coinCount < cost)
            return;

        Unit unit = Instantiate(GameManager.Instance.characterPrefab[GameManager.Instance.characterPrefab.Length - 1]);
        unit.gameObject.transform.position = pos;
        unit.transform.SetParent(NewbieBox.transform, false);
        GameManager.Instance.ChangeCoinText(-cost);
    }

}
