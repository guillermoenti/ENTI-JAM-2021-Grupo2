using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CocoBombController : MonoBehaviour
{
    Animator animator;
    [SerializeField] Transform Monkey;
    int radius = 500;
    bool noChill;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (checkSpikeyPosition()) noChill = true;

        animator.SetBool("noChill", noChill);
    }

    private bool checkSpikeyPosition()
    {
        return Vector2.Distance(this.transform.position, Monkey.transform.position) <= this.radius;
    }

    private void DestroyObject()
    {
        Destroy(gameObject);
    }
}
