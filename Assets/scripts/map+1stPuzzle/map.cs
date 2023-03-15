using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
public class map : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject enemy, question1, question2,mapGO;
    public Button switch1, switch2, switch3;
    public TextMeshProUGUI wakeUp;
    private void Start()
    {
        switch1.gameObject.SetActive(false);
        switch2.gameObject.SetActive(false);
        switch3.gameObject.SetActive(false);
        wakeUp.gameObject.SetActive(false);
    }
    private void OnMouseEnter()
    {
        if (gameObject.name == "colliderquestion1")
        {
            question1.SetActive(true);
        }
        if (gameObject.name == "colliderquestion2")
        {
            question2.SetActive(true);
        }
       
        if (gameObject.name == "colliderenemy1")
        {
            enemy.SetActive(true);
        }


    }
    private void OnMouseExit()
    {
        if (gameObject.name == "colliderquestion1")
        {
            question1.SetActive(false);
        }
        if (gameObject.name == "colliderquestion2")
        {
            question2.SetActive(false);
        }
        
        if (gameObject.name == "colliderenemy1")
        {
            enemy.SetActive(false);
        }
    }
    private void OnMouseDown()
    {
        if(gameObject.name == "colliderquestion1")
        {
            mapGO.SetActive(false);
            switch1.gameObject.SetActive(true);
            switch2.gameObject.SetActive(true);
            switch3.gameObject.SetActive(true);
            wakeUp.gameObject.SetActive(true);
        }
        else if(gameObject.name == "colliderquestion2")
        {
            SceneManager.LoadScene(1);
        }
        else 
        {
            SceneManager.LoadScene(2);
        }

    }
}
