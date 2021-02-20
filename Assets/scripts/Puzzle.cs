[System.Serializable]
public class Puzzle {

    public Problem problem;
    public Square[,] currentProblem;
    public Inventory inventory;

    public int stars;
    private int thresholdMistakesStarLoss;
    private bool canPunishMistake;

    public Puzzle(Problem problem, Square[,] currentProblem) {
        this.problem = problem;
        this.currentProblem = currentProblem;
        inventory = new Inventory(getNbLightSourcesInSolution(), getAmountOfType(Square.Type.EMPTY) + getAmountOfType(Square.Type.CROSS) - getNbLightSourcesInSolution(), Square.Type.LIGHTBULB);
        stars = 3;
        thresholdMistakesStarLoss = 2;
        canPunishMistake = true;
    }

    private void Update() {
        CalculateLightingSquares();
        CalculateIndicatorStatus();
        CalculateLightSourcesHittingOtherLightsources();
        CalculateInventory();
        CalculateMistakes();
    }

    public void SetSquareType(Square square, Square.Type type) {
        square.type = type;
        Update();
    }

    public void CalculateMistakes() {
        if(stars == 1) {
            return;
        }

        int mistakes = GetAmountLightsourcesPlacedWrong();
        if(mistakes >= thresholdMistakesStarLoss && canPunishMistake) {
            stars--;
            canPunishMistake = false;
        } else if(mistakes == 0) {
            canPunishMistake = true;
        }
    }

    public int GetAmountLightsourcesPlacedWrong() {
        int mistakes = 0;
        for(int x = 0; x < problem.size; x++) {
            for(int y = 0; y < problem.size; y++) {
                if((currentProblem[x, y].type == Square.Type.LIGHTBULB && !problem.solution[x, y])) {
                    mistakes++;
                }
            }
        }
        return mistakes;
    }

    public void CalculateInventory() {
        inventory.nbLightSourcesCurrent = inventory.nbLightSourcesMax - getAmountOfType(Square.Type.LIGHTBULB);
        inventory.nbCratesCurrent = inventory.nbCratesMax - getAmountOfType(Square.Type.CROSS);
    }

    public void CalculateLightingSquares() {
        ResetAllLighting();
        for(int x = 0; x < problem.size; x++) {
            for(int y = 0; y < problem.size; y++) {
                if(currentProblem[x, y].type == Square.Type.LIGHTBULB) {
                    ShowLightFromSource(x, y);
                }
            }
        }
    }

    public void CalculateIndicatorStatus() {
        for(int x = 0; x < problem.size; x++) {
            for(int y = 0; y < problem.size; y++) {
                if(currentProblem[x, y].type == Square.Type.INDICATOR) {
                    currentProblem[x, y].indicatorError = GetLightSourcesNearIndicator(x, y) > currentProblem[x, y].number;
                    currentProblem[x, y].indicatorCorrect = AllNeighboursAreInValidState(x, y) && GetLightSourcesNearIndicator(x, y) == currentProblem[x, y].number;
                }
            }
        }
    }

    public void CalculateLightSourcesHittingOtherLightsources() {
        for(int x = 0; x < problem.size; x++) {
            for(int y = 0; y < problem.size; y++) {
                if(currentProblem[x, y].type == Square.Type.LIGHTBULB) {
                    currentProblem[x, y].hitsOtherLightsource = DoesLightsourceHitOtherLightsource(x, y);
                }
            }
        }
    }

    public int GetLightSourcesNearIndicator(int x, int y) {
        int lightSources = 0;
        if(x + 1 < problem.size && currentProblem[x + 1, y].type == Square.Type.LIGHTBULB) {
            lightSources++;
        }
        if(x - 1 >= 0 && currentProblem[x - 1, y].type == Square.Type.LIGHTBULB) {
            lightSources++;
        }
        if(y + 1 < problem.size && currentProblem[x, y + 1].type == Square.Type.LIGHTBULB) {
            lightSources++;
        }
        if(y - 1 >= 0 && currentProblem[x, y - 1].type == Square.Type.LIGHTBULB) {
            lightSources++;
        }
        return lightSources;
    }

    public bool AllNeighboursAreInValidState(int x, int y) {
        return !((x - 1 >= 0 && !IsSquareInValidSolutionState(x - 1, y))
            || (x + 1 < problem.size && !IsSquareInValidSolutionState(x + 1, y))
            || (y - 1 >= 0 && !IsSquareInValidSolutionState(x, y - 1))
               || (y + 1 < problem.size && !IsSquareInValidSolutionState(x, y + 1)));
    }

    public bool IsSquareInValidSolutionState(int x, int y) {
        return currentProblem[x, y].isLit
            || currentProblem[x, y].type == Square.Type.CROSS
            || currentProblem[x, y].type == Square.Type.UNREACHABLE;
    }

    public void ResetAllLighting() {
        for(int x = 0; x < problem.size; x++) {
            for(int y = 0; y < problem.size; y++) {
                currentProblem[x, y].isLit = false;
            }
        }
    }

    public void ShowLightFromSource(int x, int y) {
        int _x = x, _y = y;

        // left direction
        _x = x;
        _y = y;
        while(_x >= 0 && CanPassLight(currentProblem[_x, _y])) {
            currentProblem[_x, _y].isLit = true;
            _x--;
        }

        // right direction
        _x = x;
        _y = y;
        while(_x <= problem.size - 1 && CanPassLight(currentProblem[_x, _y])) {
            currentProblem[_x, _y].isLit = true;
            _x++;
        }

        // down direction
        _x = x;
        _y = y;
        while(_y >= 0 && CanPassLight(currentProblem[_x, _y])) {
            currentProblem[_x, _y].isLit = true;
            _y--;
        }

        // up direction
        _x = x;
        _y = y;
        while(_y <= problem.size - 1 && CanPassLight(currentProblem[_x, _y])) {
            currentProblem[_x, _y].isLit = true;
            _y++;
        }
    }

    public bool CanPassLight(Square square) {
        return square.type != Square.Type.UNREACHABLE && square.type != Square.Type.INDICATOR;
    }

    public bool IsSolved() {
        if(problem.size == 0) {
            return false;
        }

        for(int x = 0; x < problem.size; x++) {
            for(int y = 0; y < problem.size; y++) {
                if((currentProblem[x, y].type == Square.Type.LIGHTBULB && !problem.solution[x, y])
                    || (currentProblem[x, y].type != Square.Type.LIGHTBULB && problem.solution[x, y])) {
                    return false;
                }
            }
        }
        return true;
    }

    public bool DoesLightsourceHitOtherLightsource(int x, int y) {
        int _x = x, _y = y;

        // left direction
        _x = x;
        _y = y;
        while(--_x >= 0 && CanPassLight(currentProblem[_x, _y])) {
            if(currentProblem[_x, _y].type == Square.Type.LIGHTBULB) {
                return true;
            }
        }

        // right direction
        _x = x;
        _y = y;
        while(++_x <= problem.size - 1 && CanPassLight(currentProblem[_x, _y])) {
            if(currentProblem[_x, _y].type == Square.Type.LIGHTBULB) {
                return true;
            }
        }

        // down direction
        _x = x;
        _y = y;
        while(--_y >= 0 && CanPassLight(currentProblem[_x, _y])) {
            if(currentProblem[_x, _y].type == Square.Type.LIGHTBULB) {
                return true;
            }
        }

        // up direction
        _x = x;
        _y = y;
        while(++_y <= problem.size - 1 && CanPassLight(currentProblem[_x, _y])) {
            if(currentProblem[_x, _y].type == Square.Type.LIGHTBULB) {
                return true;
            }
        }

        return false;
    }

    public int getAmountOfType(Square.Type type) {
        int amount = 0;
        foreach(Square square in currentProblem) {
            if(square.type == type) {
                amount++;
            }
        }
        return amount;
    }

    public int getNbLightSourcesInSolution() {
        int amount = 0;
        foreach(bool isLightSource in problem.solution) {
            if(isLightSource) {
                amount++;
            }
        }
        return amount;
    }
}
