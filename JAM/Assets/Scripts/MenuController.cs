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
        gameObject.transform.position = new Vector2(215, 90);
        menuoptions = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(upButton))
        {
            menuoptions--;

            if (menuoptions < 0) { menuoptions = 3; }

        }
        else if (Input.GetKeyDown(downButton))
        {
            menuoptions++;

            if (menuoptions > 3) { menuoptions = 0; }

        }
        else if (Input.GetKeyDown(select))
        {
            switch (menuoptions)
            {
                case 0:
                    SceneManager.LoadScene("Game");
                    break;
                case 1:
                    SceneManager.LoadScene("Options");
                    break;
                case 2:
                    SceneManager.LoadScene("Credits");
                    break;
                case 3:
                    Application.Quit();
                    break;
            }

        }
        switch (menuoptions)
        {
            case 0:
                gameObject.transform.position = new Vector2(100, 0);
                break;
            case 1:
                gameObject.transform.position = new Vector2(100, -60);
                break;
            case 2:
                gameObject.transform.position = new Vector2(100, -120);
                break;
            case 3:
                gameObject.transform.position = new Vector2(100, -180);
                break;
        }
    }
}
