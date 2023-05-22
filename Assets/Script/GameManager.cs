using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    public static GameManager gamemanager;
    [SerializeField] Data data;
    public int startScene;
    public int maxScene;
    public bool canEnd;
    public GameObject endUI;
    public GameObject stopUI;
    public GameObject failUI;

    private void Awake()
    {
        if (GameManager.gamemanager == null) { GameManager.gamemanager = this; DontDestroyOnLoad(this.gameObject); }
        else { Destroy(this.gameObject); return; }
       GameManager.gamemanager.maxScene = SceneManager.sceneCountInBuildSettings;
        Load();
        GameManager.gamemanager.canEnd = false;
    }
    private void Update()
    {
        //if (Input.GetKeyDown(KeyCode.K)) { SceneLoad(2); }
        if (Input.GetKeyDown(KeyCode.Escape) && SceneManager.GetActiveScene().buildIndex != 0) { ChangeStopUI();  }
        if (GameManager.gamemanager.canEnd && Input.GetKeyDown(KeyCode.E)) { endUI.SetActive(true); Time.timeScale = 0; }
    }
    public void SceneLoad(int x)
    {
        Debug.Log(x);Time.timeScale = 1;
        GameManager.gamemanager.canEnd = false;
        GameManager.gamemanager.endUI.SetActive(false);
        GameManager.gamemanager.stopUI.SetActive(false);
        GameManager.gamemanager.failUI.SetActive(false);
        
        SceneManager.LoadScene(x);


    }

    public void Save()
    {
        GameManager.gamemanager.data.Save();
    }

    public void Load()
    {
        if (!File.Exists(Path.Combine(Application.persistentDataPath, "Data.cundang"))){ GameManager.gamemanager.data.Save(); }
        GameManager.gamemanager.data.Load();
        
    }

    public void ReStart()
    {
        SceneLoad(1);
    }

    public void Continue()
    {
        if (GameManager.gamemanager.data.SaveScene + 1 >= gamemanager.maxScene) { SceneLoad(gamemanager.maxScene-1); }
        else { SceneLoad(GameManager.gamemanager.data.SaveScene + 1);Debug.Log(2); }
    }

    public void GoNext()
    {
        if (GameManager.gamemanager.data.SaveScene <= SceneManager.GetActiveScene().buildIndex) { Save(); Load(); }
        if (SceneManager.GetActiveScene().buildIndex + 1 >= gamemanager.maxScene) { SceneLoad(0); Save(); Load(); }
        else { SceneLoad(SceneManager.GetActiveScene().buildIndex + 1); }

    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void Success() {
        gamemanager.canEnd = true;
    }

    public void GoMenu()
    {
        if (GameManager.gamemanager.data.SaveScene < SceneManager.GetActiveScene().buildIndex) { Save(); Load(); }
        GameManager.gamemanager.endUI.SetActive(false);
        GameManager.gamemanager.stopUI.SetActive(false);
        GameManager.gamemanager.failUI.SetActive(false);
        SceneLoad(0);
        
    }
    public void ContinueGame()
    {
        Time.timeScale = 1;
        GameManager.gamemanager.stopUI.SetActive(false);
    
    }

    public void Stop()
    { 
        Time.timeScale = 0;
        GameManager.gamemanager.stopUI.SetActive(true);
       
    }
    public void ChangeStopUI()
    {
        if (stopUI.activeInHierarchy == true) { gamemanager.stopUI.SetActive(false); Time.timeScale = 1; }
        else { stopUI.SetActive(true); Time.timeScale = 0; }
    
    }
    public void ShowEndUI()
    {
        if (GameManager.gamemanager.canEnd) { gamemanager.endUI.SetActive(true); }
    
    }

    public void GoMenuMachine()
    {
        if (GameManager.gamemanager.data.SaveScene < SceneManager.GetActiveScene().buildIndex){ Save();Load(); }
        GameManager.gamemanager.endUI.SetActive(false);
        GameManager.gamemanager.stopUI.SetActive(false);
        GameManager.gamemanager.failUI.SetActive(false);
        SceneLoad(0);
        GameManager.gamemanager.endUI.SetActive(false);
        GameManager.gamemanager.stopUI.SetActive(false);
        GameManager.gamemanager.failUI.SetActive(false);

    }
    public void ReLoad()
    {
        GameManager.gamemanager.endUI.SetActive(false);
        GameManager.gamemanager.stopUI.SetActive(false);
        GameManager.gamemanager.failUI.SetActive(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1;
    }

    public void Fail()
    {
        gamemanager.failUI.SetActive(true);
        Time.timeScale = 0;
    }
    public void End()
    {

        GameManager.gamemanager.endUI.SetActive(true); Time.timeScale = 0;
    }
}
