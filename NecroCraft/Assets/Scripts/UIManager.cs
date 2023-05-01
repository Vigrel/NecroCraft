using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using PlayerScripts;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject endPanel;
    [SerializeField] private GameObject startPanel;

    void Start()
    {
        endPanel.SetActive(false);
    }

    void Update()
    {
        if (PlayerController.Instance.GetCurrentHp() <= 0f){
            endPanel.SetActive(true);
            Time.timeScale = 0;
        }
    }

    public void RestartGame()
    {
        GameObject[] objectsToDeactivate = GameObject.FindObjectsOfType<GameObject>();
        foreach (GameObject obj in objectsToDeactivate)
        {
            obj.SetActive(false);
        }
        SceneManager.LoadScene(0);
        foreach (GameObject obj in objectsToDeactivate)
        {
            obj.SetActive(true);
        }
    }

    public void StartGame()
    {
        Time.timeScale = 1;
        startPanel.SetActive(false);
    }

} 