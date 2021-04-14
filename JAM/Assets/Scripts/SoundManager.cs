using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SoundManager : MonoBehaviour
{
    public static SoundManager SoInstance { get; private set; }

    public static AudioClip Clicksound, Crashsound, Burnedsound, TimeUpsound, TimeDownsound, Dashsound;

    static AudioSource audiosrc;


    void Awake()
    {
        if (SoInstance == null)
        {
            SoInstance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Debug.Log("Warning: multiple " + this + " in scene!!");
        }

    }
    // Start is called before the first frame update
    void Start()
    {
        audiosrc = GetComponent<AudioSource>();

        Clicksound = Resources.Load<AudioClip>("ClickMenu");
        Crashsound = Resources.Load<AudioClip>("Crashed");
        Burnedsound = Resources.Load<AudioClip>("Burned");
        TimeUpsound = Resources.Load<AudioClip>("Banana");
        TimeDownsound = Resources.Load<AudioClip>("Peel");
        Dashsound = Resources.Load<AudioClip>("Dash");

    }

    // Update is called once per frame
    void Update()
    {
    }

    public static void PlaySound(string clip)
    {
        switch (clip)
        {
            case "Clicksound":
                audiosrc.PlayOneShot(Clicksound);
                break;
            case "Crashsound":
                audiosrc.PlayOneShot(Crashsound);
                break;
            case "Burnedsound":
                audiosrc.PlayOneShot(Burnedsound);
                break;
            case "TimeUpsound":
                audiosrc.PlayOneShot(TimeUpsound);
                break;
            case "TimeDownsound":
                audiosrc.PlayOneShot(TimeDownsound);
                break;
            case "Dashsound":
                audiosrc.PlayOneShot(Dashsound);
                break;

        }

    }
}
