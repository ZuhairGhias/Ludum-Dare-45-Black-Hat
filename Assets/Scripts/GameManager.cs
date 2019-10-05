﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public enum GameState { INPROGRESS, UPGRADING, GAMEOVER }

    [SerializeField] private SpawnSystem spawnSystem;
    [SerializeField] private TimerUI timerUI;
    [SerializeField] private float timeRemaining = 20f;
    [SerializeField] private Vector2 spawnIntervals;
    [SerializeField] private float policeSpawnChance = 0.25f;

    private GameState currentState = GameState.INPROGRESS;
    private int currentRound = 0;

    private void Start()
    {
        StartRound(currentRound);
    }

    private void Update()
    {
        UpdateTimer();
    }

    public void StartRound(int round)
    {
        StartCoroutine(SpawnNPCs());
    }

    public void EndRound(bool success)
    {
        if (success)
        {
            Debug.Log("[GameManager] Round complete, upgrading phase");

            currentState = GameState.UPGRADING;
            currentRound++;
        }
        else
        {
            Debug.Log("[GameManager] Game over, you lose");

            currentState = GameState.GAMEOVER;
        }
    }

    private IEnumerator SpawnNPCs()
    {
        while (currentState == GameState.INPROGRESS)
        {
            // Randomize movement direction
            int direction = 1;
            if (Random.Range(0f, 1f) < 0.5f)
            {
                direction = -1;
            }

            // Randomize NPC type
            if (Random.Range(0f, 1f) <= policeSpawnChance)
            {
                spawnSystem.SpawnPolice(direction);
            }
            else
            {
                spawnSystem.SpawnPedestrian(direction);
            }

            yield return new WaitForSeconds(Random.Range(spawnIntervals.x, spawnIntervals.y));
        }
    }

    private void UpdateTimer()
    {
        if (currentState == GameState.INPROGRESS)
        {
            timeRemaining -= Time.deltaTime;

            if (timeRemaining <= 0f)
            {
                currentState = GameState.UPGRADING;
            }

            timerUI.UpdateTimer(timeRemaining);
        }
    }
}
