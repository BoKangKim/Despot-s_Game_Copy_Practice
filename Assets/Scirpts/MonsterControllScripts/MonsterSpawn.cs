using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawn : MonoBehaviour
{
    [SerializeField] GameObject SpawnArea;
    [SerializeField] GameObject Monster;
    GameObject MonsterSpanwBox = null;
    List<Vector3> spawnPosList;
    List<GameObject> spawnList;
    float count = 0.5f;
    int countMonster = 10;

    private void Awake()
    {
        spawnPosList = new List<Vector3>();
        spawnList = new List<GameObject>();
        MonsterSpanwBox = new GameObject("MonsterSpawnBox");
        MonsterSpanwBox.transform.position = new Vector3(0f,0f,0f);
    }

    private void Start()
    {
        CreateRndSpawnPoint();
    }

    private void SpawnMonster()
    {
        if (spawnList.Count == 0)
            return;

        for(int i = 0; i < spawnPosList.Count; i++)
        {
            GameObject monster = Instantiate(Monster, spawnPosList[i], Quaternion.identity);
            monster.transform.SetParent(MonsterSpanwBox.transform, true);

            Destroy(spawnList[i].gameObject);
        }

        spawnList.Clear();
        spawnPosList.Clear();
    }

    // X Max 12.5
    // X Min 0

    // Y Max 4.5
    // Y Min -6.5
    private void CreateRndSpawnPoint()
    {
        if (spawnList.Count != 0)
            return;

        int i = 0;
        do
        {
            float rndX = Random.Range(0,12.5f);
            float rndY = Random.Range(-6.5f, 4.5f);
            Vector3 newPos = new Vector3(rndX,rndY,0);

            if (spawnPosList.Contains(newPos))
            {
                continue;
            }

            spawnPosList.Add(newPos);
            i++;
        } while (i < countMonster);

        for (int j = 0; j < spawnPosList.Count; j++)
        { 
            GameObject spawn = Instantiate(SpawnArea, spawnPosList[j], Quaternion.identity);
            spawn.transform.position = GameManager.Instance.GetTileMap().GetCellCenterLocal(Vector3Int.FloorToInt(spawn.transform.position));
            spawn.transform.SetParent(MonsterSpanwBox.transform, true);
            spawn.gameObject.SetActive(true);
            spawnList.Add(spawn);
        }
    }


}
