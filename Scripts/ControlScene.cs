using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ControlScene : MonoBehaviour {

    private void Update()
    {
        StartCoroutine(GameScene());
    }


    IEnumerator GameScene()
    {
        yield return new WaitForSeconds(6);
        SceneManager.LoadScene("Game");
    }

}
