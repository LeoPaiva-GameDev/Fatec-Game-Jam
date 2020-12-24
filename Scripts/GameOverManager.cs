using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameOverManager : MonoBehaviour {

    public static GameOverManager game_over;

    [SerializeField] private GameObject hudNormal;
    [SerializeField] private GameObject gameOverCanvas;
    [SerializeField] private TextMeshProUGUI playerText;

    private void Start()
    {
        if(game_over != null)
        {
            Destroy(gameObject);
        } else
        {
            game_over = this;
        }
    }

    public void GameOver(GameObject player)
    {

        hudNormal.SetActive(false);
        if(player.name == "PlayersController")
        {
            playerText.text = "Ninguem";
        } else
        {
            playerText.text = player.name;
        }
        gameOverCanvas.SetActive(true);

    }



}
