using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirePoint : MonoBehaviour
{
    [SerializeField] AttackFire FireObjects;
    ObjectPool<AttackFire> MyInstance = null;

    private void Awake()
    {
        if(FireObjects.name == "Arrow")
        {
            ArrowPool.Instance.preFab = FireObjects;
            MyInstance = ArrowPool.Instance;
        }

        Unit unit = gameObject.GetComponentInParent<Unit>();
        if (unit != null)
            unit.fire = Fire;
    }
    
    public void Fire(Monster target,Transform unitTrans)
    {
        AttackFire af = MyInstance.Get();
        af.gameObject.transform.SetParent(MyInstance.transform);
        af.gameObject.transform.position = unitTrans.position + new Vector3(0.1f,0.01f,0f);
        af.tarGet = target;
        af.release = Release;
        af.StartCoroutine(af.Fire());
    }

    public void Release(AttackFire af)
    {
        MyInstance.Release(af);
    }

}
