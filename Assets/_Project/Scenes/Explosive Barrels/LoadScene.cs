using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{    
    public void LoadSceneNow(string sceneToLoad)
    {
        SceneManager.LoadScene(sceneToLoad);
    }
}
