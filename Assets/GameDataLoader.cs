using System.IO;
using UnityEngine;

public class GameDataLoader : MonoBehaviour
{
    public static GameDataLoader Instance { get; private set; }

    public string jsonFilePath = "doofus_diary.json";
    public GameData gameData;

    private void Awake()
    {
        // Implementing the Singleton pattern
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Optional: Keeps this object alive across scenes
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        LoadGameData();
    }

    void LoadGameData()
    {
        string filePath = Path.Combine(Application.streamingAssetsPath, jsonFilePath);
        if (File.Exists(filePath))
        {
            string jsonContent = File.ReadAllText(filePath);
            gameData = JsonUtility.FromJson<GameData>(jsonContent);
            ApplyGameData();
        }
        else
        {
            Debug.LogError("File not found: " + filePath);
        }
    }

    void ApplyGameData()
    {
        // Example of applying the data
        Debug.Log("Player Speed: " + gameData.player_data.speed);
        Debug.Log("Min Pulpit Destroy Time: " + gameData.pulpit_data.min_pulpit_destroy_time);
        Debug.Log("Max Pulpit Destroy Time: " + gameData.pulpit_data.max_pulpit_destroy_time);
        Debug.Log("Pulpit Spawn Time: " + gameData.pulpit_data.pulpit_spawn_time);

        // Here you would apply the data to your game objects, e.g.:
        // player.speed = gameData.player_data.speed;
    }
}

[System.Serializable]
public class PlayerData
{
    public float speed;
}

[System.Serializable]
public class PulpitData
{
    public float min_pulpit_destroy_time;
    public float max_pulpit_destroy_time;
    public float pulpit_spawn_time;
}

[System.Serializable]
public class GameData
{
    public PlayerData player_data;
    public PulpitData pulpit_data;
}
