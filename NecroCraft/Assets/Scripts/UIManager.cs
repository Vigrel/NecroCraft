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
    [SerializeField] private GameObject player;

    void Start()
    {
        endPanel.SetActive(false);
    }

    void Update()
    {
        if (PlayerController.Instance.GetCurrentHp() <= 0f){
            endPanel.SetActive(true);
            Destroy(player);
        }
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(0);
    }

    public void StartGame()
    {
        //Time.timeScale = 1;
        startPanel.SetActive(false);
    }

} 