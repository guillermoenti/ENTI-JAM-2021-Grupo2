using UnityEngine;

public class SectionManager : MonoBehaviour
{
    [Header("Game Objects")]
    [SerializeField] GameObject grid;
    [SerializeField] GameObject section1_1;
    [SerializeField] GameObject section1_2;
    [SerializeField] GameObject section1_3;

    GameObject lastSection;

    /*Transform section1_1_tr;
    Transform section1_2_tr;
    Transform section1_3_tr;*/

    CompositeCollider2D lastSectionCll;

    float count;

    /*CompositeCollider2D section1_1_c2d;
    CompositeCollider2D section1_2_c2d;
    CompositeCollider2D section1_3_c2d;*/

    private void Awake()
    {
        /*section1_1_tr = section1_1.GetComponent<Transform>();
        section1_2_tr = section1_2.GetComponent<Transform>();
        section1_3_tr = section1_3.GetComponent<Transform>();

        section1_1_c2d = section1_1.GetComponent<CompositeCollider2D>();
        section1_1_c2d = section1_2.GetComponent<CompositeCollider2D>();
        section1_1_c2d = section1_3.GetComponent<CompositeCollider2D>();*/
    }

    // Start is called before the first frame update
    void Start()
    {
        /*section1_1_tr.position = new Vector3(0, 0, 0);
        section1_2_tr.position = new Vector3(1536, 0, 0);
        section1_3_tr.position = new Vector3(3072, 0, 0);

        rightPos = collider2d.bounds.max.x;*/
        count = 0;
        lastSection = Instantiate(section1_1, new Vector3(0, 0, 0), Quaternion.Euler(0,0,0), grid.transform);
    }

    // Update is called once per frame
    void Update()
    {
        count += Time.deltaTime;
        if (count > 1)
        {
            lastSection = Instantiate(ChooseNextSection(), new Vector3(lastSection.GetComponent<CompositeCollider2D>().bounds.max.x + 576, 0, 0), lastSection.transform.rotation, grid.transform);
            count = 0;
        }
    }

    GameObject ChooseNextSection()
    {
        int randSection = Random.Range(1, 4);

        switch (randSection)
        {
            case 1:
                return section1_1;
            case 2:
                return section1_2;
            case 3:
                return section1_3;
            default:
                return null;
        }
    }

}
