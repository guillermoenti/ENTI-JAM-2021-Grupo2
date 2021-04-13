using UnityEngine;

public class SectionManager : MonoBehaviour
{
    public static SectionManager SInstance { get; private set; }

    [Header("Game Objects")]
    [SerializeField] GameObject grid;
    /*[SerializeField] struct section
    {
        GameObject sectionObject;
        int ID_left;
        int ID_right;
    };
    [SerializeField] section[] sections;*/
    [SerializeField] GameObject[] sections0;
    [SerializeField] GameObject[] sections1;
    [SerializeField] GameObject[] sections2;
    [SerializeField] GameObject[] sections3;

    GameObject lastSection;

    CompositeCollider2D lastSectionCll;

    [SerializeField] bool startTutorial;

    //float count;

    private void Awake()
    {
        if (SInstance == null)
        {
            SInstance = this;
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
        //count = 0;
        if (startTutorial)
        {
            Instantiate(sections0[0], new Vector3(0, -490, 0), Quaternion.Euler(0, 0, 0), grid.transform);
            Instantiate(sections0[1], new Vector3(2304, -490, 0), Quaternion.Euler(0, 0, 0), grid.transform);
            Instantiate(sections0[2], new Vector3(4608, -490, 0), Quaternion.Euler(0, 0, 0), grid.transform);
            Instantiate(sections0[3], new Vector3(6912, -490, 0), Quaternion.Euler(0, 0, 0), grid.transform);
            Instantiate(sections0[4], new Vector3(9216, -490, 0), Quaternion.Euler(0, 0, 0), grid.transform);
            Instantiate(sections0[5], new Vector3(11520, -490, 0), Quaternion.Euler(0, 0, 0), grid.transform);
            lastSection = Instantiate(sections0[6], new Vector3(13824, -490, 0), Quaternion.Euler(0, 0, 0), grid.transform);
        }
        else
        {
            Instantiate(sections0[0], new Vector3(0, -490, 0), Quaternion.Euler(0, 0, 0), grid.transform);
            lastSection = Instantiate(sections0[1], new Vector3(2304, -490, 0), Quaternion.Euler(0, 0, 0), grid.transform);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void InstantiateSection()
    {
        lastSection = Instantiate(ChooseNextSection(), new Vector3(lastSection.GetComponent<Transform>().position.x + 2304, -490, 0),
                                  lastSection.transform.rotation, grid.transform);
    }

    GameObject ChooseNextSection()
    {
        int randSection = Random.Range(0, sections1.Length);

        return sections1[randSection];
    }

}
