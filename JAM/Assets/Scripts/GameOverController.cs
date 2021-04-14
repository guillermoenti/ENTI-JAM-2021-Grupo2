using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverController : MonoBehaviour
{
    int menuoptions;
    KeyCode upButton = KeyCode.UpArrow;
    KeyCode downButton = KeyCode.DownArrow;
    KeyCode select = KeyCode.Space;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.transform.position = new Vector2(0, -100);
        menuoptions = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(upButton))
        {
            SoundManager.PlaySound("Clicksound");
            menuoptions--;

            if (menuoptions < 0) { menuoptions = 1; }

        }
        else if (Input.GetKeyDown(downButton))
        {
            SoundManager.PlaySound("Clicksound");
            menuoptions++;

            if (menuoptions > 1) { menuoptions = 0; }

        }
        else if (Input.GetKeyDown(select))
        {
            SoundManager.PlaySound("Clicksound");
            switch (menuoptions)
            {
                case 0:
                    if (SectionManager.SInstance != null && GameManager.GInstance != null)
                    {
                        Destroy(SectionManager.SInstance.gameObject);
                        Destroy(GameManager.GInstance.gameObject);
                    }
                    else if (SoundManager.SoInstance != null)
                    {
                        Destroy(SoundManager.SoInstance.gameObject);
                    }
                    SceneManager.LoadScene("Game");
                    break;
                case 1:
                    Destroy(SoundManager.SoInstance.gameObject);
                    SceneManager.LoadScene("MainMenu");
                    break;
            }

        }
        switch (menuoptions)
        {
            case 0:
                gameObject.transform.position = new Vector2(0, -100);
                break;
            case 1:
                gameObject.transform.position = new Vector2(0, -150);
                break;
        }
    }
}
