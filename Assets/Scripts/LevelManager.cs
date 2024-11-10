using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public GameObject door;
    public string nextSceneName;
    public GameObject[] chickens;
    public GameObject[] eggs;
    private bool[] chickenOnEgg;

    void Start()
    {
        chickenOnEgg = new bool[chickens.Length];
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            RestartLevel();
        }
    }

    public void ActivateChickenOnEgg(int chickenIndex)
    {
        // mark the chicken as on its egg
        chickenOnEgg[chickenIndex] = true;

        if (AllChickensOnEggs())
        {
            // If all chickens are on eggs, activate the door
            door.SetActive(true);
        }
    }

    private bool AllChickensOnEggs()
    {
        foreach (bool isOnEgg in chickenOnEgg)
        {
            if (!isOnEgg)
                return false;
        }
        return true;
    }

    public void OnPlayerEnterDoor()
    {
        if (AllChickensOnEggs())
        {
            // Transition to the next scene if all chickens are on eggs
            SceneManager.LoadScene(nextSceneName);
        }
    }

    private void RestartLevel()
    {
        string currentScene = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(currentScene);
    }
}