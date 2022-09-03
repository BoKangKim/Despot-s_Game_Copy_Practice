using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEventListener : MonoBehaviour
{

    public void SendOpeningExit()
    {
        GameManager.Instance.SendOpeningExit();
    }

    public void SendClosingExit()
    {
        GameManager.Instance.SendClosingExit();
    }
    
}
