using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FTUE : MonoBehaviour
{
    // Start is called before the first frame update

    private void Start()
    {
        // stop all time in the scene
        Time.timeScale = 0f;
    }

    // Update is called once per frame
    void Update()
    {
       // if any key is pressed, resume scale time to 1f and destroy the gameobject
        if (Input.anyKeyDown)
        {
            Time.timeScale = 1f;
            Destroy(gameObject);
        }
       
    }
}
