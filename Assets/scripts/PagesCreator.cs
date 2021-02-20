using System;
using UnityEngine;
using UnityEngine.UI.Extensions;

public class PagesCreator : MonoBehaviour {

    public GameObject prefab;
    public HorizontalScrollSnap horizontalScrollSnap;

    void Start() {
        for(int i = 0; i < 1; i++) {//Enum.GetValues(typeof(ProblemType)).Length
            for(int j = 0; j < 3; j++) {//Enum.GetValues(typeof(ProblemDifficultyType)).Length
                GameObject go = Instantiate(prefab);
                go.transform.SetParent(gameObject.transform, false);
                go.GetComponent<Page>().type = (ProblemType)Enum.GetValues(typeof(ProblemType)).GetValue(i);
                go.GetComponent<Page>().difficulty = (ProblemDifficultyType)Enum.GetValues(typeof(ProblemDifficultyType)).GetValue(j);
                horizontalScrollSnap.AddChild(go);
            }
        }
    }
}
