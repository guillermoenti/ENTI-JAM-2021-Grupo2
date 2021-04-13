using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollController : MonoBehaviour
{
    Rigidbody2D rigidBody;

    int speed;

    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();
    }

    // Start is called before the first frame update
    void Start()
    {
        //speed = 
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.Rotate(0, 0, -0.2f);
    }
}
