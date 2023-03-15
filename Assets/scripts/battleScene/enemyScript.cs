using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using TMPro;
public class enemyScript : MonoBehaviour
{   
    public List<GameObject> hearts,heartsPrefabs;
    GameObject addedPrefab;
    [SerializeField]
    GameObject heart;
    Vector3 startingPoint;
    public static event Action backToStates;
    public static event Action onAttackPlayer;
    public TextMeshProUGUI EnemyDead;

    static int defenceCounter,breakEnemyDefence;
    static int enemyCounter;
    public TextMeshProUGUI brokendefence;
    public Button returnButton;

    //this is where you instantiate the hearts for the enemy, there will be 3 hearts
    //i also made the objects instantiate in a different posistion so they do not sit on1 top of eachother
    void Start()
    {
        
        startingPoint = new Vector3(-7.78768f, -2.884205f, 0);
        hearts = new List<GameObject>();                          //this list is to count the number of prefabs
        heartsPrefabs = new List<GameObject>();                   //this list is to add the instantiated prefabs and connect the prefabs to the list that will count them (the list above)
       

        defenceCounter = 3;

        for (int i = 0; i < 7; i++)
        {
            hearts.Add(heart);
        }
        for(int i = 0; i < hearts.Count; i++)
        {

            if (i == 0)
            {
                addedPrefab=Instantiate<GameObject>(heart, new Vector3(startingPoint.x + (i), startingPoint.y, 0), transform.rotation);
                heartsPrefabs.Add(addedPrefab);
            }
            else if (i < 5 && i != 0)
            {
                startingPoint.x += 1.2f;
                addedPrefab = Instantiate<GameObject>(heart, new Vector3(startingPoint.x, startingPoint.y, 0), transform.rotation);
                heartsPrefabs.Add(addedPrefab);

            }


            else
            {

                startingPoint.x -= 2;
                addedPrefab  =  Instantiate<GameObject>(heart, new Vector3(startingPoint.x, startingPoint.y - 1.3f, 0), transform.rotation);
                startingPoint.x += 0.7f;
                heartsPrefabs.Add(addedPrefab);

            }
        }
    }


    
    private void OnEnable()
    {
        turns.onDamageEnemy += removeHeart;
     
    }
    /// <summary>
    /// this is an activated action from turns script, which will take enemy state and player state, either 1 for attack or -1 for defenceAura
    /// and  when the enemy is in defenceAura, and the player attacks their defenceAura for the third time the defenceAura breaks and they lose a heart
    /// </summary>
    public void removeHeart(int enemyState, int playerState)
    {
        
        Debug.Log(hearts.Count+" hearts for enemy");
        if (hearts.Count > 0 && enemyState==1 && playerState==1)
        {
            Debug.Log( "enemy lost 1 heart");
            Destroy(heartsPrefabs[heartsPrefabs.Count-1]);
            heartsPrefabs.RemoveAt(heartsPrefabs.Count - 1);
            hearts.RemoveAt(hearts.Count - 1);

            if (hearts.Count == 0)
            {
                Debug.Log("enemy ded");
                EnemyDead.text = "Enemy is dead";
                returnButton.gameObject.SetActive(true);
            }

            onAttackPlayer.Invoke();

            backToStates.Invoke();
        }
        else if(hearts.Count>0 && playerState== -1 && defenceCounter>0 )
        {
            defenceCounter--;
            backToStates.Invoke();
            if (hearts.Count == 0)
            {
                EnemyDead.text = "Enemy is dead";
                Debug.Log("enemy ded");
                returnButton.gameObject.SetActive(true);
            }
        }
        else if(hearts.Count > 0 && enemyState == -1 && playerState == 1)
        {

            breakEnemyDefence++;
            Debug.Log("enemy broken defenceAura= " + breakEnemyDefence);
            if (breakEnemyDefence == 3)
            {
               
                Debug.Log("enemy defenceAura broken");
                Destroy(heartsPrefabs[heartsPrefabs.Count - 1]);
                heartsPrefabs.RemoveAt(heartsPrefabs.Count - 1);
                hearts.RemoveAt(hearts.Count - 1);

                backToStates.Invoke();
            }
            if (hearts.Count == 0)
            {
                Debug.Log("enemy ded");
                returnButton.gameObject.SetActive(true);
            }
        }
        else if(hearts.Count > 0 && enemyState == 0 && playerState == 1)
        {
           // Debug.Log("enemy lost 1 heart");
            Destroy(heartsPrefabs[heartsPrefabs.Count - 1]);
            heartsPrefabs.RemoveAt(heartsPrefabs.Count - 1);
            hearts.RemoveAt(hearts.Count - 1);

            backToStates.Invoke();
            if (hearts.Count == 0)
            {
                EnemyDead.text = "Enemy is dead";
                returnButton.gameObject.SetActive(true);
                Debug.Log("enemy ded");
            }

        }
        if (defenceCounter == 0)
        {
            Debug.Log("cannot do more defenceAura");
            backToStates.Invoke();
        }

    }
    private void OnDisable()
    { 
        turns.onDamageEnemy -= removeHeart;
       
    }

    private void states(int charNUM)
    {

    }
}
