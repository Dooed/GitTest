using System.Collections;
using UnityEngine;
using System;
using TMPro;

public enum battleState { START, PLAYERTURN, ENEMYTURN, WON, LOST }
public class turns : MonoBehaviour
{
    public battleState state;
    public static event Action<int,int> onDamageEnemy;
    
    private static int enemyIntent;

    public GameObject defenceAura, attackAura, neutralAura;


    [SerializeField]
    private TextMeshProUGUI deffenceCredit;
    private int counter = 4;
    

    bool canAttack;
    public float MaxTimer;
    private float timecounter;
    [SerializeField]
    private TextMeshProUGUI vunrable;

    

    enemyScript enemy;

    void Start()
    {
       state = battleState.START;
        setUpBattle();

    }
    private void Update()
    {
        deffenceCredit.text = "Defence Credit :" + counter.ToString();
        vunrable.text = "Enemy vulnerable: " + canAttack;
    }
    //new turn will start and wait for you to make an action (press a button)
    private void setUpBattle()
    {
        Debug.Log("new turn started");
        state=battleState.PLAYERTURN;

        //there is a pattern for the enemy below, A for attack, D for defenceAura , N for neutral
        // A D A A D 
        // 0 1 2 3 4 
        //enemy intent: it is the turn number-> enemy intent=0 means turn is 0 and he intends to attack 
        if (enemyIntent == 0 || enemyIntent == 3 || enemyIntent == 4)
        {
            attackAura.SetActive(true);
            defenceAura.SetActive(false);
            neutralAura.SetActive(false);
        }
        else if (enemyIntent == 1 || enemyIntent == 5)
        {
            attackAura.SetActive(false);
            defenceAura.SetActive(true);
            neutralAura.SetActive(false);
        }
        else
        {
            attackAura.SetActive(false);
            defenceAura.SetActive(false);
            neutralAura.SetActive(true);
        }



    }
    


    //button from the ui that you press to attack
    //this will invoke a new action (in turn script) to send that the player intends to attack
    // 1 is attack, -1 is defence and 0 is neutral, which means you can attack the enemy without him attacking or defending
    public void playerAttackButton()
    {
        if(state!=battleState.PLAYERTURN)
            return;

        
        
        Debug.Log("pressed attack");

        sendToInvoke(1);

        state = battleState.ENEMYTURN;
        setUpBattle();

    }

    //button from the ui that you press to defend
    //this will invoke the same action called sendToInvoke (in turn script) to send that the player intends to defend
    // 1 is attack, -1 is defenceAura
    public void playerDefenceButton()
    {
        if (counter > 0)
        {
            if (state != battleState.PLAYERTURN)
                return;
            sendToInvoke(-1);
            counter--;
            state = battleState.ENEMYTURN;
            setUpBattle();
        }
       else
        {
            deffenceCredit.text = "Out of defence Credit";
        }
        
    }
    
    //extra function 
    private void enemyTurn()
    {
        Debug.Log("enemy turn");
        state = battleState.PLAYERTURN;
      
    }
   

    private void OnEnable()
    {
        enemyScript.backToStates += setUpBattle;
        playerScript.playerBackToStates += setUpBattle;
    }
    private void OnDisable()
    {
        enemyScript.backToStates -= setUpBattle;
        playerScript.playerBackToStates -= setUpBattle;
    }
    private void CountDown()
    {
        canAttack = !canAttack;
       
    }


    //this function will check the player action and enemy intent, enemy intent should be either attack or defenceAura,
    //and player action should be attack or defenceAura
    //there is a pattern for the enemy below, A for attack, D for defenceAura , N for neutral
    // A D N A A D N
    // 0 1 2 3 4 5 6
    //^the numbers imply what the enemy will do each turn (variable is called enemyIntent) and on1 turn 0 the enemy will attack and so on1 
    //this will activate an action called onDamageEnemy which will either attack the enemy or defend against it, this will be activated in the enemyScript
    private void sendToInvoke(int playerAction)
    {
        if (enemyIntent == 0 || enemyIntent == 3 || enemyIntent == 4)
        {
            Debug.Log("enemy intends to attack");
            onDamageEnemy.Invoke(1, playerAction);
            state = battleState.PLAYERTURN;
        }

        else if (enemyIntent == 1 || enemyIntent == 5)
        {
            Debug.Log("enemy intends to defend");
            onDamageEnemy.Invoke(-1, playerAction);
            state = battleState.PLAYERTURN;
        }
        else
        {
            Debug.Log("enemy is neutral");
            onDamageEnemy.Invoke(0, playerAction);
        }

        enemyIntent++;
        if (enemyIntent % 7 == 0) enemyIntent = 0;
    }

    //extra
    IEnumerator neutralToAttack()
    {
        yield return new WaitForSeconds(2f);

        Debug.Log("waited for 2 secs");
    }
}
