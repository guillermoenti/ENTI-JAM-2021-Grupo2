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
        Destroy(SectionManager.SInstance.gameObject);
        Destroy(GameManager.GInstance.gameObject);

        gameObject.transform.position = new Vector2(0, -100);
        menuoptions = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(upButton))
        {
            menuoptions--;

            if (menuoptions < 0) { menuoptions = 1; }

        }
        else if (Input.GetKeyDown(downButton))
        {
            menuoptions++;

            if (menuoptions > 1) { menuoptions = 0; }

        }
        else if (Input.GetKeyDown(select))
        {
            switch (menuoptions)
            {
                case 0:
                    //GameManager.GInstance.metersRunned = 0000;
                    //GameManager.GInstance.timeRemaining = 100;
                    SceneManager.LoadScene("Game");
                    break;
                case 1:
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
