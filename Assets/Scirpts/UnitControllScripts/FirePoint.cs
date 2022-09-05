using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirePoint : MonoBehaviour
{
    [SerializeField] AttackFire FireObjects;
    ObjectPool<AttackFire> MyInstance = null;
    Unit myUnit = null;
    private void Awake()
    {
        if(FireObjects.name == "Arrow")
        {
            ArrowPool.Instance.preFab = FireObjects;
            MyInstance = ArrowPool.Instance;
        }
        else if(FireObjects.name == "Gun")
        {
            GunPool.Instance.preFab = FireObjects;
            MyInstance = GunPool.Instance;
        }else if(FireObjects.name == "SuckerBullet")
        {
            SuckerPool.Instance.preFab = FireObjects;
            MyInstance = SuckerPool.Instance;
        }

        myUnit = gameObject.GetComponentInParent<Unit>();
        if (myUnit != null)
            myUnit.fire = Fire;
    }
    
    public void Fire(Monster target)
    {
        AttackFire af = MyInstance.Get();
        af.gameObject.transform.SetParent(MyInstance.transform);
        af.gameObject.transform.position = gameObject.transform.position;
        af.tarGet = target;
        af.release = Release;
        af.MyUnitData = myUnit.GetUnitData();
        af.StartCoroutine(af.Fire());
    }

    public void Release(AttackFire af)
    {
        MyInstance.Release(af);
    }

}
