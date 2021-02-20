using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DummyButton : MonoBehaviour {

    public Button button;

    // Use this for initialization
    void Start() {
        button.onClick.AddListener(OnClick);
    }

    private void OnClick() {
        Debug.Log("Button clicked");
    }
}
