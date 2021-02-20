using TMPro;
using UnityEngine;

public class NumberOfGemsIndicator : MonoBehaviour {

    public TextMeshProUGUI text;

    private void Update() {
        text.text = GameDataManager.Instance.playerProgress.gems.ToString();
    }
}
