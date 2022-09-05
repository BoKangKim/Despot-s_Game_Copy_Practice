using System.Collections;
using System.Collections.Generic;
using UnityEngine.Tilemaps;
using UnityEngine;
using UnityEngine.UI;

public enum SCENE_STATE
{
    ASSIGN,
    BATTLE,
    SHOP,
    SCROLL,
    MAX
}

public delegate void SetIsStart(bool isStart);
public delegate void StartScroll();
public class GameManager : Singleton<GameManager>
{
    [Header("UI Á¤º¸")]
    [SerializeField] Text CoinCount;
    [SerializeField] Text UnitCount;
    [SerializeField] Text FoodCount;
    public int coinCount { get; set; }
    public int unitCount { get; set; }
    public int foodCount { get; set; }

    [Header("À¯´Ö Á¤º¸")]
    [SerializeField] UnitClass[] unitPrefab;
    public Unit[] characterPrefab;
    [SerializeField] Tilemap floorTileMap;
    DoorControll dc;
    bool isStart = false;
    public SetIsStart IsStart = null;

    List<Monster> monsters = null;
    List<Unit> units = null;

    [Header("¸Ê Á¤º¸")]
    public StartScroll scroll;
    public SCENE_STATE sceneState { get; set; } = SCENE_STATE.ASSIGN;


    public void StartScrollCoroutine()
    {
        if (sceneState != SCENE_STATE.SHOP)
            return;

        sceneState = SCENE_STATE.SCROLL;
        ChangeFoodText(-units.Count);
        for(int i = 0; i < units.Count; i++)
        {
            units[i].gameObject.SetActive(false);
        }
        if (scroll != null)
            scroll();
    }

    void RequestIsStart(bool isStart)
    {
        this.isStart = isStart;
        if (IsStart != null)
            IsStart(isStart);
    }

    public Tilemap GetTileMap()
    {
        return floorTileMap;
    }

    #region MonoBeHavior
    private void Awake()
    {
        coinCount = 20;
        unitCount = 0;
        foodCount = 40;
        CoinCount.text = (coinCount).ToString();
        UnitCount.text = (unitCount).ToString();
        FoodCount.text = (foodCount).ToString();
        sceneState = SCENE_STATE.ASSIGN;
        
        units = new List<Unit>();
        monsters = new List<Monster>();
        dc = FindObjectOfType<DoorControll>();
        DontDestroyOnLoad(this);
    }

    #endregion

    #region UI Info
    public void ChangeCoinText(int count)
    {
        coinCount += count;
        CoinCount.text = (coinCount).ToString();
    }
    public void ChangeUnitText(int count)
    {
        unitCount += count;
        UnitCount.text = (unitCount).ToString();
    }
    public void ChangeFoodText(int count)
    {
        foodCount += count;
        FoodCount.text = (foodCount).ToString();
    }

    #endregion

    #region Units Info
    public UnitClass GetUnitPrefab(int idx)
    {
        return unitPrefab[idx];
    }

    public Unit GetCharacterPrefab(int idx)
    {
        return characterPrefab[idx];
    }

    public int UnitClassCount()
    {
        return unitPrefab.Length;
    }

    #endregion

    #region List Controll
    public void AddUnit(Unit unit)
    {
        units.Add(unit);
        ChangeUnitText(1);
    }

    public int GetMyIdx()
    {
        return units.Count - 1;
    }

    public void RemoveUnit(int idx)
    {
        if (units.Count == 0)
            return;

        units.RemoveAt(idx);

        ChangeUnitText(-1);
        for (int i = 0; i < units.Count; i++)
        {
            units[i].myIdx = i;
        }
    }

    public int UnitListCount()
    {
        return units.Count;
    }

    public void AddMonster(Monster monster)
    {
        monsters.Add(monster);
    }

    public int GetMonsterIdx()
    {
        return monsters.Count - 1;
    }

    public void RemoveMonster(int idx)
    {
        if (monsters.Count == 0)
            return;

        monsters.RemoveAt(idx);

        for(int i = 0; i < monsters.Count; i++)
        {
            monsters[i].myIdx = i;
        }
    }

    public int GetMonsterListCount()
    {
        return monsters.Count;
    }

    #endregion

    #region Battle Move Position

    public Monster FindTargetMonster(Unit unit)
    {
        if (monsters.Count == 0)
            return null;

        int resultIdx = 0;
        float distance = 0;

        distance = Vector3.Distance(unit.transform.position,monsters[0].gameObject.transform.position);
        
        if(monsters.Count > 1)
        {
            for (int i = 0; i < monsters.Count; i++)
            {
                float temp = Vector3.Distance(unit.transform.position, monsters[i].gameObject.transform.position);

                if (distance > temp)
                {
                    distance = temp;
                    resultIdx = i;
                }
            }
        }

        return monsters[resultIdx];
    }

    public Unit FindTargetUnit(Monster monster)
    {
        if (units.Count == 0)
            return null;

        int resultIdx = 0;
        float distance = 0f;

        distance = Vector3.Distance(monster.transform.position, units[0].gameObject.transform.position);

        for (int i = 0; i < units.Count; i++)
        {
            float temp = Vector3.Distance(monster.transform.position, units[i].gameObject.transform.position);
            
            if (distance > temp)
            {
                distance = temp;
                resultIdx = i;
            }
        }

        return units[resultIdx];
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
