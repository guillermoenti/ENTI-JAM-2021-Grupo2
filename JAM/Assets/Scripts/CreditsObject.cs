using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CreditsObject : MonoBehaviour
{
    private Rigidbody2D rigidBody;
    public float speed = 20.5f;
    public bool quiet = false;
    private float esc;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        rigidBody.velocity = -transform.up * speed;
    }

    // Update is called once per frame
    void Update()
    {
        if (quiet)
        {
            rigidBody.velocity = Vector3.zero;
            esc += Time.deltaTime;
            if (esc > 2) { SceneManager.LoadScene("MainMenu"); }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Coco")
        {
            quiet = true;
        }
    }
}
