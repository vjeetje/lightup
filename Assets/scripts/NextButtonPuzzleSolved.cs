using UnityEngine;
using UnityEngine.UI;

public class NextButtonPuzzleSolved : MonoBehaviour {

    public Button button;
    public PuzzleController puzzleController;

    private GameDataManager gameDataManager;

    private void Awake() {
        gameDataManager = GameDataManager.Instance;
        button.onClick.AddListener(Onclick);
    }

    private void Onclick() {
        puzzleController.ResetPuzzle();
        gameDataManager.SetGameState(GameState.Game);
    }
}
