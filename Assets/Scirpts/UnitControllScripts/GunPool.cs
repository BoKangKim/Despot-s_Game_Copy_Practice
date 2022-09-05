using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunPool : ObjectPool<AttackFire>
{
    static GunPool instance = null;

    public static GunPool Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new GameObject("GunPool").AddComponent<GunPool>();
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
