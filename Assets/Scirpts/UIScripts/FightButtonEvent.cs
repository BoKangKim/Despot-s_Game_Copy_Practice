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

    bool isStart = false;

    private void Awake()
    {
        unitClassControll = FindObjectOfType<UnitClassControll>();
        sprite = GetComponent<SpriteRenderer>();
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
