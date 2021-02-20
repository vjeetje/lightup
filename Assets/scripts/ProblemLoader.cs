using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;

public class ProblemLoader : Singleton<ProblemLoader> {

    public bool isLoading = true;
    public Dictionary<ProblemId, Problem> problems;

    // Use this for initialization
    void Start() {
        problems = new Dictionary<ProblemId, Problem>();
        StartCoroutine(LoadProblems("5x5_classic_pack.txt"));
    }

    // Update is called once per frame
    void Update() {

    }

    public int GetMaxPuzzleId(ProblemType problemType) {
        return 29;
    }

    private IEnumerator LoadProblems(String packname) {
        String path = Path.Combine(Application.streamingAssetsPath, packname);
        String rawFile = "";
        if(path.Contains("://")) {
            UnityWebRequest www = UnityWebRequest.Get(path);
            yield return www.SendWebRequest();
            rawFile = www.downloadHandler.text;
        } else
            rawFile = File.ReadAllText(path);
        ParseAllProblems(rawFile);
        isLoading = false;
    }

    private void ParseAllProblems(string rawProblemFile) {
        string[] lines = rawProblemFile.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);
        foreach(string line in lines) {
            string[] parts = line.Split('|');
            ProblemId problemId = ParseProblemId(parts[0]);
            int size = (int)problemId.type;
            int[,] definition = ParseDefinition(parts[1], size);
            bool[,] solution = ParseSolution(parts[2], size);
            problems.Add(problemId, new Problem(problemId, size, definition, solution));
        }
    }

    private ProblemId ParseProblemId(String data) {
        string[] problemIdKeys = data.Split(',');
        return new ProblemId(
            (ProblemType)Enum.Parse(typeof(ProblemType), problemIdKeys[0]),
            (ProblemDifficultyType)Enum.Parse(typeof(ProblemDifficultyType), problemIdKeys[1]),
            int.Parse(problemIdKeys[2]));
    }

    private int[,] ParseDefinition(String data, int size) {
        int[,] definition = new int[size, size];
        for(int i = 0; i < size; i++) {
            for(int j = 0; j < size; j++) {
                definition[i, j] = ConvertCharToIntDefintion(data[i * size + j]);
            }
        }
        return definition;
    }

    private int ConvertCharToIntDefintion(char c) {
        if(c == 'B') {
            return -2;
        } else if(c == '.') {
            return -1;
        } else {
            return (int)Char.GetNumericValue(c);
        }
    }

    private bool[,] ParseSolution(String data, int size) {
        bool[,] solution = new bool[size, size];
        for(int i = 0; i < size; i++) {
            for(int j = 0; j < size; j++) {
                solution[i, j] = data[i * size + j] == 'y';
            }
        }
        return solution;
    }
}
