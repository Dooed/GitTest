using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using UnityEngine;
using System;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class switches : MonoBehaviour
{
    public GameObject awake1, awake2, awake3;

    public Button returnToMap;

    private static bool on1 = false, on2 = false, on3 = false;



    private void Start()
    {
       
    }

    
    public void switch1()
    {
         awake2.SetActive(true);
         awake3.SetActive(false);

       
        on2 = true;
        on3 = false;

        if (on1 && on2 && on3)
        {

            returnToMap.gameObject.SetActive(true);
            on1 = on2 = on3 = false;
        }

    }
    public void switch2()
    {
       
        awake3.SetActive(true);

        on3 = true;    
        on3 = true;
        
        if (on1 && on2 && on3)
        {

            returnToMap.gameObject.SetActive(true);
            on1 = on2 = on3 = false;
        }
    }

    public void switch3()
    {
        awake1.SetActive(true);
        awake3.SetActive(false);

        on1 = true;
        on3 = false;

        if (on1 && on2 && on3)
        {

            returnToMap.gameObject.SetActive(true);
            on1 = on2 = on3 = false;
        }

    }
}
