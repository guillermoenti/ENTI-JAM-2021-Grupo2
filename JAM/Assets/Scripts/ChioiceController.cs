using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChioiceController : MonoBehaviour
{
    Animator animator;
    bool isSelected = false;

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
        animator.SetBool("isSelected", isSelected);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Control")
        {
            isSelected = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Control")
        {
            isSelected = false;
        }
    }
}
