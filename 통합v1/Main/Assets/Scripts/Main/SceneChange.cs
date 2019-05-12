using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class SceneChange : MonoBehaviour
{
    public void SceneChangeToMain()
    {
        SceneManager.LoadScene("Main");
    }
    public void SceneChangeToBasic()
    {
        SceneManager.LoadScene("Basic");
    }
    public void SceneChangeToMath()
    {
        SceneManager.LoadScene("Math");
    }
    public void SceneChangeToEnglish()
    {
        SceneManager.LoadScene("English");
    }
}


