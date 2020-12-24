using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class deleteExplosion : MonoBehaviour {



	// Use this for initialization
	void Start () {
        StartCoroutine(delete());
	}
	IEnumerator delete()
    {
        yield return new WaitForSeconds(0.6f);
        Destroy(gameObject);
    }
}
