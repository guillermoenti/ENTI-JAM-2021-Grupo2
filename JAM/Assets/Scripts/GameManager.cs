using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; //No necesario
using System.IO;


public class GameManager : MonoBehaviour
{
    public static GameManager GInstance { get; private set; }
    [SerializeField] Transform player;

    public bool gameIsRunning;
    public float timeRemaining = 100;
    private bool timerIsRunning;
    public float metersRunned;

    private List<int> records;


    private void Awake()
    {
        if(GInstance == null)
        {
            GInstance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Debug.Log("Warining: DisallowMultipleComponent " + this + " in scene!");
        }
        BinaryReader();
        gameIsRunning = true;
    }

    // Start is called before the first frame update
    void Start()
    {
        metersRunned = 0000;
        timerIsRunning = true;

    }

    void Update()
    {
        if (gameIsRunning)
        {
            if (timerIsRunning)
            {
                if (timeRemaining > 0)
                {
                    timeRemaining -= Time.deltaTime;
                    metersRunned += Time.deltaTime;
                }
                else
                {
                    Debug.Log("Time finished");
                    timeRemaining = 0;
                    timerIsRunning = false;
                }
            }
        }
    }

    public void BinaryWriter()
    {
        BinaryWriter writer = new BinaryWriter(File.Open(".Save/save.sav", FileMode.Create));
        for(int i = 0; i < 3; i++)
        {
            writer.Write(records[i]);
        }
        writer.Close();
    }

    private void BinaryReader()
    {
        BinaryReader reader;
        if (File.Exists("/.Save/save.sav"))
        {
            reader = new BinaryReader(File.Open(".Save/save.sav", FileMode.Open));
        }
        else
        {
            return;
        }
        for(int i = 0; i < 3; i++)
        {
            int score = reader.ReadInt32();
            records.Add(score);
        }
        reader.Close();
    }

    private void SortAndPruge(ref List<int> _list)
    {
        _list.Add(Mathf.FloorToInt(metersRunned / 60));
        _list.Sort();
        _list.RemoveAt(0);
        _list.Reverse();

    }

    public bool GetTimerIsRunning()
    {
        return timerIsRunning;
    }

    public void AddMetersOnDash()
    {
        metersRunned += 5;
    }

}
