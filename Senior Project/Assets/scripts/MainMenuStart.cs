using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuStart : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void LoadGame()
    {
        SceneManager.LoadScene("Level1");
    }

    public void LoadPrologue()
    {
        SceneManager.LoadScene("Prologue");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
