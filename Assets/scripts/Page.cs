using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Page : MonoBehaviour {

    public ProblemType type;
    public ProblemDifficultyType difficulty;

    private void Start() {
        GetComponentInChildren<TextMeshProUGUI>().text = (int)type + "x" + (int)type + " " + difficulty.ToString();
        GetComponentInChildren<BoardCreator>().type = type;
        GetComponentInChildren<BoardCreator>().difficulty = difficulty;
    }
}
