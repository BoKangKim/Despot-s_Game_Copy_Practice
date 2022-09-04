using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightButtonEvent : MonoBehaviour
{
    SpriteRenderer sprite;
    UnitClassControll unitClassControll = null;
    [SerializeField] Sprite buttonImg;
    [SerializeField] GameObject Floor;
    [SerializeField] MonsterSpawn spawn;
    [SerializeField] GameObject unitClass;
    Sprite startImg = null;

    bool isStart = false;

    void IsStart(bool isStart)
    {
        if (isStart == true)
            return;

        this.isStart = isStart;
        Floor.SetActive(true);
        sprite.sprite = startImg;
        unitClass.SetActive(true);
    }

    private void Awake()
    {
        GameManager.Instance.IsStart += IsStart;
        unitClassControll = FindObjectOfType<UnitClassControll>();
        sprite = GetComponent<SpriteRenderer>();
        startImg = sprite.sprite;
    }

    private void OnMouseDown()
    {
        if (isStart == true)
            return;

        spawn.SendMessage("SpawnMonster", SendMessageOptions.RequireReceiver);
        isStart = true;
        GameManager.Instance.SendMessage("RequestIsStart",isStart,SendMessageOptions.DontRequireReceiver);
        sprite.sprite = buttonImg;
        Floor.SetActive(false);
        if (unitClassControll != null)
        {
            unitClassControll.SendMessage("DestroyList",SendMessageOptions.RequireReceiver);
        }
    }
}
