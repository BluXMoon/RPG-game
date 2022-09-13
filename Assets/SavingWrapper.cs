using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using RPG.Saving;

public class SavingWrapper : MonoBehaviour, ISaveable
{
    const string defaultSaveFile = "save";
    string levelName = "";

    private void Awake()
    {
        Load();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            Load();
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            Save();
        }
    }

    private void Load()
    {
        GetComponent<SavingSystem>().Load(defaultSaveFile);
        SceneManager.LoadScene(levelName);
    }

    private void Save()
    {
        GetComponent<SavingSystem>().Save(defaultSaveFile);
    }

    public object CaptureState()
    {
        levelName = SceneManager.GetActiveScene().name;
        return levelName;
    }

    public void RestoreState(object state)
    {
        levelName = (string)state;
    }
}
