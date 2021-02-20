using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PuzzleAnchor : MonoBehaviour {

    public ProblemId problemId;

    public Button button;
    public TextMeshProUGUI text;
    public GameObject star1;
    public GameObject star2;
    public GameObject star3;

    private void Start() {
        GetComponentInChildren<TextMeshProUGUI>().text = (problemId.sequenceId + 1).ToString();
        button.onClick.AddListener(OnClick);
    }

    private void Update() {
        int stars = GameDataManager.Instance.playerProgress.GetStars(problemId);
        star1.SetActive(stars >= 1);
        star2.SetActive(stars >= 2);
        star3.SetActive(stars >= 3);
    }

    private void OnClick() {
        GameDataManager.Instance.game.problemId = problemId;
        GameDataManager.Instance.SetGameState(GameState.Game);
    }
}
