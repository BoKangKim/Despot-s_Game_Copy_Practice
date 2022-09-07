using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapScroll : MonoBehaviour
{
    float scrollSpeed = 15f;
    Vector3 prePos = Vector3.zero;
    bool isCheck = false;
    [SerializeField] int info;
    [SerializeField] GameObject[] minimapTile;
    [SerializeField] Tilemap tileMap;
    [SerializeField] Sprite[] imgs;
    GameObject minimap = null;
    GameObject myMini = null;
    SpriteRenderer sprite = null;
    int myIdx;
    int dir = -1;

    private void Awake()
    {
        if (gameObject.transform.position.y == 0
            && gameObject.transform.position.x == 0)
        {
            isCheck = true;
            GameManager.Instance.mapCount = info;
            GameManager.Instance.floorTileMap = tileMap;
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
            obj = Instantiate(minimapTile[0], Vector3.zero + (new Vector3(transform.position.x/210f,transform.position.y/160f,0)), Quaternion.identity);
            obj.transform.SetParent(minimap.transform, false) ;
        }
        else
        {
            obj = Instantiate(minimapTile[1], Vector3.zero + (new Vector3(transform.position.x / 210f, transform.position.y / 160f, 0)), Quaternion.identity);
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

    public void StartScroll(int dir)
    {
        this.dir = dir;
        StartCoroutine(Scroll());
    }

    IEnumerator Scroll()
    {
        while (true)
        {
            if(dir == 0)
            {
                gameObject.transform.Translate(Vector3.down * scrollSpeed * Time.deltaTime);
                if (prePos.y - 20 >= gameObject.transform.position.y)
                {
                    if (isCheck == true)
                    {
                        isCheck = false;
                        sprite.sprite = imgs[1];
                    }
                    gameObject.transform.position = new Vector3(gameObject.transform.position.x, prePos.y - 20, 0);
                    if (gameObject.transform.position.x == 0
                        && gameObject.transform.position.y == 0)
                    {
                        isCheck = true;
                        sprite.sprite = imgs[0];
                        GameManager.Instance.mapCount = info;
                        GameManager.Instance.floorTileMap = tileMap;
                    }
                    prePos = gameObject.transform.position;
                    GameManager.Instance.SetActiveTrueUnit();
                    GameManager.Instance.sceneState = SCENE_STATE.ASSIGN;

                    yield break;
                }
                
            }
            else if (dir == 1)
            {
                gameObject.transform.Translate(Vector3.left * scrollSpeed * Time.deltaTime);
                if (prePos.x - 35 >= gameObject.transform.position.x)
                {
                    if (isCheck == true)
                    {
                        isCheck = false;
                        sprite.sprite = imgs[1];
                    }
                    gameObject.transform.position = new Vector3(prePos.x - 35, gameObject.transform.position.y, 0);
                    if (gameObject.transform.position.x == 0
                        && gameObject.transform.position.y == 0)
                    {
                        isCheck = true;
                        sprite.sprite = imgs[0];
                        GameManager.Instance.mapCount = info;
                        GameManager.Instance.floorTileMap = tileMap;
                    }
                    prePos = gameObject.transform.position;
                    GameManager.Instance.SetActiveTrueUnit();
                    GameManager.Instance.sceneState = SCENE_STATE.ASSIGN;

                    yield break;
                }
            }
            else if (dir == 2)
            {
                gameObject.transform.Translate(Vector3.up * scrollSpeed * Time.deltaTime);
                if (prePos.y + 20 <= gameObject.transform.position.y)
                {
                    if (isCheck == true)
                    {
                        isCheck = false;
                        sprite.sprite = imgs[1];
                    }
                    gameObject.transform.position = new Vector3(gameObject.transform.position.x, prePos.y + 20, 0);
                    if (gameObject.transform.position.x == 0
                        && gameObject.transform.position.y == 0)
                    {
                        isCheck = true;
                        sprite.sprite = imgs[0];
                        GameManager.Instance.mapCount = info;
                        GameManager.Instance.floorTileMap = tileMap;
                    }
                    prePos = gameObject.transform.position;
                    GameManager.Instance.SetActiveTrueUnit();
                    GameManager.Instance.sceneState = SCENE_STATE.ASSIGN;

                    yield break;
                }
            }
            else if (dir == 3)
            {
                gameObject.transform.Translate(Vector3.right * scrollSpeed * Time.deltaTime);

                if (prePos.x + 35 <= gameObject.transform.position.x)
                {
                    if (isCheck == true)
                    {
                        isCheck = false;
                        sprite.sprite = imgs[1];
                    }
                    gameObject.transform.position = new Vector3(prePos.x + 35, gameObject.transform.position.y, 0);
                    if (gameObject.transform.position.x == 0
                        && gameObject.transform.position.y == 0)
                    {
                        isCheck = true;
                        sprite.sprite = imgs[0];
                        GameManager.Instance.mapCount = info;
                        GameManager.Instance.floorTileMap = tileMap;
                    }
                    prePos = gameObject.transform.position;
                    GameManager.Instance.SetActiveTrueUnit();
                    GameManager.Instance.sceneState = SCENE_STATE.ASSIGN;

                    yield break;
                }
            }


            yield return null;
        }
    }
}
