using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : Singleton<T>
{
    #region 싱글턴
    private static T instance = null; // 싱글톤을 할당할 전역 변수
    static public T Instance
    {
        get
        {
            if (instance == null)
            {
                instance = GameObject.FindObjectOfType<T>();
                if (instance == null)
                {
                    instance = new GameObject().AddComponent<T>();
                    if (instance == null)
                        Debug.LogError("Not Found Singleton Class");
                }
            }

            return instance;
        }
    }
    #endregion
    
}
