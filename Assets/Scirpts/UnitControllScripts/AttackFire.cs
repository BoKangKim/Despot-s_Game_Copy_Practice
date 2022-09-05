using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void ReleaseThis(AttackFire af);

public class AttackFire : MonoBehaviour
{
    public Monster tarGet { get; set; }
    protected float speed = 3f;
    float AttackPower = 0f;
    public ReleaseThis release;
    [SerializeField] ScriptableBullet MyData;
    [HideInInspector] public ScriptableUnit MyUnitData;

    private void OnEnable()
    {
        if(MyUnitData != null)
            AttackPower = MyUnitData.AttackDamage;
    }

    public IEnumerator Fire()
    {
        Vector3 dir = Vector3.zero;


        while (tarGet != null)
        {
            if (tarGet != null)
                dir = (tarGet.transform.position - gameObject.transform.position).normalized;

            gameObject.transform.Translate(dir * MyData.Speed * Time.deltaTime);

            if (Vector3.Distance(tarGet.transform.position, gameObject.transform.position) <= 0.1f)
            {
                if (release != null)
                {
                    if (tarGet != null)
                        tarGet.SendMessage("TransferHP",AttackPower ,SendMessageOptions.DontRequireReceiver);
                    release(this);
                    yield break;
                }
            }

            yield return null;
        }

        release(this);
        
    }


}
