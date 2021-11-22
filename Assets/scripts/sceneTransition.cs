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
        else
        {
            SceneManager.LoadScene("TestLevel");
        }
    }
}
