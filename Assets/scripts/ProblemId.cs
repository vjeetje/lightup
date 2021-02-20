[System.Serializable]
public class ProblemId {

    public ProblemType type;
    public ProblemDifficultyType difficulty;
    public int sequenceId;

    private ProblemId() { }

    public ProblemId(ProblemType type, ProblemDifficultyType difficulty, int sequenceId) {
        this.type = type;
        this.difficulty = difficulty;
        this.sequenceId = sequenceId;
    }

    public override bool Equals(object obj) {
        var id = obj as ProblemId;
        return id != null &&
               type == id.type &&
               difficulty == id.difficulty &&
               sequenceId == id.sequenceId;
    }

    public override int GetHashCode() {
        var hashCode = 1444101314;
        hashCode = hashCode * -1521134295 + type.GetHashCode();
        hashCode = hashCode * -1521134295 + difficulty.GetHashCode();
        hashCode = hashCode * -1521134295 + sequenceId.GetHashCode();
        return hashCode;
    }

    public string getId() {
        return (int)type + "-" + (int)difficulty + "-" + sequenceId;
    }
}
