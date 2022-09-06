using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightButtonEvent : MonoBehaviour
{
    SpriteRenderer sprite;
    UnitClassControll unitClassControll = null;
    [SerializeField] Sprite buttonImg;
    GameObject[] Floors = null;
    [SerializeField] MonsterSpawn spawn;
    [SerializeField] GameObject[] mapInfo;
    Sprite startImg = null;

    bool isStart = false;

    void IsStart(bool isStart)
    {
        if (isStart == true)
            return;

        this.isStart = isStart;
        for(int i = 0; i < Floors.Length; i++)
        {
            Floors[i].SetActive(true);
        }
        mapInfo[GameManager.Instance.mapCount].SetActive(true);
    }

    private void Awake()
    {
        Floors = GameObject.FindGameObjectsWithTag("Floor");
        GameManager.Instance.IsStart += IsStart;
        GameManager.Instance.sab = SetActiveButton;
        unitClassControll = FindObjectOfType<UnitClassControll>();
        sprite = GetComponent<SpriteRenderer>();
        startImg = sprite.sprite;
    }
    
    private void OnMouseDown()
    {
        if (isStart == true)
            return;
        if (GameManager.Instance.sceneState != SCENE_STATE.ASSIGN)
            return;

        spawn.SendMessage("SpawnMonster", SendMessageOptions.RequireReceiver);
        isStart = true;
        GameManager.Instance.SendMessage("RequestIsStart",isStart,SendMessageOptions.DontRequireReceiver);
        sprite.sprite = buttonImg;
        for (int i = 0; i < Floors.Length; i++)
        {
            Floors[i].SetActive(false);
        }
        if (unitClassControll != null)
        {
            unitClassControll.SendMessage("DestroyList",SendMessageOptions.RequireReceiver);
        }
    }

    public void SetActiveButton()
    {
        sprite.sprite = startImg;
    }

}
