using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonsManager : MonoBehaviour {

    public GameObject PlayerTres;
    public GameObject PlayerQuatro;

    public void IniciaJogoDois()
    {
        SceneManager.LoadScene("Instrucoes");
        PlayerController.PlayerControllerInstance.numberOfPlayers = 2;
    }

    public void IniciaJogoTres()
    {
        SceneManager.LoadScene("Instrucoes");
        PlayerController.PlayerControllerInstance.numberOfPlayers = 3;
    }


    public void IniciaJogoQuatro()
    {
        SceneManager.LoadScene("Instrucoes");
        PlayerController.PlayerControllerInstance.numberOfPlayers = 4;
    }
        
    
}
