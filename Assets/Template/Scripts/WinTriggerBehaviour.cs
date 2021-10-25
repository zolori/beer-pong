using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinTriggerBehaviour : MonoBehaviour
{
    public GameObject winScreen;

    // Start is called before the first frame update
    void Start()
    {
        winScreen.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        winScreen.SetActive(true);
    }
}
