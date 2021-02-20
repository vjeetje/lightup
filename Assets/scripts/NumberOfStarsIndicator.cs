using TMPro;
using UnityEngine;

public class NumberOfStarsIndicator : MonoBehaviour {

    public TextMeshProUGUI text;

    private void Update() {
        text.text = GameDataManager.Instance.playerProgress.stars.ToString();
    }
}
