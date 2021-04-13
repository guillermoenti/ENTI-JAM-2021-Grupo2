using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UmbrellaController : MonoBehaviour
{
    Animator animator;

    bool a_getHit;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        animator.SetBool("getHit", a_getHit);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Monkey")
        {
            a_getHit = true;
        }
    }

    private void SetIddle()
    {
        a_getHit = false;
    }
}
