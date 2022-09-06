using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogoControll : MonoBehaviour
{
    [Header("UI Á¤º¸")]
    [SerializeField] GameObject tbLogo;
    [SerializeField] GameObject kgLogo;
    [SerializeField] GameObject title;
    [SerializeField] Canvas ButtonCanvas;
    [SerializeField] Texture2D defaultCursor;

    SpriteRenderer tbRender = null;
    SpriteRenderer kgRender = null;
    float colorA = 255f;
    float removeSpeed = 80f;

    private void Awake()
    {
        Cursor.SetCursor(defaultCursor, Vector2.zero, CursorMode.ForceSoftware);
        tbRender = tbLogo.GetComponent<SpriteRenderer>();
        kgRender = kgLogo.GetComponent<SpriteRenderer>();
        StartCoroutine(StartTBLogo());
    }

    IEnumerator StartTBLogo()
    {
        tbLogo.SetActive(true);
        while (true)
        {
            colorA -= (Time.deltaTime * removeSpeed);
            if(colorA <= 0)
            {
                colorA = 255f;
                tbLogo.SetActive(false);
                StartCoroutine(StartKGLogo());
                yield break;
            }
            tbRender.color = new Color(1f,1f,1f, colorA / 255f);
            yield return null;
        }
    }

    IEnumerator StartKGLogo()
    {
        kgLogo.SetActive(true);
        while (true)
        {
            colorA -= (Time.deltaTime * removeSpeed);
            if (colorA <= 0)
            {
                colorA = 255f;
                kgLogo.SetActive(false);
                title.SetActive(true);
                ButtonCanvas.gameObject.SetActive(true);
                yield break;
            }
            kgRender.color = new Color(1f, 1f, 1f, colorA / 255f);
            yield return null;
        }
    }

}
