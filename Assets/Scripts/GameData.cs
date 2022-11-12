using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using TMPro;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class GameData : MonoBehaviour
{
    public static GameData Instance;
    public TextMeshProUGUI countText;

    [SerializeField] private int _curCount;
    public int countNumber
    {
        get { return _curCount; }
    }

    // protect from deleting when player restart scene
    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
        LoadCountData();
    }

    [System.Serializable]
    public class CountData
    {
        public int curCountData;
    }

    // load at start, if not exist create one
    private void LoadCountData()
    {
        int curCountData = _curCount;
        string path = Application.persistentDataPath + "/countfile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            CountData data = JsonUtility.FromJson<CountData>(json);

            curCountData = data.curCountData;
            _curCount = curCountData;
            SaveCountData();
        }
        else
        {
            _curCount = 1;
            SaveCountData();
        }
        print("LaunchCount data Loaded");
    }

    public void SaveCountData()
    {
        int curCountData = _curCount;
        curCountData++;
        CountData data = new CountData();
        data.curCountData = curCountData;
        string json = JsonUtility.ToJson(data);
        File.WriteAllText(Application.persistentDataPath + "/countfile.json", json);
        print("LaunchCount data Saved");
    }
/*
    // for tests delete >>>
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
            DeleteData();
        }
    }

    public void DeleteData()
    {
        string path = Application.persistentDataPath + "/countfile.json";
        if (File.Exists(path))
        {
            File.Delete(Application.persistentDataPath + "/countfile.json");
            print("LaunchCount data Deleted");
        }
    }
    // <<< delete
*/

}
