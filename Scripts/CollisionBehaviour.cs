using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionBehaviour : MonoBehaviour {

    [SerializeField] private Transform respawn;
    private Player script;

    private void Start()
    {
        script = gameObject.GetComponentInParent<Player>();
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.tag == "EVerde" && gameObject.tag == "Verde")
        {
            script.playerLives -= 1;
        }


        if (other.gameObject.tag == "ELaranja" && gameObject.tag == "Laranja")
        {
            script.playerLives -= 1;
        }


        if (other.gameObject.tag == "EAzul" && gameObject.tag == "Azul")
        {
            script.playerLives -= 1;
        }
        
        if (other.gameObject.tag == "End")
        {
            script.playerLives -= 1;
        }

        

    }


}
