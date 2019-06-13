using UnityEngine;
using UnityEngine.SceneManagement;


public class ForceStart : MonoBehaviour
{
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    static void FirstLoad()
    {
        if (SceneManager.GetActiveScene().name.CompareTo("Main") != 0)
        {
            //SceneManager.LoadScene("Main");
        }
    }
}