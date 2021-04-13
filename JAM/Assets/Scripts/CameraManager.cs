using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public static CameraManager CInstance { get; private set; }

    [SerializeField] Camera Maincamera;
    [SerializeField] Transform Monkey;

    //Vector3 Camerapos;
    //Vector3 Monkeypos;


    private void Awake()
    {
        if (CInstance == null)
        {
            CInstance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Debug.Log("Warining: DisallowMultipleComponent " + this + " in scene!");
        }

    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        float Monkeypos = Monkey.transform.position.x;
        Maincamera.transform.position = new Vector3 (Monkeypos + 300, 0.0f, -10.0f);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Tilemap")
        {
            Destroy(collision.gameObject);
            //SectionManager.SInstance.InstantiateSection();
        }
    }
}
