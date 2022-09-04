using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowPool : ObjectPool<AttackFire>
{
    static ArrowPool instance = null;

    public static ArrowPool Instance 
    {
        get
        {
            if (instance == null)
            {
                instance = new GameObject("ArrowPool").AddComponent<ArrowPool>();
                instance.gameObject.transform.position = Vector3.zero;
            }

            return instance;
        }
    }

    public AttackFire preFab { get; set; } = null;

    public override AttackFire CreatePool()
    {
        return preFab;
    }
    
}
