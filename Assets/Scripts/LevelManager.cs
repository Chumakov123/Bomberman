using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance { get; private set; }
    public LevelGenerator generator;
    public Player player;

    private void Start()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            EventManager.Instance.OnPlayerDead.AddListener(RestartLevel);
            EventManager.Instance.OnPlayerWin.AddListener(Win);
        }
    }
    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            Quit();
        }
    }
    public void Win()
    {
        Debug.Log("Win");
        StartCoroutine(WinCoroutine());
    }
    IEnumerator WinCoroutine()
    {
        player.DisableControl();
        Overlay.Instance.ShowMessage("Уровень пройден!");
        yield return new WaitForSeconds(1f);
        generator.GenerateBricks();
        player.Respawn();
    }

    public void RestartLevel()
    {
        Debug.Log("RestartLevel");
        StartCoroutine(RestartLevelCoroutine());
    }
    IEnumerator RestartLevelCoroutine()
    {
        yield return new WaitForSeconds(1);
        player.Respawn();
        generator.RespawnBricks();
    }
    public void Quit()
    {
        Application.Quit();
    }
}
