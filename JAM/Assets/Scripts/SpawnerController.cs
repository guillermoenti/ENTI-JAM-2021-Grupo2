using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerController : MonoBehaviour
{
    [SerializeField] GameObject[] Mensajes;
    [SerializeField] Transform mainCamera;

    GameObject Mensaje;

    BoxCollider2D boxCollider;

    float timer;

    // Start is called before the first frame update
    void Start()
    {
        timer = 0;
    }

    // Update is called once per frame
    void Update()
    {

        timer += Time.deltaTime;
        if (timer > 3)
        {
            Destroy(Mensaje);
            Destroy(this.gameObject);
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "CameraManager")
        {
            int rand = Random.Range(0, 3);
            Mensaje = Instantiate(Mensajes[rand], new Vector3(transform.position.x - 150, transform.position.y, transform.position.z), transform.rotation, mainCamera);
            
        }
    }
}
