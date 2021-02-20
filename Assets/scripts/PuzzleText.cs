using TMPro;
using UnityEngine;

public class PuzzleText : MonoBehaviour {

    public TextMeshProUGUI text;

    private void Update() {
        ProblemId problemId = GameDataManager.Instance.game.problemId;
        text.text = "<color=#097000FF>" + (int)problemId.type + "x" + (int)problemId.type + "</color> Puzzle " + problemId.sequenceId;
    }
}
