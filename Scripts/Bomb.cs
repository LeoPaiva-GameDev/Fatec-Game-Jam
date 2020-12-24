using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour {

    [SerializeField] private float bombThrowForce;
    [SerializeField] private float bombThrowForceUp;
    public bool bombThrew;
    [SerializeField] private GameObject fxPrefabs;

    // Use this for initialization
    void Start () {
      
	}

    private void Update()
    {
        if (bombThrew)
        {
            bombThrew = false;
            gameObject.transform.SetParent(null);
            StartCoroutine(kaboom());
        }
    }

    // Update is called once per frame


    IEnumerator kaboom()
    {
        yield return new WaitForSeconds(3);
        GameObject explosao = Instantiate(fxPrefabs, transform.position, Quaternion.identity); 
        if(gameObject.tag == "BombaAzul")
        {
            explosao.tag = "EAzul";
        }
        if (gameObject.tag == "BombaVerde")
        {
            explosao.tag = "EVerde";
        }
        if (gameObject.tag == "BombaLaranja")
        {
            explosao.tag = "ELaranja";
        }

        Destroy(gameObject);
    }

    public void ThrowBomb()
    {
        gameObject.GetComponent<Rigidbody>().useGravity = true;
        gameObject.GetComponent<Rigidbody>().isKinematic = false;
        StartCoroutine(throwingBomb());
    }

    IEnumerator throwingBomb()
    {
        for (int i = 0; i < 10; i++)
        {
            gameObject.GetComponent<Rigidbody>().AddForce(0, bombThrowForceUp, 0);
            yield return new WaitForSeconds(0.01f);
        }
        gameObject.GetComponent<Rigidbody>().AddForce(transform.forward * -(bombThrowForce), ForceMode.Impulse);

        yield return new WaitForSeconds(0.2f);
        gameObject.GetComponent<SphereCollider>().isTrigger = false;
    }
}
