using System.IO;
using System.Xml.Serialization;
using UnityEngine;

public class GameDataManager : Singleton<GameDataManager> {

    [HideInInspector]
    public Game game = new Game();

    [HideInInspector]
    public PlayerProgress playerProgress = new PlayerProgress();

    private GameState gameState = GameState.PuzzlesOverview;
    private GameEventManager gameEventManager;

    protected GameDataManager() { }

    private void Awake() {
        gameEventManager = GameEventManager.Instance;
        LoadPlayerProgress();
    }

    private void Start() {
        SetGameState(GameState.PuzzlesOverview);
    }

    public void SavePlayerProgress() {
        XmlSerializer ser = new XmlSerializer(typeof(PlayerProgress));
        using(var writer = new StreamWriter(File.Open(GetPlayerProgressFilePath(), FileMode.OpenOrCreate))) {
            ser.Serialize(writer, playerProgress);
        }
    }

    public void LoadPlayerProgress() {
        if(File.Exists(GetPlayerProgressFilePath())) {
            XmlSerializer ser = new XmlSerializer(typeof(PlayerProgress));
            using(var reader = new StreamReader(File.Open(GetPlayerProgressFilePath(), FileMode.Open))) {
                playerProgress.Copy(ser.Deserialize(reader) as PlayerProgress);
            }
        }
    }

    private string GetPlayerProgressFilePath() {
        return Path.Combine(Application.persistentDataPath, "playerProgress.xml");
    }

    public GameState GetGameState() {
        return gameState;
    }

    public void SetGameState(GameState newGameState) {
        gameEventManager.FireGameStateChanged(gameState = newGameState);
        SavePlayerProgress();
    }

    public void SetGameStateAsPuzzlesOverview() {
        SetGameState(GameState.PuzzlesOverview);
    }

    public void SetGameStateAsGame() {
        SetGameState(GameState.Game);
    }

    public void SetGameStateAsPuzzleSolved() {
        SetGameState(GameState.PuzzleSolved);
    }
}
