using UnityEngine;

public class SectionManager : MonoBehaviour
{
    public static SectionManager SInstance { get; private set; }

    [Header("Game Objects")]
    [SerializeField] GameObject grid;
    [SerializeField] GameObject section1_1;
    [SerializeField] GameObject section1_2;
    [SerializeField] GameObject section1_3;

    GameObject lastSection;

    CompositeCollider2D lastSectionCll;

    float count;

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
        count = 0;
        Instantiate(section1_1, new Vector3(0, 0, 0), Quaternion.Euler(0, 0, 0), grid.transform);
        Instantiate(section1_2, new Vector3(2304, 0, 0), Quaternion.Euler(0, 0, 0), grid.transform);
        lastSection = Instantiate(section1_3, new Vector3(4608, 0, 0), Quaternion.Euler(0,0,0), grid.transform);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void InstantiateSection()
    {
        lastSection = Instantiate(ChooseNextSection(), new Vector3(lastSection.GetComponent<CompositeCollider2D>().bounds.max.x + 576, 0, 0),
                                  lastSection.transform.rotation, grid.transform);
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
