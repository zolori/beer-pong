using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VectorDisplayer : MonoBehaviour
{
    public GameObject ball;
    private BallBehaviour ballBehaviour;

    void Awake()
    {
        ballBehaviour = ball.GetComponent<BallBehaviour>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
