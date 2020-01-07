﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private AttributeManager player;
    //[SerializeField] private GameObject gameMenu;

    [Header("UI Elements")]
    [SerializeField] private GameObject gameOverUI;
    [SerializeField] private GameObject startRoundButton;
    [SerializeField] private GameObject roundTimerPanel;
    [SerializeField] private GameObject towerMenu;
    [SerializeField] private Text roundText;

    [Header("Path Spawner")]
    [SerializeField] private Spawner[] spawners;
    private int remaining_waves;
    private int wave = 0;

    private bool isInCombat;

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1.0f;
        //gameMenu.SetActive(false);
        gameOverUI.SetActive(false);
        startRoundButton.SetActive(true);
        foreach (Spawner sp in spawners)
            sp.waveEnded.AddListener(RoundEnd);
    }

    // Update is called once per frame
    void Update()
    {
        if (isInCombat)
        {
            roundTimerPanel.SetActive(true);
            startRoundButton.SetActive(false);
            towerMenu.SetActive(false);
            if (player.health.value <= 0)
            {
                Time.timeScale = 0.00001f;
                gameOverUI.SetActive(true);
            }
        }
        else
        {
            roundTimerPanel.SetActive(false);
            startRoundButton.SetActive(true);
            towerMenu.SetActive(true);
        }
    }

    public void NextWave()
    {
        remaining_waves = spawners.Length;
        isInCombat = true;
        foreach (Spawner sp in spawners)
            sp.SpawnNextWave();
        wave++;
        roundText.text = "Wave " + wave + " / " + spawners[0].waves.Length;
    }

    public void RoundEnd()
    {
        remaining_waves--;
        if (remaining_waves == 0)
        {
            isInCombat = false;
            player.level.Increase(1);
        }
    }

    public void OpenMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}