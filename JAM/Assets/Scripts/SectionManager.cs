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
    [SerializeField] GameObject firstSection;
    [SerializeField] GameObject[] sections;

    GameObject lastSection;

    CompositeCollider2D lastSectionCll;

    [SerializeField] bool startTutorial;

    private int[] lastsSectionsIDs;

    public int counter;

    private bool instanciate;
    private int tenInstaniates;


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
        lastsSectionsIDs = new int[2];
        counter = 0;
        instanciate = false;
        tenInstaniates = 10;
    }

    // Start is called before the first frame update
    void Start()
    {
        //count = 0;
        if (startTutorial)
        {
            Instantiate(firstSection, new Vector3(0, -490, 0), Quaternion.Euler(0, 0, 0), grid.transform);
            Instantiate(sections[0], new Vector3(2304, -490, 0), Quaternion.Euler(0, 0, 0), grid.transform);
            Instantiate(sections[1], new Vector3(4608, -490, 0), Quaternion.Euler(0, 0, 0), grid.transform);
            Instantiate(sections[2], new Vector3(6912, -490, 0), Quaternion.Euler(0, 0, 0), grid.transform);
            Instantiate(sections[3], new Vector3(9216, -490, 0), Quaternion.Euler(0, 0, 0), grid.transform);
            Instantiate(sections[4], new Vector3(11520, -490, 0), Quaternion.Euler(0, 0, 0), grid.transform);
            Instantiate(sections[5], new Vector3(13824, -490, 0), Quaternion.Euler(0, 0, 0), grid.transform);
            lastSection = Instantiate(sections[6], new Vector3(16128, -490, 0), Quaternion.Euler(0, 0, 0), grid.transform);
        }
        else
        {
            Instantiate(sections[0], new Vector3(0, -490, 0), Quaternion.Euler(0, 0, 0), grid.transform);
            lastSection = Instantiate(sections[1], new Vector3(2304, -490, 0), Quaternion.Euler(0, 0, 0), grid.transform);
            
        }
        lastsSectionsIDs[0] = 6;
        lastsSectionsIDs[1] = sections[5].GetComponent<Section>().sectionID;
    }

    // Update is called once per frame
    void Update()
    {
        if (tenInstaniates <= 0)
        {
            counter = 0;
            instanciate = false;
            tenInstaniates = 10;
        }
        if (instanciate)
        {
            lastsSectionsIDs[0] = lastsSectionsIDs[1];
            GameObject test = ChooseNextSection();
            if (test == null)
            {
                return;
            }
            lastSection = Instantiate(test, new Vector3(lastSection.GetComponent<Transform>().position.x + 2304, -490, 0),
                                      lastSection.transform.rotation, grid.transform);
            lastsSectionsIDs[1] = lastSection.GetComponent<Section>().sectionID;

            tenInstaniates--;
        }
        
    }

    public void InstantiateSection()
    {
        if(counter >= 5)
        {

            instanciate = true;
        }
        
    }

    GameObject ChooseNextSection()
    {
        bool validSection = false;
        int randSection = -1;

        randSection = Random.Range(0, sections.Length);
        Section actualSection = sections[randSection].GetComponent<Section>();
        if(lastsSectionsIDs[0] != actualSection.sectionID || lastsSectionsIDs[1] != actualSection.sectionID)
        {
            validSection = true;
        }

        if (validSection)
        {
            return sections[randSection];
        }
        return null;
    }

}
