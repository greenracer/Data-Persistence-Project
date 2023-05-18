using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class HighScore : MonoBehaviour
{
    public static HighScore Instance;

    public string PlayerName;
    public int PlayerHighScore;

    private void Awake()
    {
        LoadPlayer();
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    [System.Serializable]

    public class SaveData
    {
        public string Name;
        public int Score;
    }

    public void SavePlayer()
    {
        SaveData data = new SaveData();

        data.Name = PlayerName;
        data.Score = PlayerHighScore;

        string json = JsonUtility.ToJson(data);

        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }

    public void LoadPlayer()
    {
        string path = Application.persistentDataPath + "/savefile.json";

        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);

            SaveData data = JsonUtility.FromJson<SaveData>(json);

            PlayerName = data.Name;
            PlayerHighScore = data.Score;
        }
    }
}
