using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class BallBehaviour : MonoBehaviour
{
    private Rigidbody2D rb;
    public float minAngle;
    public float maxAngle;
    public float timeAngleLoop;
    public float minForce;
    public float maxForce;
    public float timeForceLoop;
    public GameObject VectorDisplayer;

    // input flow bool
    private short state = 0; // 0 - aiming angle ; 1 - aiming force ; 2 - has been launched
    private bool launch = false;

    private float time = 0;
    private float angle;
    private float force;
    private Vector3 initPos;

    void Awake()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    // Start is called before the first frame update
    void Start()
    {
        initPos = transform.position;
        init();
    }

    void init()
    {
        state = 0;
        time = 0;
        angle = minAngle;
        force = minForce;
        rb.isKinematic = true;
        rb.velocity = Vector2.zero;
        transform.position = initPos;
        VectorDisplayer.SetActive(true);
        VectorDisplayer.transform.rotation = Quaternion.Euler(0, 0, angle);
        VectorDisplayer.transform.localScale = Vector2.right * force + Vector2.up;
    }

    // Update is called once per frame
    void Update()
    {
        switch (state)
        {
            case 0:
                time += Time.deltaTime;
                angle = (Mathf.Sin(time * (2 * Mathf.PI) / timeAngleLoop) + 1) / 2 * (maxAngle - minAngle) + minAngle;
                VectorDisplayer.transform.rotation = Quaternion.Euler(0, 0, angle);
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    state++;
                    time = 0;
                }
                break;
            case 1:
                time += Time.deltaTime;
                force = (Mathf.Sin(time * (2 * Mathf.PI) / timeForceLoop) + 1) /2 * (maxForce - minForce) + minForce;
                VectorDisplayer.transform.localScale = Vector2.right * force + Vector2.up;
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    state++;
                    launch = true;
                    VectorDisplayer.SetActive(false);
                }
                break;
            case 2:
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    init();
                }
                break;
        }

        if (force > maxForce)
        {
            force = maxForce;
        }
    }

    void FixedUpdate()
    {
        if (state == 2 && launch)
        {
            rb.isKinematic = false;
            rb.AddForce(force * new Vector2(Mathf.Cos(angle * 2 * Mathf.PI / 360), Mathf.Sin(angle * 2 * Mathf.PI / 360)), ForceMode2D.Impulse);
            launch = false;
        }
    }

    public float GetAngle()
    {
        return angle;
    }

    public float GetForce()
    {
        return force;
    }

    public int GetState()
    {
        return state;
    }
}
