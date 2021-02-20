using System;
using UnityEngine;

public class ActiveOnGameState : MonoBehaviour {

    public GameState[] activeGameStates;
    public GameObject controlledGameObject;

    private GameEventManager gameEventManager;

    void Awake() {
        gameEventManager = GameEventManager.Instance;
        gameEventManager.OnGameStateChanged += OnGameStateChanged;
    }

    private void OnGameStateChanged(GameState newGameState) {
        controlledGameObject.SetActive(Array.IndexOf(activeGameStates, newGameState) > -1);
    }
}
