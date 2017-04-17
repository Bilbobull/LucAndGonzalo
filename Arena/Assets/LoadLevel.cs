using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadLevel : MonoBehaviour {

    public string LevelToLoad;

    public void DoLoadLevel()
    {
        SceneManager.LoadScene(LevelToLoad);
    }

}
