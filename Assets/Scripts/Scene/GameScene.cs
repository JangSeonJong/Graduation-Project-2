using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameScene : MonoBehaviour
{
    public static GameScene instance;
    public string currentScene;
    public GameObject player;
    public InputManager input;
    public Canvas canvas; 
    public Map map;

    void Start()
    {
        if (instance == null)
        {
            instance = this;
            player = FindObjectOfType<PlayerController>().gameObject;
            input = FindObjectOfType<InputManager>();
            DontDestroyOnLoad(instance);
            DontDestroyOnLoad(player);
            DontDestroyOnLoad(canvas);
            DontDestroyOnLoad(input);
        }
        else
        {
            Destroy(gameObject);
            Destroy(player.gameObject);
            Destroy(canvas.gameObject);
            Destroy(input.gameObject);
            return;
        }

        currentScene = SceneManager.GetActiveScene().name;
    }


    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (player != null)
            player.transform.position = Vector3.zero;

        ResetWorld();
        GameManager.canMove = true;
    }

    public void MoveScene(string scene)
    {
        if (currentScene == scene)
            return;

        currentScene = scene;

        ClearWorld();
        map.OffButton(scene);
        map.CloseMap();
        SceneManager.LoadScene(scene);
    }

    public void ClearWorld()
    {
        ItemSpawner[] itemSpawner = FindObjectsOfType<ItemSpawner>();
        if (itemSpawner != null)
        {
            foreach (ItemSpawner spawner in itemSpawner)
            {
                if (spawner != null)
                {
                    spawner.ClearSpawner();
                }
            }
        }

        SpawningPool spawningPool = FindObjectOfType<SpawningPool>();
        if (spawningPool != null)
        {
            spawningPool.ClearMonster();
        }
    }

    public void ResetWorld()
    {
        ItemSpawner[] itemSpawner = FindObjectsOfType<ItemSpawner>();
        if (itemSpawner != null)
        {
            foreach (ItemSpawner spawner in itemSpawner)
            {
                if (spawner != null)
                {
                    spawner.SpawnItem();
                }
            }
        }

        SpawningPool spawningPool = FindObjectOfType<SpawningPool>();
        if (spawningPool != null)
        {
            spawningPool.SpawnMonster();
        }
    }
}