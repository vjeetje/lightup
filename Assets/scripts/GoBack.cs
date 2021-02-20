using UnityEngine;
using UnityEngine.UI;

public class GoBack : MonoBehaviour {

    public Button button;

    private void Start() {
        button.onClick.AddListener(OnClick);
    }

    public void OnClick() {
        if(GameDataManager.Instance.GetGameState() == GameState.Game) {
            GameDataManager.Instance.SetGameState(GameState.PuzzlesOverview);
        }
    }
}
