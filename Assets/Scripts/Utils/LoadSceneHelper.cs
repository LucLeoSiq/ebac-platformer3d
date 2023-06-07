using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using static Cinemachine.DocumentationSortingAttribute;

public class LoadSceneHelper : MonoBehaviour
{
    private string _path = Application.streamingAssetsPath + "/save.txt";

    public void LoadNewGame(int level)
    {
        File.Delete(_path);
        SceneManager.LoadScene(level);
    }
    
    public void LoadLevel(int level)
    {
        SceneManager.LoadScene(level);
        SaveManager.Instance.Load();
        Debug.Log("Level Loaded");
    }
}
    