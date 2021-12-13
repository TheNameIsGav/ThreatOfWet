using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class sceneTransition : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (SceneManager.GetActiveScene().name == "WrightTutorialLevel")
        {
            SceneManager.LoadScene("CombatTutorial");
        }
        else if(SceneManager.GetActiveScene().name == "CombatTutorial")
        {
            SceneManager.LoadScene("TestLevel");
        }
        else
        {
            int i = Random.Range(0, 2);
            if (SceneManager.GetActiveScene().name == "TestLevel")
            {
                if(i == 0)
                {
                    SceneManager.LoadScene("CityLevel");
                }
                else
                {
                    SceneManager.LoadScene("SpaceshipLevel");
                }

            }
            else if(SceneManager.GetActiveScene().name == "CityLevel")
            {
                if (i == 0)
                {
                    SceneManager.LoadScene("TestLevel");
                }
                else
                {
                    SceneManager.LoadScene("SpaceshipLevel");
                }
            }
            else
            {
                if (i == 0)
                {
                    SceneManager.LoadScene("CityLevel");
                }
                else
                {
                    SceneManager.LoadScene("TestLevel");
                }
            }
                    
        }
    }
}
