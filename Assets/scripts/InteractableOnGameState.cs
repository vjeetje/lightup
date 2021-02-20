using System;
using UnityEngine;
using UnityEngine.UI;

public class InteractableOnGameState : MonoBehaviour {

    public Button button;
    public GameState[] activeGameStates;

    private GameEventManager gameEventManager;

    void Awake() {
        gameEventManager = GameEventManager.Instance;
        gameEventManager.OnGameStateChanged += OnGameStateChanged;
    }

    private void OnGameStateChanged(GameState newGameState) {
        button.interactable = Array.IndexOf(activeGameStates, newGameState) > -1;
    }
}
