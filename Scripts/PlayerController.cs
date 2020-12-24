using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class PlayerController : MonoBehaviour
{

    public class InputManager {

        public KeyCode Bomb { get; set; }


    }




    /// Player controller manager will be another singleton that will always 
    /// keep track of how many players are in the game, it will spawn based on the input manager

    //Singleton variable
    public static PlayerController PlayerControllerInstance;

    private void Start()
    {
        //Singleton
        if (PlayerControllerInstance != null)
        {
            Destroy(gameObject);
        } else
        {
            PlayerControllerInstance = this;
            DontDestroyOnLoad(gameObject);
        }

    }

    //Variables to keep track of players
    public int numberOfPlayers = 0; // to keep track of the amount of players in the game
    public int playerNumber = 0; //to know which player it is
    private List<Player> playerScripts; //scripts to access player controls
    private GameObject[] playerObjectReference; // just to know which is which by tag
    private GameObject body; // instantiating the body depending on player >>>>>>>>>>> going to replace this part
    [SerializeField] private GameObject playerPrefabForSpawing;
    private GameObject playerCanvas;

    private string[] JoystickNames;
    private List<int> usedJoystickSlots = new List<int>();
    private List<int> playerIndex = new List<int>(); //this variable if for knowing what player it is for spawning 

    public void AddXbox360Player(int joystickNumber)
    {
        print("xbox 360");
    }

    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////   ADD PLAYER

    public void AddPlayer(int joystickNumber, string type) ///This function is to be called whenever you want to add a new player
    {
        //check if player index is not already with this joystick number
        bool usedJoystick = false;
        bool emptySpace = false;
        int emptySpaceIndex = 0;

        for (int i = 0; i < playerIndex.Count; i++)
        {
            if (playerIndex[i] == 0) //means there is an empty space
            {
                emptySpace = true;
                emptySpaceIndex = i + 1;
                break;
            }

            if (playerIndex[i] == joystickNumber)
            {
                usedJoystick = true;
                break;
            }
        }



        if (emptySpace && !usedJoystick) //spawn at the empty space
        {
            GameObject player = Instantiate(playerPrefabForSpawing, transform.position, Quaternion.identity);
            numberOfPlayers++;

            playerIndex[emptySpaceIndex - 1] = joystickNumber;
            player.name = "Player" + emptySpaceIndex.ToString();

            player.GetComponent<Player>().playerNumber = playerNumber;
            player.GetComponent<Player>().horizontal = type + "LeftH" + joystickNumber;
            player.GetComponent<Player>().vertical = type + "LeftV" + joystickNumber;
            player.GetComponent<Player>().Rhorizontal = type + "RightH" + joystickNumber;
            player.GetComponent<Player>().Rvertical = type + "RightV" + joystickNumber;
            print(joystickNumber);
            print(type);
        }

        if (!usedJoystick && !emptySpace)
        {
            GameObject player = Instantiate(playerPrefabForSpawing, transform.position, Quaternion.identity);
            numberOfPlayers++;
            playerNumber++;
            playerIndex.Add(joystickNumber);
            player.name = "Player" + playerNumber.ToString();
            player.GetComponent<Player>().playerNumber = playerNumber;
            player.GetComponent<Player>().horizontal = type + "LeftH" + joystickNumber;
            player.GetComponent<Player>().vertical = type + "LeftV" + joystickNumber;
            player.GetComponent<Player>().Rhorizontal = type + "RightH" + joystickNumber;
            player.GetComponent<Player>().Rvertical = type + "RightV" + joystickNumber;
            print(joystickNumber);
            print(type);

        }

    }

    ///////////////////////END
    /// ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////


    public void RemovePlayer(int playerNumber)
    {
        GameObject player = GameObject.Find("Player" + playerNumber);
        string index = player.GetComponent<Player>().horizontal.Substring(player.GetComponent<Player>().horizontal.Length - 1); //Grab the last character out of the horizontal axis for this object
        int finalNumber = int.Parse(index);

        //now that we have the joystick index, we need to remove it in a backwards way from the "Add"
        numberOfPlayers--;
        //Space for removing the player index list
        for (int i = 0; i <= playerIndex.Count; i++)
        {

            if (finalNumber == playerIndex[i])
            {
                playerIndex[i] = 0; //setting the space to empty
                break;
            }
        }

        Destroy(player);
    }

    private void ScanForControllers() //This function scans for new controllers by keeping an eye if the player has pressed A in any controller
    {
        if (numberOfPlayers < 4) //simple if to not allow more than 4 players on the game
        {
            for (int i = 0; i <= 4; i++) // I = joystick number, not allowing more than 4 
            {
                if (i < JoystickNames.Length) //the I goes further than the lenght, so this IF stops some errors
                {
                    if (JoystickNames[i].Contains("360"))
                    {
                        if (Input.GetKeyDown("joystick " + (i + 1) + " button 7")) //Xbox 360 controller
                        {
                            AddPlayer((i + 1), "X360");
                        } else if (Input.GetKeyDown("joystick " + (i + 1) + " button 6") && playerIndex[0] == 5) //meaning that the first position is occupied by a PC
                        {
                            RemovePlayer(1);
                            AddPlayer((i + 1), "X360");
                        }
                    }
                    else if (JoystickNames[i].Contains("Wireless Controller")) //PS4 controller
                    {
                        if (Input.GetKeyDown("joystick " + (i + 1) + " button 9"))
                        {
                            AddPlayer(i + 1, "PS4");
                        } else if (Input.GetKeyDown("joystick " + (i + 1) + " button 8") && playerIndex[0] == 5) //meaning that the first position is occupied by a PC
                        {
                            RemovePlayer(1);
                            AddPlayer((i + 1), "PS4");
                        }
                    }
                }

            } // end of I for loop

        }
    }

    private void AddPC()
    {
        GameObject player = Instantiate(playerPrefabForSpawing, transform.position, Quaternion.identity);
        numberOfPlayers++;
        playerNumber++;

        if (playerIndex.Count == 0)
        {
            playerIndex.Add(5);
        } else
        {
            playerIndex[0] = 5;
        }
        player.name = "Player" + 1;
        player.GetComponent<Player>().playerNumber = 1;
        player.GetComponent<Player>().horizontal = "PCH5";
        player.GetComponent<Player>().vertical = "PCV5";
        player.GetComponent<Player>().Rhorizontal = "";
        player.GetComponent<Player>().Rvertical = "";

    }

    public void Dropout() // if the player hit SELECT
    {

    }

    private void Update()
    {
        //Placing a PC if there isn't any controllers connected
        if(numberOfPlayers == 0 || playerIndex[0] == 0)
        {
            AddPC();
        } 

        JoystickNames = Input.GetJoystickNames();
        ScanForControllers();

        if (Input.GetKeyDown(KeyCode.B))
        {
            
            for (int i = 0; i < playerIndex.Count; i++)
            {
                print(playerIndex[i]);
            }
        }

        if (Input.GetKeyDown(KeyCode.T))
        {
            RemovePlayer(2);
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            RemovePlayer(1);
        }


    }



}