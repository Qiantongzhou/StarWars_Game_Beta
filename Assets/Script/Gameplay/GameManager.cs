using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int gameTime = 300; // 5 minutes

    // Team 1
    public GameObject team1Base;
    public GameObject team1WinUI;
    public GameObject team1ShipPrefab;

    // Team 2
    public GameObject team2Base;
    public GameObject team2WinUI;
    public GameObject team2ShipPrefab;


    public GameObject tieUI;

    private int currentTime;
    private bool isGameOver = false;

    // Start is called before the first frame update
    void Start()
    {
        currentTime = gameTime;
        StartCoroutine(GameTimer());
        Cursor.lockState = CursorLockMode.Confined;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isGameOver)
        {
            CheckBaseStatus();
        }
    }

    IEnumerator GameTimer()
    {
        while (currentTime > 0 && !isGameOver)
        {
            yield return new WaitForSeconds(1.0f);
            currentTime--;
        }

        if (!isGameOver)
        {
            EndGame(null); // tie
        }
    }

    void CheckBaseStatus()
    {
        if (!team1Base.activeSelf)
        {
            EndGame("Team 2");
        }
        else if (!team2Base.activeSelf)
        {
            EndGame("Team 1");
        }
    }

    void EndGame(string winningTeam)
    {
        isGameOver = true;

        if (winningTeam == null)
        {
            tieUI.SetActive(true);
        }
        else if (winningTeam == "Team 1")
        {
            team1WinUI.SetActive(true);
        }
        else if (winningTeam == "Team 2")
        {
            team2WinUI.SetActive(true);
        }
    }
}

