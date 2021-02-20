using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerProgress {

    public int stars;
    public int gems;
    public List<PuzzleProgress> puzzles;

    public PlayerProgress() {
        stars = 0;
        gems = 3;
        puzzles = new List<PuzzleProgress>();
    }

    public void Copy(PlayerProgress other) {
        stars = other.stars;
        gems = other.gems;
        puzzles = new List<PuzzleProgress>(other.puzzles);
    }

    public int GetStars(ProblemId problemId) {
        foreach(PuzzleProgress puzzle in puzzles) {
            if(puzzle.id.Equals(problemId)) {
                return puzzle.stars;
            }
        }
        return 0;
    }

    public void SetPuzzleSolved(ProblemId id, int stars) {
        foreach(PuzzleProgress puzzle in puzzles) {
            if(puzzle.id.Equals(id)) {
                puzzle.stars = Mathf.Max(puzzle.stars, stars);
                CalculateStars();
                return;
            }
        }
        puzzles.Add(new PuzzleProgress(id, stars));
        CalculateStars();
    }

    private void CalculateStars() {
        stars = 0;
        foreach(PuzzleProgress puzzle in puzzles) {
            stars += puzzle.stars;
        }
    }
}
