using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapScroll : MonoBehaviour
{
    float scrollSpeed = 5f;
    Vector3 prePos = Vector3.zero;

    private void Awake()
    {
        GameManager.Instance.scroll += StartScroll;
        prePos = gameObject.transform.position;
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
                gameObject.transform.position = new Vector3(gameObject.transform.position.x , prePos.y - 20, 0);
                prePos = gameObject.transform.position;
                GameManager.Instance.sceneState = SCENE_STATE.ASSIGN;
                yield break;
            }

            yield return null;
        }
    }
}
