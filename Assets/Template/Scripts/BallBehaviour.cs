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

    public Slider Powerslider;



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
        rb.isKinematic = true;
        angle = minAngle;
        force = minForce;
        initPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Powerslider.value = force / maxForce;
        switch (state)
        {
            case 0:
                time += Time.deltaTime;
                angle = Mathf.Sin(time * timeAngleLoop / 2 * Mathf.PI) * (maxAngle - minAngle) + minAngle;
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    state++;
                    time = 0;
                }
                break;
            case 1:
                time += Time.deltaTime;
                force = Mathf.Sin(time * timeForceLoop / 2 * Mathf.PI) * (maxForce - minForce) + minForce;
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    state++;
                    launch = true;
                }
                break;
            case 2:
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    state = 0;
                    time = 0;
                    rb.isKinematic = true;
                    transform.position = initPos;
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
}
