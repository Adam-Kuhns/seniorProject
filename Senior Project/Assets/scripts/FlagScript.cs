using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FlagScript : MonoBehaviour
{
    private Animator m_Animator;

    // Start is called before the first frame update
    void Start()
    {
        m_Animator = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D collider)
    {

        if (collider.gameObject.tag == "Player" && GameObject.Find("boss") == null)
        {
            m_Animator.SetTrigger("flagLower");
        }
    }

    public void LoadNextLevel()
    {
        SceneManager.LoadScene(sceneName: "Level2");
    }
}
