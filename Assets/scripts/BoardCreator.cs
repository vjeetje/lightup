using UnityEngine;
using UnityEngine.UI;

public class BoardCreator : MonoBehaviour {

    public ProblemType type;
    public ProblemDifficultyType difficulty;
    public int rows;
    public int cols;
    public GameObject prefab;

    void Start() {
        RectTransform currentRectTransform = transform.GetComponent<RectTransform>();
        GridLayoutGroup gridLayout = gameObject.GetComponent<GridLayoutGroup>();
        float width = currentRectTransform.rect.width / cols;
        float height = currentRectTransform.rect.height / rows;
        gridLayout.cellSize = new Vector2(width, height);
        RectTransform prefabRectTransform = prefab.GetComponent<RectTransform>();
        for(int i = 0; i < rows; i++) {
            for(int j = 0; j < cols; j++) {
                GameObject go = Instantiate(prefab);
                go.transform.SetParent(gameObject.transform, false);
                go.GetComponent<PuzzleAnchor>().problemId = new ProblemId(type, difficulty, i * cols + j);
            }
        }
    }
}
