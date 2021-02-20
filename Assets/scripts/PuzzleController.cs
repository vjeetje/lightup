using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PuzzleController : MonoBehaviour {

    public GameObject tilesParent;
    public Button squarePrefab;

    public Button buttonInventoryLightSources;
    public Button buttonInventoryCrates;
    public Text textInventoryLightSources;
    public Text textInventoryCrates;
    public Image imageInventoryLightSources;
    public Image imageInventoryCrates;
    public Color colorInventoryDefault;
    public Color colorInventoryOk;
    public Color colorInventoryError;
    public Color colorInventoryItemSelected;
    public Color colorInventoryItemNotSelected;

    private Game game;
    private PlayerProgress playerProgress;
    private GameDataManager gameDataManager;
    private int tileSize;

    private void Awake() {
        gameDataManager = GameDataManager.Instance;
        playerProgress = gameDataManager.playerProgress;
        game = gameDataManager.game;
    }

    private void OnEnable() {
        LoadPuzzle(game.problemId);
    }

    private void Start() {
        // todo slow: gui resolution fix
        // todo: add save file encoding/decoding
        // todo sounds

        // todo hint=2) watch video for gems

        //StartCoroutine(LoadPuzzleRoutine(new ProblemId(ProblemType.FibyByFive, ProblemDifficultyType.EASY, 1)));
    }

    private void LoadPuzzle(ProblemId problemId) {
        StartCoroutine(LoadPuzzleRoutine(problemId));
    }

    private IEnumerator LoadPuzzleRoutine(ProblemId problemId) {
        while(ProblemLoader.Instance.isLoading) {
            yield return new WaitForSeconds(0.1f);
        }

        CreatePuzzle(ProblemLoader.Instance.problems[problemId]);
        buttonInventoryLightSources.onClick.AddListener(OnLightsourceItemClicked);
        buttonInventoryCrates.onClick.AddListener(OnCrateItemClicked);
    }

    private void OnLightsourceItemClicked() {
        game.puzzle.inventory.selectedItem = Square.Type.LIGHTBULB;
    }

    private void OnCrateItemClicked() {
        game.puzzle.inventory.selectedItem = Square.Type.CROSS;
    }

    private void Update() {
        UpdateInventory();

        if(game.puzzle != null && game.puzzle.IsSolved() && gameDataManager.GetGameState() == GameState.Game) {
            playerProgress.SetPuzzleSolved(game.puzzle.problem.id, game.puzzle.stars);
            gameDataManager.SetGameState(GameState.PuzzleSolved);
        }
    }

    public void CreatePuzzle(Problem problem) {
        int size = problem.size;
        tileSize = Mathf.FloorToInt(tilesParent.GetComponent<RectTransform>().rect.width / size);

        Square[,] currentProblem = new Square[size, size];
        for(int x = 0; x < size; x++) {
            for(int y = 0; y < size; y++) {
                Button gameObject = Instantiate(squarePrefab, Vector3.zero, Quaternion.identity, tilesParent.transform) as Button;
                float _x = x - (size - 1) / 2.0f;
                float _y = y - (size - 1) / 2.0f;
                gameObject.transform.localPosition = new Vector3(_x * tileSize, _y * tileSize, 0);
                gameObject.name = "Tile (" + x + ", " + y + ")";
                Rect rect = gameObject.GetComponent<RectTransform>().rect;
                gameObject.transform.localScale = new Vector3(tileSize / rect.width, tileSize / rect.height, 1);
                Square square = gameObject.GetComponent<Square>();
                gameObject.onClick.AddListener(delegate { onClick(square); });
                square.type = convertProblemValue(problem.definition[x, y]);
                square.number = problem.definition[x, y];
                square.onLeftBorder = x == 0;
                square.onRightBorder = x == size - 1;
                square.onBottomBorder = y == 0;
                square.onTopBorder = y == size - 1;
                currentProblem[x, y] = square;
            }
        }
        game.puzzle = new Puzzle(problem, currentProblem);
    }

    public void ResetPuzzle() {
        CreatePuzzle(game.puzzle.problem);
    }

    private Square.Type convertProblemValue(int value) {
        if(value == -2) {
            return Square.Type.UNREACHABLE;
        } else if(value == -1) {
            return Square.Type.EMPTY;
        } else {
            return Square.Type.INDICATOR;
        }
    }

    private void onClick(Square square) {
        if((square.type == Square.Type.EMPTY || square.type == Square.Type.CROSS || square.type == Square.Type.LIGHTBULB) && !square.isSetByHint) {
            ChangeSquareType(square);
        }
    }

    private void ChangeSquareType(Square square) {
        if(square.type == game.puzzle.inventory.selectedItem) {
            game.puzzle.SetSquareType(square, Square.Type.EMPTY);
        } else {
            game.puzzle.SetSquareType(square, game.puzzle.inventory.selectedItem);
        }
    }

    private void UpdateInventory() {
        if(game.puzzle == null) {
            return;
        }

        displayInventoryItemAmount(textInventoryLightSources, game.puzzle.inventory.nbLightSourcesCurrent);
        displayInventoryItemAmount(textInventoryCrates, game.puzzle.inventory.nbCratesCurrent);
        imageInventoryLightSources.color = game.puzzle.inventory.selectedItem == Square.Type.LIGHTBULB ? colorInventoryItemSelected : colorInventoryItemNotSelected;
        imageInventoryCrates.color = game.puzzle.inventory.selectedItem == Square.Type.CROSS ? colorInventoryItemSelected : colorInventoryItemNotSelected;
    }

    private void displayInventoryItemAmount(Text inventoryItem, int amount) {
        inventoryItem.text = amount.ToString();
        if(amount < 0) {
            inventoryItem.color = colorInventoryError;
        } else if(amount == 0) {
            inventoryItem.color = colorInventoryOk;
        } else {
            inventoryItem.color = colorInventoryDefault;
        }
    }
}
