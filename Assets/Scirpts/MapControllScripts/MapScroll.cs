using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapScroll : MonoBehaviour
{
    float scrollSpeed = 5f;
    Vector3 prePos = Vector3.zero;
    bool isCheck = false;
    [SerializeField] int info;
    [SerializeField] GameObject[] minimapTile;
    [SerializeField] Sprite[] imgs;
    GameObject minimap = null;
    GameObject myMini = null;
    SpriteRenderer sprite = null;
    int myIdx;

    private void Awake()
    {
        if (gameObject.transform.position.y == 0)
        {
            isCheck = true;
            GameManager.Instance.mapCount = info;
        }
        minimap = GameObject.FindWithTag("Minimap");
        GameManager.Instance.scroll += StartScroll;
        prePos = gameObject.transform.position;
    }

    private void OnEnable()
    {
        GameObject obj = null;
        GameObject stateObj = null;
        if (isCheck == true)
        {
            obj = Instantiate(minimapTile[0], Vector3.zero + transform.position / 160f, Quaternion.identity);
            obj.transform.SetParent(minimap.transform, false) ;
        }
        else
        {
            obj = Instantiate(minimapTile[1], Vector3.zero + transform.position / 160f,Quaternion.identity);
            obj.transform.SetParent(minimap.transform, false);
        }
        myMini = obj;
        sprite = myMini.GetComponent<SpriteRenderer>();
        if(info == 0)
        {
            stateObj = Instantiate(minimapTile[3],Vector3.zero,Quaternion.identity);
            stateObj.gameObject.transform.SetParent(obj.transform, false);
        }
        else
        {
            stateObj = Instantiate(minimapTile[2],Vector3.zero,Quaternion.identity);
            stateObj.gameObject.transform.SetParent(obj.transform, false);
        }

    }

    public void StartScroll()
    {
        StartCoroutine(Scroll());
    }

    IEnumerator Scroll()
    {
        while (true)
        {
            gameObject.transform.Translate(Vector3.down * scrollSpeed * Time.deltaTime);

            if (prePos.y - 20 >= gameObject.transform.position.y)
            {
                if(isCheck == true)
                {
                    isCheck = false;
                    sprite.sprite = imgs[1];
                }
                gameObject.transform.position = new Vector3(gameObject.transform.position.x , prePos.y - 20, 0);
                if(gameObject.transform.position.y == 0)
                {
                    isCheck = true;
                    sprite.sprite = imgs[0];
                    GameManager.Instance.mapCount = info;
                }
                prePos = gameObject.transform.position;
                GameManager.Instance.SetActiveTrueUnit();
                GameManager.Instance.sceneState = SCENE_STATE.ASSIGN;

                yield break;
            }

            yield return null;
        }
    }
}
