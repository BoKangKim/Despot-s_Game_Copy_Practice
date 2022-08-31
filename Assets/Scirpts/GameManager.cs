using System.Collections;
using System.Collections.Generic;
using UnityEngine.Tilemaps;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    

    [Header("À¯´Ö Á¤º¸")]
    [SerializeField] GameObject[] unitPrefab;
    [SerializeField] Tilemap floorTileMap;

    DoorControll dc;

    public Tilemap GetTileMap()
    {
        return floorTileMap;
    }

    #region MonoBeHavior
    private void Awake()
    {
        dc = FindObjectOfType<DoorControll>();
    }

    

    
    #endregion


    #region Units Info
    public GameObject GetUnitPrefab(int idx)
    {
        return unitPrefab[idx];
    }


    #endregion



    #region Door Event
    public bool IsTriggerExit { get; set; } = false;
   

    public void SendOpeningExit()
    {
        dc.SendMessage("SetAniOpening", SendMessageOptions.DontRequireReceiver);
    }

    public void SendClosingExit()
    {
        dc.SendMessage("SetAniClosing", SendMessageOptions.DontRequireReceiver);
    }
    #endregion


}
