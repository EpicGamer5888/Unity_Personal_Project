using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;

public class SaveSystemCode 
{
    private static SaveData _saveData = new SaveData();

    [System.Serializable]
    public struct SaveData
    {
        public PlayerSaveData PlayerData;
    }

    public static string SaveFileName()
    {
        string savefile = Application.persistentDataPath +"/save" + ".save";
        return savefile;
    }
    public static void Save()
    {
        HandleSaveData();

        File.WriteAllText(SaveFileName(), JsonUtility.ToJson(_saveData, true));
    }

    private static void HandleSaveData()
    {
        GameManager.Instance.player.Save( ref _saveData.PlayerData);
    }

    public static void Load()
    {
        string saveContent = File.ReadAllText(SaveFileName());

        _saveData = JsonUtility.FromJson<SaveData>(saveContent);
        HandleLoadData();
    }

    private static void HandleLoadData()
    {
        GameManager.Instance.player.Load(_saveData.PlayerData);
    }
}

public class SaveSystem : MonoBehaviour
{
    public int scene;
    public bool loading;
    private static SaveSystem instance;

    private void Start()
    {
        if (instance == null)
            DontDestroyOnLoad(this);
        else Destroy(gameObject);
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += onSceneLoad;
    }

    void onSceneLoad(Scene scene, LoadSceneMode mode)
    {
        if (loading)
            SaveSystemCode.Load();
        loading = false;
    }

    public void Load()
    {
        loading = true;
        scene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(scene);
    }
}