
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using TMPro;
public class playerScript : MonoBehaviour
{
    public List<GameObject> hearts, heartsPrefabs;
    GameObject addedPrefab;

    [SerializeField]
    GameObject heart;

    Vector3 startingPoint;

   
    public static event Action playerBackToStates;
    public TextMeshProUGUI displayWL;

    /// <summary>
    /// this is a script that instantiates the player hearts and destroys them
    /// </summary>
    /// 



    //this is where you instantiate the hearts for the player, there will be 3 hearts
    //i also made the objects instantiate in a different posistion so they do not sit on1 top of eachother
    void Start()
    {

        startingPoint = new Vector3(3, 3.4f, 0);
        hearts = new List<GameObject>();            //this list is to count the number of prefabs
        heartsPrefabs = new List<GameObject>();     //this list is to add the instantiated prefabs and connect the prefabs to the list that will count them (the list above)


        for (int i = 0; i < 3; i++)
        {
            hearts.Add(heart);
        }
        for (int i = 0; i < hearts.Count; i++)
        {

            if (i == 0)
            {
                addedPrefab = Instantiate<GameObject>(heart, new Vector3(startingPoint.x + (i), startingPoint.y, 0), transform.rotation);
                heartsPrefabs.Add(addedPrefab);
            }
            else if (i < 3 && i != 0)
            {
                startingPoint.x += 1.2f;
                addedPrefab = Instantiate<GameObject>(heart, new Vector3(startingPoint.x, startingPoint.y, 0), transform.rotation);
                heartsPrefabs.Add(addedPrefab);

            }         
        }
    }


    //enemy script has an action that we activate here, it will basically tell that you need to remove a heart from the player
    //because the enemy intended to attack and you did not opt for defenceAura
    private void OnEnable()
    {
        enemyScript.onAttackPlayer += attackPlayer;
    }
    private void attackPlayer()
    {
       
        
            Debug.Log("player lost 1 heart");
            Destroy(heartsPrefabs[heartsPrefabs.Count - 1]);
            heartsPrefabs.RemoveAt(heartsPrefabs.Count - 1);
            hearts.RemoveAt(hearts.Count - 1);

            if (hearts.Count == 0)
            {
                Debug.Log("player ded");
                displayWL.text = "Player is dead";
                
            }
            playerBackToStates.Invoke();
        
    }
    private void OnDisable()
    {
        enemyScript.onAttackPlayer += attackPlayer;
    }

}
