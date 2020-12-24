using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    ///// singleton
    public static GameManager instance;

    public bool inMatch = false;

    private void Start()
    {

        /////// Singleton
        if (instance != null)
        {
            Destroy(gameObject);
        } else
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
       
        

    }

    private void Update()
    {
        
    }


}
