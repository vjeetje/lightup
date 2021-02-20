using UnityEngine;

public class GameEventManager : Singleton<GameEventManager> {
    protected GameEventManager() { }

    public delegate void GameStateChangedHandler(GameState gameState);
    public event GameStateChangedHandler OnGameStateChanged;

    public void FireGameStateChanged(GameState gameState) {
        if(OnGameStateChanged != null) {
            OnGameStateChanged(gameState);
        }
    }
}
