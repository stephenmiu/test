using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YurenSDK;

public class NewBehaviourScript : MonoBehaviour
{
    private void Awake()
    {
        HzzxSDKHandler.Instance.InitAndGetOpenId("1382", false);
    }

    // Start is called before the first frame update
    void Start()
    {


        
    }
}
