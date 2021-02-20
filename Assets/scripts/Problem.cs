[System.Serializable]
public class Problem {

    public ProblemId id;
    public int size;
    public int[,] definition;
    public bool[,] solution;

    public Problem(ProblemId id, int size, int[,] definition, bool[,] solution) {
        this.id = id;
        this.size = size;
        this.definition = definition;
        this.solution = solution;
    }
}
