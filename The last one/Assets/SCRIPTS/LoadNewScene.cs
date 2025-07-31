using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadNewScene : MonoBehaviour
{
    // Start is called before the first frame update
    public void LoadSceneByName(string sceneName)
    {
        SceneManager.LoadScene("Scan");
    }
    public void TheLoadSceneByName(string sceneName)
    {
        SceneManager.LoadScene("Glossary");
    }
    public void BackSceneByName(string sceneName)
    {
        SceneManager.LoadScene("MainUI");
    }
    public void BackSceneToHome(string sceneName)
    {
        SceneManager.LoadScene("MainUI");
    }

   

}