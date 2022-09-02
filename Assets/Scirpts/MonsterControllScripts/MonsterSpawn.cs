using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawn : MonoBehaviour
{
    [SerializeField] GameObject SpawnArea;
    [SerializeField] GameObject Monster;
    GameObject MonsterSpanwBox = null;
    int countMonster = 2;
    Vector3[] SpawnAreaPos = null;
    List<GameObject> spawnList;
    float count = 0.5f;
    // X Max 12.5
    // X Min 0

    // Y Max 4.5
    // Y Min -6.5
    private void Awake()
    {
        spawnList = new List<GameObject>();
        SpawnAreaPos = new Vector3[countMonster];
        MonsterSpanwBox = new GameObject("MonsterSpawnBox");
        MonsterSpanwBox.transform.position = new Vector3(0f,0f,0f);
    }

    private void Start()
    {
        for(int i = 0; i < countMonster; i++)
        {
            GameObject spawn = Instantiate(SpawnArea,new Vector3(5.5f,(float)(i + count - 3.5f),0),Quaternion.identity);
            spawn.transform.position = GameManager.Instance.GetTileMap().GetCellCenterLocal(Vector3Int.FloorToInt(spawn.transform.position));
            spawn.transform.SetParent(MonsterSpanwBox.transform,true);
            SpawnAreaPos[i] = spawn.transform.position;

            spawnList.Add(spawn);
            count += 2f;
        }
    }

    private void SpawnMonster()
    {
        for(int i = 0; i < countMonster; i++)
        {
            GameObject monster = Instantiate(Monster, SpawnAreaPos[i], Quaternion.identity);
            monster.transform.SetParent(MonsterSpanwBox.transform, true);

            Destroy(spawnList[i].gameObject);
        }

        spawnList.Clear();
    }


}
