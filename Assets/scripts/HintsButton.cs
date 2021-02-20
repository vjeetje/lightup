using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class HintsButton : MonoBehaviour {

    public Button button;
    private Game game;


    private void Start() {
        game = GameDataManager.Instance.game;
        button.onClick.AddListener(onClick);
    }

    private void onClick() {
        // add missing lightsource
        List<Square> availableLightSources = new List<Square>();
        for(int x = 0; x < game.puzzle.problem.size; x++) {
            for(int y = 0; y < game.puzzle.problem.size; y++) {
                if(game.puzzle.problem.solution[x, y] && game.puzzle.currentProblem[x, y].type != Square.Type.LIGHTBULB) {
                    availableLightSources.Add(game.puzzle.currentProblem[x, y]);
                }
            }
        }
        if(availableLightSources.Count > 0) {
            Square square = availableLightSources.ElementAt(Random.Range(0, availableLightSources.Count));
            game.puzzle.SetSquareType(square, Square.Type.LIGHTBULB);
            square.isSetByHint = true;
            return;
        }

        // remove invalid lightsource
        availableLightSources = new List<Square>();
        for(int x = 0; x < game.puzzle.problem.size; x++) {
            for(int y = 0; y < game.puzzle.problem.size; y++) {
                if(!game.puzzle.problem.solution[x, y] && game.puzzle.currentProblem[x, y].type == Square.Type.LIGHTBULB) {
                    availableLightSources.Add(game.puzzle.currentProblem[x, y]);
                }
            }
        }
        if(availableLightSources.Count > 0) {
            Square square = availableLightSources.ElementAt(Random.Range(0, availableLightSources.Count));
            game.puzzle.SetSquareType(square, Square.Type.CROSS);
            square.isSetByHint = true;
            return;
        }
    }
}
