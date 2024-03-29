using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ebac.Core.Singleton;
using System;
using UnityEngine.SceneManagement;

public class SaveManager : Singleton<SaveManager>
{
    [SerializeField] private SaveSetup _saveSetup;
    private string _path = Application.streamingAssetsPath + "/save.txt";

    public int lastLevel;
    public int lastCheckpoint;

    public Action<SaveSetup> FileLoaded;

    public SaveManager() : base(true) { }

    public SaveSetup Setup
    {
        get { return _saveSetup; }
    }

    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(gameObject);
    }

    private void CreateNewSave()
    {
        _saveSetup = new SaveSetup();
        _saveSetup.lastLevel = 1;
        _saveSetup.playerName = "Rafael";
    }

    private void Start()
    {
        //Invoke(nameof(Load), .1f);
    }

    [NaughtyAttributes.Button]
    private void Save()
    {
        string setupToJson = JsonUtility.ToJson(_saveSetup, true);
        Debug.Log(setupToJson);
        SaveFile(setupToJson);
    }

    public void SaveItems()
    {
        _saveSetup.coins = Items.ItemManager.Instance.GetItemByType(Items.ItemType.COIN).soInt.value;
        _saveSetup.health = Items.ItemManager.Instance.GetItemByType(Items.ItemType.LIFE_PACK).soInt.value;
        Save();
    }



    public void SaveName(string text)
    {
        _saveSetup.playerName = text;
        Save();
    }

    public void SaveLastLevel(int level)
    {
        _saveSetup.lastLevel = level;
        SaveItems();
        Save();
    }

    public void SaveLastCheckpoint(int checkpointKey)
    {
        _saveSetup.lastCheckpoint = checkpointKey;
        SaveItems();
        Save();
    }

    private void SaveFile(string json)
    {
       Debug.Log(_path);
       File.WriteAllText(_path, json);
    }

    [NaughtyAttributes.Button]
    public void Load()
    {
        string fileLoaded = "";

        if (File.Exists(_path))
        {
            fileLoaded = File.ReadAllText(_path);
            _saveSetup = JsonUtility.FromJson<SaveSetup>(fileLoaded);

            lastLevel = _saveSetup.lastLevel;
            lastCheckpoint = _saveSetup.lastCheckpoint;

            if (SceneManager.GetActiveScene().buildIndex == 1)
            {
                CheckpointManager.Instance.SaveCheckPoint(lastCheckpoint);
                Player.Instance.Respawn();
            }
            else
            {
                SceneManager.sceneLoaded += GameSceneLoadedCallback;
            }
        }
        else
        {
            CreateNewSave();
            Save();
        }
        
        FileLoaded?.Invoke(_saveSetup);
    }

    private void GameSceneLoadedCallback(Scene loadedScene, LoadSceneMode loadSceneMode)
    {
        if(loadedScene.buildIndex == 1)
        {
            CheckpointManager.Instance.SaveCheckPoint(lastCheckpoint);
            Player.Instance.Respawn();
            SceneManager.sceneLoaded -= GameSceneLoadedCallback;
        }
    }


    [System.Serializable]
    public class SaveSetup
    {
        public int lastLevel;
        public int lastCheckpoint;
        
        public float coins;
        public float health;

        public string playerName;
        
    }
}
