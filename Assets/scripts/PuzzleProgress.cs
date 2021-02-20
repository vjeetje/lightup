[System.Serializable]
public class PuzzleProgress {

    public ProblemId id;
    public int stars;

    private PuzzleProgress() { }

    public PuzzleProgress(ProblemId id, int stars) {
        this.id = id;
        this.stars = stars;
    }
}
