using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MatchManager : MonoBehaviour {

    /// when in match, this manager will place all the players on the respective respawn points
    /// it will co relate with the GameManager script and organize matches
    /// 



    // Variables to be used on match manager
    public float matchTimer = 10; //timer to kill the lowest health player
    [SerializeField] private List<float> playerLives;
    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private Transform[] SpawnPoint;
    [SerializeField] private GameObject[] playerHealthBars;

    /*
    void Awake()
    {

        ////Match
        matchTimer = 60;
    }


    void FixedUpdate()
    {

        playerObjectReference = GameObject.FindGameObjectsWithTag("Player");
        if (playerObjectReference.Length == 1)
        {
            GameOverManager.game_over.GameOver(playerObjectReference[0]);
        }
        else if (playerObjectReference.Length <= 0)
        {
            GameOverManager.game_over.GameOver(gameObject);
        }


        timerText.text = matchTimer.ToString("F2");

        if (matchTimer <= 0) //Killing off a player
        {
            int playerIndex = 0;
            resetReferences();
            playerIndex = findLowestValue(playerLives, playerIndex);

            playerScripts[playerIndex].playerLives = 0; //Killing the lowest health player
            matchTimer = 30; //Reseting the timer

            resetReferences();
        }
        else
        {
            matchTimer -= Time.deltaTime;
        }
    }

    void resetReferences() //function to re-find all players inside game and all scripts to get their health points

    {
        playerObjectReference = GameObject.FindGameObjectsWithTag("Player");

        playerScripts.Clear();
        playerScripts = new List<Player>(new Player[playerObjectReference.Length]);
        playerLives.Clear();
        playerLives = new List<float>(new float[playerScripts.Count]);

        for (int i = 0; i < playerObjectReference.Length; i++)
        {
            playerScripts[i] = playerObjectReference[i].GetComponent<Player>();
            playerLives[i] = playerScripts[i].playerLives;
        }

    }

    int findLowestValue(List<float> number, int playerLive)
    {
        float lowestValue = number[0];
        var lowestIndex = 0;

        for (int i = 1; i < number.Count; i++)
        {
            if (number[i] < lowestValue)
            {
                lowestValue = number[i];
                lowestIndex = i;
            }
        }

        return lowestIndex;

    }

    */
}
