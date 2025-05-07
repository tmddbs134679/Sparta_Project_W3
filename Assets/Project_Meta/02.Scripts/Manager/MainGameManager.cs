using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainGameManager : MonoBehaviour
{

    public Transform player;

    private Vector2 playerLastPos;
    public static MainGameManager Instance { get; private set; }
    public EGAMESTATE CurrentState { get; private set; }

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        SavePlayerPosition();
    }
    public void LoadSceneAsync(EGAMESTATE scene)
    {
        SavePlayerPosition();

        EGAMESTATE state = GetStateFromScene(scene);
        ChangeState(state);

        SceneManager.LoadSceneAsync(scene.ToString());
        CurrentState = scene;
    }

    public void ChangeState(EGAMESTATE newState)
    {
        CurrentState = newState;
    }

    private EGAMESTATE GetStateFromScene(EGAMESTATE scene)
    {
        switch (scene)
        {
            case EGAMESTATE.LOBBY:
                return EGAMESTATE.LOBBY;
            case EGAMESTATE.MINIGAME:
                return EGAMESTATE.MINIGAME;
            default:
                return EGAMESTATE.LOBBY;
        }
    }

    public void SavePlayerPosition()
    {
        if (CurrentState != EGAMESTATE.LOBBY)
            return;

        PlayerPrefs.SetFloat("PlayerX", player.position.x);
        PlayerPrefs.SetFloat("PlayerY", player.position.y);
        PlayerPrefs.Save();
    }

    public void LoadPlayerPosition()
    {
        float x = PlayerPrefs.GetFloat("PlayerX", player.position.x);
        float y = PlayerPrefs.GetFloat("PlayerY", player.position.y);

        player.position = new Vector2(x, y);
    }



}
