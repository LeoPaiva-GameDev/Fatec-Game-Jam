using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Steamworks;

public class testSteamName : MonoBehaviour
{
    private TextMeshProUGUI textTest;

    void Start()
    {
        textTest = GetComponent<TextMeshProUGUI>();
        if (SteamManager.Initialized)
        {
            textTest.text = SteamFriends.GetPersonaName();
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
