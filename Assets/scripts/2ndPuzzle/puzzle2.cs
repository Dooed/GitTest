using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using UnityEngine;
using System;
using Unity.VisualScripting;
using UnityEngine.SceneManagement;
using System.Diagnostics.Tracing;
using UnityEngine.UI;

public class puzzle2 : MonoBehaviour
{
    
    private Rigidbody2D monkeRB;
    private static int cnt=0;
    public Button returnButton;
    public Sprite deactivatedSprite;

    

    private LayerMask blue, white, black;
    

    private void Start()
    {
        monkeRB = GetComponent<Rigidbody2D>();
        
        cnt = 6;
        

    }
    private void FixedUpdate()
    {
        float h = Input.GetAxis("Horizontal") * 5;
        float v = Input.GetAxis("Vertical") * 5;

        Vector3 val=monkeRB.velocity;
        val.x = h;
        val.y = v;
        monkeRB.velocity = val;
       
    }

    // 6 is blue, 7 is white, 8 is black
    private void OnTriggerEnter2D(Collider2D other)
    {
        
        monkeRB.velocity = Vector3.zero;
        monkeRB.angularVelocity = 0;
        cnt--;

        if(other.gameObject.layer==6)
        {

            other.gameObject.layer = 8;
            other.gameObject.GetComponent<SpriteRenderer>().sprite = deactivatedSprite;

            if (cnt == 0)
            {
                returnButton.gameObject.SetActive(true);
            }           
        }
        else
            {
                SceneManager.LoadScene(1);
            }
    }
   


}
