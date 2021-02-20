using UnityEngine;

public class EnableStarsPuzzleSolved : MonoBehaviour {

    public GameObject star1;
    public GameObject star2;
    public GameObject star3;

    private Game game;

    void Awake() {
        game = GameDataManager.Instance.game;
    }

    public void Update() {
        if(game.puzzle == null) {
            return;
        }

        star1.SetActive(game.puzzle.stars >= 1);
        star2.SetActive(game.puzzle.stars >= 2);
        star3.SetActive(game.puzzle.stars >= 3);
    }
}
