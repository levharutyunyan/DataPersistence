using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class GameInfo : MonoBehaviour
{

    [System.Serializable]
    public class SaveData
    {
        public string BestName;
        public int BestScore;

        public string CurrentName;
        public int CurrentScore;

        public void UpdateBestScore()
        {
            if(CurrentScore > BestScore)
            {
                BestScore = CurrentScore;
                BestName = CurrentName;
            }
        }

        public void ResetCurrentPlayer(string name)
        {
            CurrentName = name;
            CurrentScore = 0;
            if(BestName == null)
            {
                BestName = CurrentName;
                BestScore = CurrentScore;
            }
        }
    }
    public static GameInfo Instance { get; private set; }
    public SaveData Data;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            Load();
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void Load()
    {
        string filename = Application.persistentDataPath + "/savefile.json";
        if(File.Exists(filename))
        {
            string json = File.ReadAllText(filename);
            Data = JsonUtility.FromJson<SaveData>(json);
        }
    }

    public void Save()
    {
        string filename = Application.persistentDataPath + "/savefile.json";
        string json = JsonUtility.ToJson(Data);
        File.WriteAllText(filename, json);
    }

    public void ResetPlayer(string playerName)
    {
        Data.CurrentName = playerName;
        Data.BestScore = 0;
        Save();
    }

    public void OnApplicationQuit()
    {
        Save();
    }
}
