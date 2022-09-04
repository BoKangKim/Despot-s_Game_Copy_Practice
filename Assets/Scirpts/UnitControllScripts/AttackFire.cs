using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void ReleaseThis(AttackFire af);

public class AttackFire : MonoBehaviour
{
    public Monster tarGet { get; set; }
    protected float speed = 3f;
    public ReleaseThis release;
    [SerializeField] ScriptableUnit MyData;
   

    public IEnumerator Fire()
    {
        Vector3 dir = Vector3.zero;


        while (tarGet != null)
        {
            if (tarGet != null)
                dir = (tarGet.transform.position - gameObject.transform.position).normalized;

            gameObject.transform.Translate(dir * 10f * Time.deltaTime);

            if (Vector3.Distance(tarGet.transform.position, gameObject.transform.position) <= 0.1f)
            {
                if (release != null)
                {
                    if (tarGet != null)
                        tarGet.SendMessage("TransferHP", MyData.AttackDamage, SendMessageOptions.DontRequireReceiver);
                    release(this);
                    yield break;
                }
            }

            yield return null;
        }

        release(this);
        
    }


}
