using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    int menuoptions;
    KeyCode upButton = KeyCode.UpArrow;
    KeyCode downButton = KeyCode.DownArrow;
    KeyCode select = KeyCode.Space;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.transform.position = new Vector2(100, -20);
        menuoptions = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(upButton))
        {
            SoundManager.PlaySound("Clicksound");
            menuoptions--;

            if (menuoptions < 0) { menuoptions = 2; }

        }
        else if (Input.GetKeyDown(downButton))
        {
            SoundManager.PlaySound("Clicksound");
            menuoptions++;

            if (menuoptions > 2) { menuoptions = 0; }

        }
        else if (Input.GetKeyDown(select))
        {
            SoundManager.PlaySound("Clicksound");
            switch (menuoptions)
            {
                case 0:
                    SceneManager.LoadScene("Game");
                    break;
                case 1:
                    Destroy(SoundManager.SoInstance.gameObject);
                    SceneManager.LoadScene("Credits");
                    break;
                case 2:
                    Application.Quit();
                    break;
            }

        }
        switch (menuoptions)
        {
            case 0:
                gameObject.transform.position = new Vector2(100, -20);
                break;
            case 1:
                gameObject.transform.position = new Vector2(100, -100);
                break;
            case 2:
                gameObject.transform.position = new Vector2(100, -180);
                break;
        }
    }
}
