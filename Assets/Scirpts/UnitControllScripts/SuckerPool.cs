using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuckerPool : ObjectPool<AttackFire>
{
    static SuckerPool instance = null;

    public static SuckerPool Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new GameObject("SuckerPool").AddComponent<SuckerPool>();
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
