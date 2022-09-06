using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour
{
    [SerializeField] ScriptableFood MyData;
    GameObject CostText;
    int count = 0;
    private void Awake()
    {
        count = MyData.Count;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (GameManager.Instance.coinCount < MyData.Cost)
                return;

            if(MouseControll.Instance.mousePos == this.gameObject.transform.position)
            {
                GameManager.Instance.ChangeFoodText(MyData.AddFoodCount);
                GameManager.Instance.ChangeCoinText(-MyData.Cost);
                count -= 1;

                if (count <= 0)
                {
                    Destroy(this.gameObject);
                }
            }
            
        }
    }

   
}
