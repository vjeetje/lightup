using System.Collections;
using UnityEngine;

public class EnableStarsPuzzleTitle : MonoBehaviour {

    public GameObject star1;
    public GameObject star2;
    public GameObject star3;

    private Game game;
    private PlayerProgress playerProgress;
    private int stars;

    private void Awake() {
        game = GameDataManager.Instance.game;
        playerProgress = GameDataManager.Instance.playerProgress;
        GameEventManager.Instance.OnGameStateChanged += OnGameStateChanged;
    }

    private void Start() {
        StartCoroutine(UpdateStars());
        /*star1.SetActive(true);
        star2.SetActive(true);
        star3.SetActive(true);*/
    }

    private void OnGameStateChanged(GameState newGameState) {
        if(newGameState == GameState.Game) {
            StartCoroutine(UpdateStars());
        }
    }

    private void Update() {
        star1.SetActive(stars >= 1);
        star2.SetActive(stars >= 2);
        star3.SetActive(stars >= 3);
    }

    private IEnumerator UpdateStars() {
        while(game.puzzle == null) {
            yield return new WaitForSeconds(0.1f);
        }
        stars = playerProgress.GetStars(game.puzzle.problem.id);
    }

}
