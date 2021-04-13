using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxController : MonoBehaviour
{
    float length, startpos;
    [SerializeField] GameObject cam;
    [SerializeField] float parallaxEffect;

    private void Awake()
    {
        length = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    // Start is called before the first frame update
    void Start()
    {
        startpos = transform.position.x;
    }

    private void FixedUpdate()
    {
        float temp = (cam.transform.position.x * (1 - parallaxEffect));
        float distance = (cam.transform.position.x * parallaxEffect);

        

        if (temp > startpos + length) startpos += length;
        else if (temp < startpos - length) startpos -= length;

        transform.position = new Vector3(startpos + distance, transform.position.y, transform.position.z);
    } 
}
