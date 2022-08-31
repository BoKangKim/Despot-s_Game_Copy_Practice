using System.Collections;
using System.Collections.Generic;
using UnityEngine.Tilemaps;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    [Header("À¯´Ö Á¤º¸")]
    [SerializeField] GameObject[] unitPrefab;
    [SerializeField] GameObject[] characterPrefab;
    [SerializeField] Tilemap floorTileMap;
    List<Unit> characters;
    DoorControll dc;
    
    public Tilemap GetTileMap()
    {
        return floorTileMap;
    }

    #region MonoBeHavior
    private void Awake()
    {
        characters = new List<Unit>();
        dc = FindObjectOfType<DoorControll>();
    }

    #endregion


    #region Units Info
    public GameObject GetUnitPrefab(int idx)
    {
        return unitPrefab[idx];
    }

    public GameObject GetCharacterPrefab(int idx)
    {
        return characterPrefab[idx];
    }

    public Unit GetIdxCharacter(int idx)
    {
        return characters[idx];
    }

    public void AddPositionList(Unit unit)
    {
        characters.Add(unit);
    }

    public int FindPosition(Unit unit)
    {
        int check = characters.FindIndex((values) =>
        {
            return unit == values;
        });

        return check;
    }

    public void RemoveUnitPosition(int index)
    {
        characters.RemoveAt(index);
    }

    public int GetUnitPosIndex()
    {
        return characters.Count - 1;
    }

    public Vector3 GetUnitPos(int index)
    {
        return characters[index].gameObject.transform.position;
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
